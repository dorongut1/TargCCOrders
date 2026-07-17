/* ═══════════════════════════════════════════════════════════════════════════
   TargCCOrders — Migration Script  (2026-07-16)
   ═══════════════════════════════════════════════════════════════════════════
   מטרה: תיקון כל הממצאים הקריטיים מסקירת הקוד + השלמת שדות מהאקסל המקורי.
   הרץ על:  TargCCOrdersNew
   ⚠️  בצע גיבוי מלא לפני הרצה:  BACKUP DATABASE TargCCOrdersNew TO DISK='...'
   הסקריפט אידמפוטנטי — אפשר להריץ אותו יותר מפעם אחת בבטחה.

   אחרי הרצה מוצלחת: להריץ מחדש TargCC (Sprocs + DBController + WebAPI)
   כדי שהשדות החדשים (RivhitCustomerNo, enmDefaultDeliveryMethod) ייחשפו
   ב-DTO וב-UI.
   ═══════════════════════════════════════════════════════════════════════════ */

SET XACT_ABORT ON;
SET NOCOUNT ON;
GO

/* ───────────────────────────────────────────────────────────────────────────
   1. ניקוי טבלאות זבל (טבלאות בדיקה ושאריות מיגרציה)
   ─────────────────────────────────────────────────────────────────────────── */
IF OBJECT_ID('dbo.CheckPrg','U')  IS NOT NULL DROP TABLE dbo.CheckPrg;
IF OBJECT_ID('dbo.CheckPrg1','U') IS NOT NULL DROP TABLE dbo.CheckPrg1;
GO
-- גיבוי zzPreviousRole לפני מחיקה (שאריות מיגרציית תפקידים חד-פעמית)
IF OBJECT_ID('dbo.zzPreviousRole','U') IS NOT NULL
BEGIN
    IF OBJECT_ID('dbo.zzPreviousRole_Backup','U') IS NULL
        SELECT * INTO dbo.zzPreviousRole_Backup FROM dbo.zzPreviousRole;
    DROP TABLE dbo.zzPreviousRole;
END
GO
-- הסרה מ-c_Table כדי ש-TargCC יפסיק לייצר עבורן קוד
DELETE FROM dbo.c_Table WHERE TableName IN ('CheckPrg','CheckPrg1','zzPreviousRole');
GO

/* ───────────────────────────────────────────────────────────────────────────
   2. OrderHeader — שיעור מע"מ פר-הזמנה (snapshot) + Sequence למספרי הזמנה
   ─────────────────────────────────────────────────────────────────────────── */
IF COL_LENGTH('dbo.OrderHeader','VATRatePercent') IS NULL
BEGIN
    ALTER TABLE dbo.OrderHeader
        ADD VATRatePercent decimal(5,2) NOT NULL
            CONSTRAINT DF_OrderHeader_VATRatePercent DEFAULT (18.00);
END
GO
-- Backfill: הזמנות לפני 1.1.2025 היו במע"מ 17%
UPDATE dbo.OrderHeader SET VATRatePercent = 17.00
WHERE OrderDate < '2025-01-01' AND VATRatePercent = 18.00;
GO

-- Sequence למספרי הזמנה — מונע התנגשויות של MAX+1 במקביל
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'seq_OrderNumber')
BEGIN
    DECLARE @next int = (SELECT ISNULL(MAX(OrderNumber),0) + 1 FROM dbo.OrderHeader);
    DECLARE @sql nvarchar(400) =
        N'CREATE SEQUENCE dbo.seq_OrderNumber AS int START WITH ' + CAST(@next AS nvarchar(20)) + N' INCREMENT BY 1;';
    EXEC sp_executesql @sql;
END
GO
-- פרוצדורה לקבלת המספר הבא (בשימוש ה-WebAPI)
CREATE OR ALTER PROCEDURE dbo.sp_GetNextOrderNumber
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NEXT VALUE FOR dbo.seq_OrderNumber AS NextOrderNumber;
END
GO

