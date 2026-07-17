using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TargCCOrders.DataController;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Supplier-order email — replaces the Excel "שליחה" sheet flow:
    /// composes an HTML email with the order's product table (purchase prices)
    /// for Biobee, sends it via SMTP when configured, and updates the
    /// SupplierOrder status. When SMTP is not configured the composed email is
    /// returned so the user can copy it into their mail client.
    /// </summary>
    [Route("api/supplierOrders")]
    [ApiController]
    public class SupplierOrderSendController : ControllerBase
    {
        // POST api/supplierOrders/{id}/compose — build subject+body from the linked order
        [HttpPost("{id}/compose")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult Compose(long id)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var so = new clsSupplierOrder();
            var fault = so.GetByID(id, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(fault.Message);

            var (subject, body, error) = ComposeEmail(so, requester);
            if (error != null) return BadRequest(new { message = error });

            // Persist the composed email on the record
            so.EmailSubject = subject;
            so.EmailBody = body;
            fault = so.Update(requester);
            if (!fault.isOK) return BadRequest(fault.Message);

            return Ok(new { subject, body });
        }

        // POST api/supplierOrders/{id}/send
        [HttpPost("{id}/send")]
        [Authorize(Policy = "AdminUI")]
        public ActionResult Send(long id)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            var so = new clsSupplierOrder();
            var fault = so.GetByID(id, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(fault.Message);

            if (string.IsNullOrWhiteSpace(so.SupplierEmail))
                return BadRequest(new { message = "לא הוגדרה כתובת מייל לספק בהזמנה זו." });

            // Compose if empty
            var subject = so.EmailSubject;
            var body = so.EmailBody;
            if (string.IsNullOrWhiteSpace(body))
            {
                var (s, b, error) = ComposeEmail(so, requester);
                if (error != null) return BadRequest(new { message = error });
                subject = s; body = b;
                so.EmailSubject = s; so.EmailBody = b;
            }

            // SMTP from app.config (TargCC standard keys)
            var smtpServer = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPServer"];
            if (string.IsNullOrWhiteSpace(smtpServer))
            {
                // Not configured — return the composed email for manual sending
                so.Update(requester);
                return Ok(new
                {
                    sent = false,
                    message = "שרת SMTP אינו מוגדר (app.config). המייל הוכן — ניתן להעתיק ולשלוח ידנית.",
                    to = so.SupplierEmail,
                    subject,
                    body
                });
            }

            try
            {
                var port = int.TryParse(System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPPort"], out var p) ? p : 25;
                var enableSsl = string.Equals(System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPEnableSSL"], "True", StringComparison.OrdinalIgnoreCase);
                var fromEmail = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPEmailFrom"];
                var fromName = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPNameFrom"];
                var user = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPUserName"];
                var pwd = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.SMTPPassword"];

                if (string.IsNullOrWhiteSpace(fromEmail))
                    return BadRequest(new { message = "חסר TargCCOrders.SMTPEmailFrom ב-app.config." });

                using var client = new SmtpClient(smtpServer, port) { EnableSsl = enableSsl };
                if (!string.IsNullOrWhiteSpace(user))
                    client.Credentials = new NetworkCredential(user, pwd);

                using var mail = new MailMessage
                {
                    From = new MailAddress(fromEmail, string.IsNullOrWhiteSpace(fromName) ? fromEmail : fromName),
                    Subject = string.IsNullOrWhiteSpace(subject) ? "הזמנת ספק" : subject,
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8
                };
                mail.To.Add(so.SupplierEmail);

                client.Send(mail);

                so.EmailStatus = clsEnums.enmEmailStatus.Sent;
                so.SentDate = DateTime.Now;
                fault = so.Update(requester);
                if (!fault.isOK) return BadRequest(fault.Message);

                return Ok(new { sent = true, message = "המייל נשלח לספק.", to = so.SupplierEmail });
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Supplier order email send failed for ID {Id}", id);
                try { so.EmailStatus = clsEnums.enmEmailStatus.Failed; so.Update(requester); } catch { }
                return StatusCode(502, new { sent = false, message = "שליחת המייל נכשלה: " + ex.Message });
            }
        }

        // ─────────────────────────────────────────────────────────────

        private static (string subject, string body, string? error) ComposeEmail(clsSupplierOrder so, clsRequester requester)
        {
            if (so.OrderHeaderID <= 0)
                return ("", "", "הזמנת הספק אינה מקושרת להזמנת לקוח — אין ממה להרכיב את המייל.");

            var header = new clsOrderHeader(clsEnums.enmLoadParent.DoNotLoad);
            var fault = header.GetByID(so.OrderHeaderID, requester, vMustExist: true);
            if (!fault.isOK) return ("", "", "הזמנת הלקוח המקושרת לא נמצאה.");

            fault = header.FillOrderLines(requester);
            if (!fault.isOK) return ("", "", "שגיאה בטעינת שורות ההזמנה.");

            var customer = new clsCustomer();
            customer.GetByID(header.CustomerID, requester);

            var rows = new StringBuilder();
            decimal total = 0;
            foreach (clsOrderLine line in header.OrderLines)
            {
                var product = new clsProduct();
                var pf = product.GetByID(line.ProductID, requester);
                var name = pf.isOK ? product.ProductName : $"מוצר {line.ProductID}";
                var code = pf.isOK ? product.ProductCode : "";
                var cost = line.UnitCost;
                var lineCost = cost * line.Quantity;
                total += lineCost;
                rows.Append("<tr>")
                    .Append($"<td style='border:1px solid #ccc;padding:6px'>{WebUtility.HtmlEncode(code)}</td>")
                    .Append($"<td style='border:1px solid #ccc;padding:6px'>{WebUtility.HtmlEncode(name)}</td>")
                    .Append($"<td style='border:1px solid #ccc;padding:6px;text-align:center'>{line.Quantity}</td>")
                    .Append($"<td style='border:1px solid #ccc;padding:6px;text-align:left'>{cost:N2} ₪</td>")
                    .Append($"<td style='border:1px solid #ccc;padding:6px;text-align:left'>{lineCost:N2} ₪</td>")
                    .Append("</tr>");
            }

            var deliveryInfo = "";
            if (so.RequestedDeliveryDate > DateTime.MinValue)
                deliveryInfo = $"<p><b>מועד אספקה מבוקש:</b> {so.RequestedDeliveryDate:dd/MM/yyyy}</p>";

            var subject = string.IsNullOrWhiteSpace(so.EmailSubject)
                ? $"הזמנה מס' {header.OrderNumber}"
                : so.EmailSubject;

            var body = $@"<div dir='rtl' style='font-family:Arial,sans-serif;font-size:14px'>
<p>שלום,</p>
<p>נשמח לקבל את הפריטים הבאים עבור הזמנה מס' <b>{header.OrderNumber}</b>:</p>
<table style='border-collapse:collapse;border:1px solid #ccc'>
<thead><tr style='background:#f0f4f8'>
<th style='border:1px solid #ccc;padding:6px'>מק""ט</th>
<th style='border:1px solid #ccc;padding:6px'>שם המוצר</th>
<th style='border:1px solid #ccc;padding:6px'>כמות</th>
<th style='border:1px solid #ccc;padding:6px'>מחיר</th>
<th style='border:1px solid #ccc;padding:6px'>סה""כ</th>
</tr></thead>
<tbody>{rows}</tbody>
<tfoot><tr style='background:#fef5e7;font-weight:bold'>
<td colspan='4' style='border:1px solid #ccc;padding:6px'>סה""כ לפני מע""מ</td>
<td style='border:1px solid #ccc;padding:6px;text-align:left'>{total:N2} ₪</td>
</tr></tfoot>
</table>
{deliveryInfo}
<p>תודה,<br/>{WebUtility.HtmlEncode(requester.UserFullName ?? "")}</p>
</div>";

            return (subject, body, null);
        }
    }
}
