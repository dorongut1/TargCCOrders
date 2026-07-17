using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class BeehiveBuyerTrackingExtensions 
    { 
        public static BeehiveBuyerTrackingDto ToDto(this clsBeehiveBuyerTracking beehiveBuyerTracking) 
        { 
            if (beehiveBuyerTracking is null) return null!; 
 
            var dto = new BeehiveBuyerTrackingDto 
            { 
                Id = beehiveBuyerTracking.ID, 
                FkCustomerId = beehiveBuyerTracking.CustomerID, 
                LastOrderDate = beehiveBuyerTracking.LastOrderDate, 
                BeehiveQuantity = beehiveBuyerTracking.BeehiveQuantity, 
                ReminderMonth = beehiveBuyerTracking.ReminderMonth, 
                IsRelevant = beehiveBuyerTracking.IsRelevant, 
                Notes = beehiveBuyerTracking.Notes
            }; 
            dto._etag = ComputeETag(beehiveBuyerTracking); 
            return dto; 
        } 
 
        public static string ComputeETag(clsBeehiveBuyerTracking entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.CustomerID); 
            sb.Append('|').Append(entity.LastOrderDate.Ticks); 
            sb.Append('|').Append(entity.BeehiveQuantity); 
            sb.Append('|').Append(entity.ReminderMonth); 
            sb.Append('|').Append(entity.IsRelevant); 
            sb.Append('|').Append(entity.Notes ?? ""); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static clsBeehiveBuyerTracking FromDto(this BeehiveBuyerTrackingUpdateDto beehiveBuyerTrackingDto, clsRequester requester) 
        { 
            if (beehiveBuyerTrackingDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            clsBeehiveBuyerTracking beehiveBuyerTracking = new clsBeehiveBuyerTracking(); 
            if (beehiveBuyerTrackingDto.Id > 0) 
            { 
                clsFault fault = beehiveBuyerTracking.GetByID(beehiveBuyerTrackingDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //beehiveBuyerTracking.ID = beehiveBuyerTrackingDto.Id; //not transferred on purpose ! 
            beehiveBuyerTracking.CustomerID = beehiveBuyerTrackingDto.FkCustomerId; 
            beehiveBuyerTracking.LastOrderDate = beehiveBuyerTrackingDto.LastOrderDate; 
            beehiveBuyerTracking.BeehiveQuantity = beehiveBuyerTrackingDto.BeehiveQuantity; 
            beehiveBuyerTracking.ReminderMonth = beehiveBuyerTrackingDto.ReminderMonth; 
            beehiveBuyerTracking.Notes = beehiveBuyerTrackingDto.Notes; 
 
            return beehiveBuyerTracking; 
        } 
 
        public static void PopulateFKDisplayNames(this BeehiveBuyerTrackingDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkCustomerId > 0) 
            { 
                try 
                { 
                    var pCustomer = new clsCustomer(); 
                    var fault = pCustomer.GetByID(dto.FkCustomerId, requester); 
                    if (fault.isOK) dto.CustomerDisplayName = pCustomer.CustomerName; 
                } 
                catch { } 
            } 
        } 
    } 
} 