/* ───────────────────────────────────────────────────────────────────────────
   3. חישוב סכומי הזמנה — הבעיה הקריטית ביותר:
      clc_TotalAmount / clc_VATAmount / clc_TotalWithVAT הן עמודות רגילות
      שאף גורם לא עדכן מעולם (כל ההזמנות היו 0).
      הפתרון: טריגר על OrderLine שמעדכן את הכותרת בכל שינוי שורה.
   ─────────────────────────────────────────────────────────────────────────── */
CREATE OR ALTER TRIGGER dbo.trg_OrderLine_RecalcHeaderTotals
ON dbo.OrderLine
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Affected AS (
        SELECT OrderHeaderID FROM inserted
        UNION
        SELECT OrderHeaderID FROM deleted
    ),
    Totals AS (
        SELECT a.OrderHeaderID,
               ISNULL(SUM(CASE WHEN ol.DeletedOn IS NULL THEN ol.clc_LineTotal END),0) AS TotalAmount
        FROM Affected a
        LEFT JOIN dbo.OrderLine ol ON ol.OrderHeaderID = a.OrderHeaderID
        GROUP BY a.OrderHeaderID
    )
    UPDATE oh SET
        oh.clc_TotalAmount  = ROUND(t.TotalAmount, 2),
        oh.clc_VATAmount    = ROUND(t.TotalAmount * oh.VATRatePercent / 100.0, 2),
        oh.clc_TotalWithVAT = ROUND(t.TotalAmount, 2) + ROUND(t.TotalAmount * oh.VATRatePercent / 100.0, 2)
    FROM dbo.OrderHeader oh
    INNER JOIN Totals t ON t.OrderHeaderID = oh.ID;
END
GO

-- פרוצדורת עזר: חישוב מחדש של כל ההזמנות (הרצה חד-פעמית + לתחזוקה)
CREATE OR ALTER PROCEDURE dbo.sp_RecalcAllOrderTotals
AS
BEGIN
    SET NOCOUNT ON;
    ;WITH Totals AS (
        SELECT oh.ID,
               ISNULL(SUM(CASE WHEN ol.DeletedOn IS NULL THEN ol.clc_LineTotal END),0) AS TotalAmount
        FROM dbo.OrderHeader oh
        LEFT JOIN dbo.OrderLine ol ON ol.OrderHeaderID = oh.ID
        GROUP BY oh.ID
    )
    UPDATE oh SET
        oh.clc_TotalAmount  = ROUND(t.TotalAmount, 2),
        oh.clc_VATAmount    = ROUND(t.TotalAmount * oh.VATRatePercent / 100.0, 2),
        oh.clc_TotalWithVAT = ROUND(t.TotalAmount, 2) + ROUND(t.TotalAmount * oh.VATRatePercent / 100.0, 2)
    FROM dbo.OrderHeader oh
    INNER JOIN Totals t ON t.ID = oh.ID
    WHERE ISNULL(oh.clc_TotalAmount,0)  <> ROUND(t.TotalAmount,2)
       OR ISNULL(oh.clc_VATAmount,0)    <> ROUND(t.TotalAmount * oh.VATRatePercent / 100.0, 2);
END
GO
EXEC dbo.sp_RecalcAllOrderTotals;
GO

/* ───────────────────────────────────────────────────────────────────────────
   4. תיקון trg_OrderLine_SetUnitCost — הטריגר הישן דרס את עלות הקנייה
      ההיסטורית בכל עדכון שורה (הרס נתוני רווחיות היסטוריים).
      החדש: קובע עלות רק בשורה חדשה או כשמחליפים מוצר.
   ─────────────────────────────────────────────────────────────────────────── */
CREATE OR ALTER TRIGGER dbo.trg_OrderLine_SetUnitCost
ON dbo.OrderLine
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ol
        SET ol.blg_UnitCost = p.BaseCost
    FROM dbo.OrderLine ol
    INNER JOIN inserted i  ON ol.ID = i.ID
    INNER JOIN dbo.Product p ON p.ID = i.ProductID
    LEFT  JOIN deleted d   ON d.ID = i.ID
    WHERE d.ID IS NULL                              -- שורה חדשה
       OR d.ProductID <> i.ProductID                -- הוחלף מוצר
       OR ISNULL(i.blg_UnitCost,0) = 0;             -- עלות טרם נקבעה
