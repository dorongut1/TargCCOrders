using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class TableExtensions 
    { 
        public static TableDto ToDto(this csTable table) 
        { 
            if (table is null) return null!; 
 
            var dto = new TableDto 
            { 
                Id = table.ID, 
                Name = table.Name, 
                DefaultTextFields = table.DefaultTextFields, 
                UsedForIdentity = table.UsedForIdentity, 
                IsSingleRow = table.IsSingleRow, 
                CanAdd = table.CanAdd, 
                CanEdit = table.CanEdit, 
                CanDelete = table.CanDelete, 
                AuditAdd = table.AuditAdd, 
                AuditEdit = table.AuditEdit, 
                AuditDelete = table.AuditDelete, 
                TrackRowChangers = table.TrackRowChangers, 
                CreateUiMenu = table.CreateUIMenu, 
                CreateUiCollection = table.CreateUICollection, 
                CreateUiEntity = table.CreateUIEntity, 
                SortOrder = table.SortOrder
            }; 
            dto._etag = ComputeETag(table); 
            return dto; 
        } 
 
        public static string ComputeETag(csTable entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Name ?? ""); 
            sb.Append('|').Append(entity.DefaultTextFields ?? ""); 
            sb.Append('|').Append(entity.UsedForIdentity); 
            sb.Append('|').Append(entity.IsSingleRow); 
            sb.Append('|').Append(entity.CanAdd ?? ""); 
            sb.Append('|').Append(entity.CanEdit ?? ""); 
            sb.Append('|').Append(entity.CanDelete ?? ""); 
            sb.Append('|').Append(entity.AuditAdd); 
            sb.Append('|').Append(entity.AuditEdit); 
            sb.Append('|').Append(entity.AuditDelete); 
            sb.Append('|').Append(entity.TrackRowChangers); 
            sb.Append('|').Append(entity.CreateUIMenu); 
            sb.Append('|').Append(entity.CreateUICollection); 
            sb.Append('|').Append(entity.CreateUIEntity); 
            sb.Append('|').Append(entity.SortOrder); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csTable FromDto(this TableUpdateDto tableDto, clsRequester requester) 
        { 
            if (tableDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csTable table = new csTable(); 
            if (tableDto.Id > 0) 
            { 
                clsFault fault = table.GetByID(tableDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //table.ID = tableDto.Id; //not transferred on purpose ! 
            table.Name = tableDto.Name; 
            table.DefaultTextFields = tableDto.DefaultTextFields; 
            table.UsedForIdentity = tableDto.UsedForIdentity; 
            table.IsSingleRow = tableDto.IsSingleRow; 
            table.CanAdd = tableDto.CanAdd; 
            table.CanEdit = tableDto.CanEdit; 
            table.CanDelete = tableDto.CanDelete; 
            table.AuditAdd = tableDto.AuditAdd; 
            table.AuditEdit = tableDto.AuditEdit; 
            table.AuditDelete = tableDto.AuditDelete; 
            table.TrackRowChangers = tableDto.TrackRowChangers; 
            table.CreateUIMenu = tableDto.CreateUiMenu; 
            table.CreateUICollection = tableDto.CreateUiCollection; 
            table.CreateUIEntity = tableDto.CreateUiEntity; 
            table.SortOrder = tableDto.SortOrder; 
 
            return table; 
        } 
    } 
} 
