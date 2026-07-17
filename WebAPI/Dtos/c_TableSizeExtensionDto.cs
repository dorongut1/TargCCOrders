using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class TableSizeExtensions 
    { 
        public static TableSizeDto ToDto(this csTableSize tableSize) 
        { 
            if (tableSize is null) return null!; 
 
            var dto = new TableSizeDto 
            { 
                Id = tableSize.ID, 
                TableName = tableSize.TableName, 
                NumberOfRows = tableSize.NumberOfRows, 
                ReservedSizeKb = tableSize.ReservedSizeKb, 
                DataSizeKb = tableSize.DataSizeKb, 
                IndexSizeKb = tableSize.IndexSizeKb, 
                UnusedSizeKb = tableSize.UnusedSizeKb
            }; 
            dto._etag = ComputeETag(tableSize); 
            return dto; 
        } 
 
        public static string ComputeETag(csTableSize entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.TableName ?? ""); 
            sb.Append('|').Append(entity.NumberOfRows); 
            sb.Append('|').Append(entity.ReservedSizeKb); 
            sb.Append('|').Append(entity.DataSizeKb); 
            sb.Append('|').Append(entity.IndexSizeKb); 
            sb.Append('|').Append(entity.UnusedSizeKb); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csTableSize FromDto(this TableSizeUpdateDto tableSizeDto, clsRequester requester) 
        { 
            if (tableSizeDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csTableSize tableSize = new csTableSize(); 
            if (tableSizeDto.Id > 0) 
            { 
                clsFault fault = tableSize.GetByID(tableSizeDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //tableSize.ID = tableSizeDto.Id; //not transferred on purpose ! 
            tableSize.TableName = tableSizeDto.TableName; 
            tableSize.NumberOfRows = tableSizeDto.NumberOfRows; 
            tableSize.ReservedSizeKb = tableSizeDto.ReservedSizeKb; 
            tableSize.DataSizeKb = tableSizeDto.DataSizeKb; 
            tableSize.IndexSizeKb = tableSizeDto.IndexSizeKb; 
            tableSize.UnusedSizeKb = tableSizeDto.UnusedSizeKb; 
 
            return tableSize; 
        } 
    } 
} 