END
GO

/* ───────────────────────────────────────────────────────────────────────────
   5. תיקון trg_OrderHeader_AutoCreateDelivery —
      הישן: כתובת ארוכה מ-500 תווים הפילה את יצירת ההזמנה, כתובת NULL
      איפסה גם את העיר, נוצרו כפילויות משלוח, ו-AddedBy לא נכתב.
   ─────────────────────────────────────────────────────────────────────────── */
CREATE OR ALTER TRIGGER dbo.trg_OrderHeader_AutoCreateDelivery
ON dbo.OrderHeader
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Delivery
        (OrderHeaderID, DeliveryAddress, ContactPhone, ContactName,
         enmDeliveryMethod, enmDeliveryStatus, AddedBy, AddedOn)
    SELECT
        i.ID,
        LEFT(CONCAT(c.[Address], N', ' + NULLIF(c.City, N'')), 500),
        c.Phone,
        c.CustomerName,
        i.enmDeliveryMethod,
        'Pending',
        ISNULL(i.AddedBy, N'System'),
        GETDATE()
    FROM inserted i
    INNER JOIN dbo.Customer c ON c.ID = i.CustomerID
    WHERE i.enmDeliveryMethod IS NOT NULL
      AND i.enmDeliveryMethod <> 'NoDelivery'
      AND NOT EXISTS (SELECT 1 FROM dbo.Delivery d
                      WHERE d.OrderHeaderID = i.ID AND d.DeletedOn IS NULL);
END
GO

/* ───────────────────────────────────────────────────────────────────────────
   6. CustomerDebt — תיקון "חוב נעלם":
      clc_RemainingAmount היה DebtAmount-PaidAmount ללא ISNULL —
      PaidAmount=NULL העלים את החוב מכל הדוחות.
   ─────────────────────────────────────────────────────────────────────────── */
UPDATE dbo.CustomerDebt SET PaidAmount = 0 WHERE PaidAmount IS NULL;
GO
-- שינוי PaidAmount ל-NOT NULL מחייב הסרה זמנית של כל התלויות בעמודה:
-- העמודה המחושבת, האינדקס וה-CHECK — ואז יצירתם מחדש.
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('dbo.CustomerDebt')
             AND name = 'PaidAmount' AND is_nullable = 1)
   OR EXISTS (SELECT 1 FROM sys.computed_columns
              WHERE object_id = OBJECT_ID('dbo.CustomerDebt')
                AND name = 'clc_RemainingAmount'
                AND definition NOT LIKE '%isnull%')
BEGIN
    -- 1) הסרת תלויות
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_CustomerDebt_enmDebtStatus' AND object_id=OBJECT_ID('dbo.CustomerDebt'))
        DROP INDEX IX_CustomerDebt_enmDebtStatus ON dbo.CustomerDebt;

    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_CustomerDebt_Amounts')
        ALTER TABLE dbo.CustomerDebt DROP CONSTRAINT CK_CustomerDebt_Amounts;

    IF EXISTS (SELECT 1 FROM sys.computed_columns
               WHERE object_id = OBJECT_ID('dbo.CustomerDebt') AND name = 'clc_RemainingAmount')
        ALTER TABLE dbo.CustomerDebt DROP COLUMN clc_RemainingAmount;

    -- 2) השינוי עצמו
    IF EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID('dbo.CustomerDebt')
                 AND name = 'PaidAmount' AND is_nullable = 1)
        ALTER TABLE dbo.CustomerDebt ALTER COLUMN PaidAmount decimal(10,2) NOT NULL;
END
GO
-- 3) יצירה מחדש של העמודה המחושבת (עם ISNULL — תיקון "החוב הנעלם")
IF NOT EXISTS (SELECT 1 FROM sys.computed_columns
               WHERE object_id = OBJECT_ID('dbo.CustomerDebt') AND name = 'clc_RemainingAmount')
    ALTER TABLE dbo.CustomerDebt
        ADD clc_RemainingAmount AS (DebtAmount - ISNULL(PaidAmount,0)) PERSISTED;
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_CustomerDebt_Amounts')
    ALTER TABLE dbo.CustomerDebt WITH NOCHECK
        ADD CONSTRAINT CK_CustomerDebt_Amounts CHECK (DebtAmount >= 0 AND PaidAmount >= 0);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_CustomerDebt_enmDebtStatus' AND object_id=OBJECT_ID('dbo.CustomerDebt'))
    CREATE NONCLUSTERED INDEX IX_CustomerDebt_enmDebtStatus
        ON dbo.CustomerDebt (enmDebtStatus) INCLUDE (CustomerID, DebtAmount, PaidAmount)
        WHERE DeletedOn IS NULL;
GO

/* ───────────────────────────────────────────────────────────────────────────
   7. Customer — שדות שהיו באקסל וחסרו: מספר לקוח בריווחית + ברירת מחדל
      לצורת משלוח, והרחבת טלפון (באקסל יש שני מספרים בשדה).
      ⚠️ אחרי סעיף זה יש להריץ TargCC מחדש כדי לחשוף את השדות ב-UI.
   ─────────────────────────────────────────────────────────────────────────── */
IF COL_LENGTH('dbo.Customer','RivhitCustomerNo') IS NULL
    ALTER TABLE dbo.Customer ADD RivhitCustomerNo int NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='UQ_Customer_RivhitCustomerNo' AND object_id=OBJECT_ID('dbo.Customer'))
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Customer_RivhitCustomerNo
        ON dbo.Customer (RivhitCustomerNo)
        WHERE RivhitCustomerNo IS NOT NULL AND DeletedOn IS NULL;
GO
IF COL_LENGTH('dbo.Customer','enmDefaultDeliveryMethod') IS NULL
    ALTER TABLE dbo.Customer ADD enmDefaultDeliveryMethod nvarchar(50) NULL;
GO
-- טלפון: 20 תווים לא מספיקים ("050.../04..." באקסל)
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id=OBJECT_ID('dbo.Customer') AND name='Phone' AND max_length = 40) -- nvarchar(20)=40 bytes
    ALTER TABLE dbo.Customer ALTER COLUMN Phone nvarchar(50) NULL;
GO

/* ───────────────────────────────────────────────────────────────────────────
   8. OrderLine — אילוצי שלמות + ייחודיות מספר שורה בהזמנה
   ─────────────────────────────────────────────────────────────────────────── */
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_OrderLine_Quantity')
    ALTER TABLE dbo.OrderLine WITH NOCHECK
        ADD CONSTRAINT CK_OrderLine_Quantity CHECK (Quantity > 0);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_OrderLine_UnitPrice')
    ALTER TABLE dbo.OrderLine WITH NOCHECK
        ADD CONSTRAINT CK_OrderLine_UnitPrice CHECK (UnitPrice >= 0);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_OrderLine_Discount')
    ALTER TABLE dbo.OrderLine WITH NOCHECK
        ADD CONSTRAINT CK_OrderLine_Discount CHECK (DiscountPercent IS NULL OR (DiscountPercent >= 0 AND DiscountPercent <= 100));
GO
-- מספור שורות כפול באותה הזמנה: קודם מנרמלים, ואז אוכפים ייחודיות
;WITH Dups AS (
    SELECT ID, ROW_NUMBER() OVER (PARTITION BY OrderHeaderID ORDER BY LineNumber, ID) AS rn
    FROM dbo.OrderLine WHERE DeletedOn IS NULL
)
UPDATE ol SET LineNumber = d.rn
FROM dbo.OrderLine ol INNER JOIN Dups d ON d.ID = ol.ID
WHERE ol.LineNumber <> d.rn;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='UQ_OrderLine_Header_LineNo' AND object_id=OBJECT_ID('dbo.OrderLine'))
    CREATE UNIQUE NONCLUSTERED INDEX UQ_OrderLine_Header_LineNo
        ON dbo.OrderLine (OrderHeaderID, LineNumber)
        WHERE DeletedOn IS NULL;
GO

/* ───────────────────────────────────────────────────────────────────────────
   9. ProductPrice — מניעת מחירים כפולים לאותו (מוצר, סוג לקוח)
      הישן: אינדקס לא-ייחודי איפשר שתי שורות סותרות → מחיר לא דטרמיניסטי.
   ─────────────────────────────────────────────────────────────────────────── */
-- מסמנים כפילויות ישנות כמחוקות (משאירים את העדכנית ביותר)
;WITH Dups AS (
    SELECT ID, ROW_NUMBER() OVER (PARTITION BY ProductID, enmCustomerType
                                  ORDER BY ISNULL(ChangedOn, AddedOn) DESC, ID DESC) AS rn
    FROM dbo.ProductPrice WHERE DeletedOn IS NULL
)
UPDATE pp SET DeletedOn = GETDATE(), DeletedBy = N'Migration-Dedup'
FROM dbo.ProductPrice pp INNER JOIN Dups d ON d.ID = pp.ID
WHERE d.rn > 1;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='UQ_ProductPrice_Product_CustomerType' AND object_id=OBJECT_ID('dbo.ProductPrice'))
    CREATE UNIQUE NONCLUSTERED INDEX UQ_ProductPrice_Product_CustomerType
        ON dbo.ProductPrice (ProductID, enmCustomerType)
        WHERE DeletedOn IS NULL;
GO
-- אינדקס מיותר (כפול לאינדקס המורכב)
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_ProductPrice_ProductID' AND object_id=OBJECT_ID('dbo.ProductPrice'))
    DROP INDEX IX_ProductPrice_ProductID ON dbo.ProductPrice;
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_ProductPrice_Price')
    ALTER TABLE dbo.ProductPrice WITH NOCHECK
        ADD CONSTRAINT CK_ProductPrice_Price CHECK (SellingPrice >= 0);
GO

/* ───────────────────────────────────────────────────────────────────────────
   10. ProductPriceHist — חיבור המנגנון המנותק:
       עמודת placeholder שנשכחה, אין FK, אין אינדקסים, ואף אחד לא כותב לטבלה.
       מוסיפים טריגר ארכוב אוטומטי על שינוי מחיר.
   ─────────────────────────────────────────────────────────────────────────── */
IF COL_LENGTH('dbo.ProductPriceHist','AddFieldsHere') IS NOT NULL
    ALTER TABLE dbo.ProductPriceHist DROP COLUMN AddFieldsHere;
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_ProductPriceHist_Product')
    ALTER TABLE dbo.ProductPriceHist WITH NOCHECK
        ADD CONSTRAINT FK_ProductPriceHist_Product FOREIGN KEY (ProductID) REFERENCES dbo.Product(ID);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_ProductPriceHist_Product' AND object_id=OBJECT_ID('dbo.ProductPriceHist'))
    CREATE NONCLUSTERED INDEX IX_ProductPriceHist_Product
        ON dbo.ProductPriceHist (ProductID, enmCustomerType, ValidFrom);
GO
CREATE OR ALTER TRIGGER dbo.trg_ProductPrice_ArchiveOnChange
ON dbo.ProductPrice
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.ProductPriceHist
        (TableName_ProductPriceHist, ProductID, enmCustomerType, BaseCost, SellingPrice,
         MinQuantity, DiscountPercent, ValidFrom, ValidTo, ArchivedDate, ArchivedReason,
         OriginalPriceID, AddedBy, AddedOn)
    SELECT
        0, d.ProductID, d.enmCustomerType,
        ISNULL(p.BaseCost,0), d.SellingPrice, d.MinQuantity, d.DiscountPercent,
        CAST(ISNULL(d.ChangedOn, d.AddedOn) AS date), CAST(GETDATE() AS date),
        GETDATE(), N'שינוי מחיר', d.ID,
        ISNULL(i.ChangedBy, N'System'), GETDATE()
    FROM deleted d
    INNER JOIN inserted i ON i.ID = d.ID
    LEFT  JOIN dbo.Product p ON p.ID = d.ProductID
    WHERE ISNULL(d.SellingPrice,0)    <> ISNULL(i.SellingPrice,0)
       OR ISNULL(d.DiscountPercent,0) <> ISNULL(i.DiscountPercent,0)
       OR ISNULL(d.MinQuantity,0)     <> ISNULL(i.MinQuantity,0);
END
GO

/* ───────────────────────────────────────────────────────────────────────────
   11. SupplierOrder — הזמנת ספק לא חייבת להיות צמודה להזמנת לקוח אחת
       (בפועל שולחים לביובי מייל מרוכז) + תחזוקת blg_TotalCost.
   ─────────────────────────────────────────────────────────────────────────── */
-- שינוי nullability מחייב הסרה זמנית של האינדקס על העמודה
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id=OBJECT_ID('dbo.SupplierOrder') AND name='OrderHeaderID' AND is_nullable=0)
BEGIN
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SupplierOrder_OrderHeaderID' AND object_id=OBJECT_ID('dbo.SupplierOrder'))
        DROP INDEX IX_SupplierOrder_OrderHeaderID ON dbo.SupplierOrder;

    ALTER TABLE dbo.SupplierOrder ALTER COLUMN OrderHeaderID bigint NULL;
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SupplierOrder_OrderHeaderID' AND object_id=OBJECT_ID('dbo.SupplierOrder'))
    CREATE NONCLUSTERED INDEX IX_SupplierOrder_OrderHeaderID ON dbo.SupplierOrder (OrderHeaderID);
GO
CREATE OR ALTER TRIGGER dbo.trg_SupplierOrderLine_RecalcTotal
ON dbo.SupplierOrderLine
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    ;WITH Affected AS (
        SELECT SupplierOrderID FROM inserted
        UNION
        SELECT SupplierOrderID FROM deleted
    )
    UPDATE so SET so.blg_TotalCost =
        (SELECT ISNULL(SUM(sol.clc_LineCost),0)
         FROM dbo.SupplierOrderLine sol
         WHERE sol.SupplierOrderID = so.ID AND sol.DeletedOn IS NULL)
    FROM dbo.SupplierOrder so
    INNER JOIN Affected a ON a.SupplierOrderID = so.ID;
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SupplierOrderLine_ProductID' AND object_id=OBJECT_ID('dbo.SupplierOrderLine'))
    CREATE NONCLUSTERED INDEX IX_SupplierOrderLine_ProductID ON dbo.SupplierOrderLine (ProductID);
GO

/* ───────────────────────────────────────────────────────────────────────────
   12. BeehiveBuyerTracking — רשומת מעקב אחת ללקוח + תוקף חודש תזכורת
   ─────────────────────────────────────────────────────────────────────────── */
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_Beehive_ReminderMonth')
    ALTER TABLE dbo.BeehiveBuyerTracking WITH NOCHECK
        ADD CONSTRAINT CK_Beehive_ReminderMonth CHECK (ReminderMonth IS NULL OR (ReminderMonth BETWEEN 1 AND 12));
GO
;WITH Dups AS (
    SELECT ID, ROW_NUMBER() OVER (PARTITION BY CustomerID ORDER BY ISNULL(ChangedOn,AddedOn) DESC, ID DESC) AS rn
    FROM dbo.BeehiveBuyerTracking WHERE DeletedOn IS NULL
)
UPDATE b SET DeletedOn = GETDATE(), DeletedBy = N'Migration-Dedup'
FROM dbo.BeehiveBuyerTracking b INNER JOIN Dups d ON d.ID = b.ID WHERE d.rn > 1;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='UQ_Beehive_Customer' AND object_id=OBJECT_ID('dbo.BeehiveBuyerTracking'))
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Beehive_Customer
        ON dbo.BeehiveBuyerTracking (CustomerID) WHERE DeletedOn IS NULL;
GO

/* ───────────────────────────────────────────────────────────────────────────
   13. אינדקסים חסרים למסלולי שאילתות ידועים
   ─────────────────────────────────────────────────────────────────────────── */
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_OrderHeader_DeliveryDate' AND object_id=OBJECT_ID('dbo.OrderHeader'))
    CREATE NONCLUSTERED INDEX IX_OrderHeader_DeliveryDate
        ON dbo.OrderHeader (DeliveryDate)
        WHERE DeletedOn IS NULL AND DeliveryDate IS NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_OrderHeader_InvoiceNumber' AND object_id=OBJECT_ID('dbo.OrderHeader'))
    CREATE NONCLUSTERED INDEX IX_OrderHeader_InvoiceNumber
        ON dbo.OrderHeader (InvoiceNumber)
        WHERE DeletedOn IS NULL AND InvoiceNumber IS NOT NULL;
GO

/* ───────────────────────────────────────────────────────────────────────────
   14. הגדרות עסקיות ב-c_SystemDefault (Group='Business') —
       מחליף את הגיליון "הגדרות" באקסל. נקרא ע"י ה-WebAPI.
   ─────────────────────────────────────────────────────────────────────────── */
MERGE dbo.c_SystemDefault AS t
USING (VALUES
    ('Business','VATRatePercent',      N'18',                  'Business VAT rate %'),
    ('Business','DebtAmountThreshold', N'100',                 'Minimum debt amount (ILS) to flag'),
    ('Business','DebtOverdueDays',     N'10',                  'Days after delivery before debt is flagged'),
    ('Business','SupplierEmailBiobee', N'doritc@biobee.com',   'Biobee supplier order email')
) AS s ([Group], SettingName, SettingValue, Descr)
ON  t.[Group] = s.[Group] AND t.SettingName = s.SettingName
WHEN NOT MATCHED THEN
    INSERT (TableName_c_SystemDefault, [Group], SettingName, spt_SettingValue,
            enmSystemDefaultType, AddedBy, AddedOn, [Description])
    VALUES (0, s.[Group], s.SettingName, s.SettingValue, 'String', N'Migration', GETDATE(), s.Descr);
GO

/* ───────────────────────────────────────────────────────────────────────────
   15. מנוע חובות — סימון אוטומטי של "חוב לטיפול" לפי כללי האקסל:
       סכום > סף  וגם  עברו X ימים מהמשלוח/מהחוב.
       להריץ יומית (SQL Agent Job או TaskManager).
   ─────────────────────────────────────────────────────────────────────────── */
CREATE OR ALTER PROCEDURE dbo.sp_UpdateDebtAttention
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @amountThreshold decimal(10,2) =
        TRY_CAST((SELECT spt_SettingValue FROM dbo.c_SystemDefault
                  WHERE [Group]='Business' AND SettingName='DebtAmountThreshold' AND DeletedOn IS NULL) AS decimal(10,2));
    DECLARE @overdueDays int =
        TRY_CAST((SELECT spt_SettingValue FROM dbo.c_SystemDefault
                  WHERE [Group]='Business' AND SettingName='DebtOverdueDays' AND DeletedOn IS NULL) AS int);

    SET @amountThreshold = ISNULL(@amountThreshold, 100);
    SET @overdueDays     = ISNULL(@overdueDays, 10);

    UPDATE cd SET blg_NeedsAttention =
        CASE WHEN cd.clc_RemainingAmount > @amountThreshold
              AND DATEDIFF(day, ISNULL(cd.DeliveryDate, cd.DebtDate), GETDATE()) > @overdueDays
              AND ISNULL(cd.enmDebtStatus,'Open') NOT IN ('Paid','Cancelled')
             THEN 1 ELSE 0 END
    FROM dbo.CustomerDebt cd
    WHERE cd.DeletedOn IS NULL
      AND cd.blg_NeedsAttention <>
        CASE WHEN cd.clc_RemainingAmount > @amountThreshold
              AND DATEDIFF(day, ISNULL(cd.DeliveryDate, cd.DebtDate), GETDATE()) > @overdueDays
              AND ISNULL(cd.enmDebtStatus,'Open') NOT IN ('Paid','Cancelled')
             THEN 1 ELSE 0 END;
END
GO
EXEC dbo.sp_UpdateDebtAttention;
GO

/* ───────────────────────────────────────────────────────────────────────────
   16. רענון מעקב קוני כוורות — LastOrderDate וכמות מתוך ההזמנות בפועל
   ─────────────────────────────────────────────────────────────────────────── */
CREATE OR ALTER PROCEDURE dbo.sp_RefreshBeehiveTracking
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH BeehiveOrders AS (
        SELECT oh.CustomerID,
               MAX(oh.OrderDate)  AS LastOrderDate,
               SUM(ol.Quantity)   AS TotalQty
        FROM dbo.OrderHeader oh
        INNER JOIN dbo.OrderLine ol ON ol.OrderHeaderID = oh.ID AND ol.DeletedOn IS NULL
        INNER JOIN dbo.Product p    ON p.ID = ol.ProductID AND p.enmCategory = 'Beehives'
        WHERE oh.DeletedOn IS NULL
        GROUP BY oh.CustomerID
    )
    MERGE dbo.BeehiveBuyerTracking AS t
    USING BeehiveOrders AS s ON t.CustomerID = s.CustomerID AND t.DeletedOn IS NULL
    WHEN MATCHED AND (ISNULL(t.LastOrderDate,'1900-01-01') <> CAST(s.LastOrderDate AS date)
                   OR ISNULL(t.BeehiveQuantity,0) <> s.TotalQty) THEN
        UPDATE SET t.LastOrderDate = CAST(s.LastOrderDate AS date),
                   t.BeehiveQuantity = s.TotalQty,
                   t.ChangedBy = N'System', t.ChangedOn = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (TableName_BeehiveBuyerTracking, CustomerID, LastOrderDate, BeehiveQuantity,
                blg_IsRelevant, AddedBy, AddedOn)
        VALUES (N'', s.CustomerID, CAST(s.LastOrderDate AS date), s.TotalQty, 1, N'System', GETDATE());
END
GO
EXEC dbo.sp_RefreshBeehiveTracking;
GO

/* ───────────────────────────────────────────────────────────────────────────
   17. תיקוני Views — דליפת רשומות מחוקות ודיוק חישוב רווחיות
   ─────────────────────────────────────────────────────────────────────────── */
IF OBJECT_ID('dbo.vwProductReport','V') IS NOT NULL
BEGIN
    EXEC sp_executesql N'
    ALTER VIEW dbo.vwProductReport AS
    SELECT
        p.ID              AS ProductID,
        p.ProductCode,
        p.ProductName,
        p.enmCategory,
        COUNT(DISTINCT oh.ID)                    AS OrdersCount,
        ISNULL(SUM(ol.Quantity),0)               AS TotalQuantity,
        ISNULL(SUM(ol.clc_LineTotal),0)          AS TotalRevenue,
        ISNULL(SUM(ol.clc_TotalCost),0)          AS TotalCost,
        ISNULL(SUM(ol.clc_Profit),0)             AS TotalProfit
    FROM dbo.Product p
    LEFT JOIN dbo.OrderLine ol
           ON ol.ProductID = p.ID AND ol.DeletedOn IS NULL
    LEFT JOIN dbo.OrderHeader oh
           ON oh.ID = ol.OrderHeaderID AND oh.DeletedOn IS NULL
    WHERE p.DeletedOn IS NULL
      AND (ol.ID IS NULL OR oh.ID IS NOT NULL)   -- אל תספור שורות של הזמנות מחוקות
    GROUP BY p.ID, p.ProductCode, p.ProductName, p.enmCategory;';
END
GO

/* ═══════════════════════════════════════════════════════════════════════════
   סיום. בדיקות מומלצות אחרי הרצה:
     SELECT TOP 20 OrderNumber, clc_TotalAmount, clc_VATAmount, clc_TotalWithVAT
     FROM OrderHeader ORDER BY ID DESC;                     -- סכומים לא אפס
     SELECT COUNT(*) FROM CustomerDebt WHERE blg_NeedsAttention = 1;
     SELECT * FROM c_SystemDefault WHERE [Group]='Business';

   מומלץ לתזמן יומית (SQL Agent):
     EXEC dbo.sp_UpdateDebtAttention;
     EXEC dbo.sp_RefreshBeehiveTracking;
   ═══════════════════════════════════════════════════════════════════════════ */
