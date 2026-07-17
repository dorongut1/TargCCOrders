using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class ProcessExtensions 
    { 
        public static ProcessDto ToDto(this csProcess process) 
        { 
            if (process is null) return null!; 
 
            var dto = new ProcessDto 
            { 
                Id = process.ID, 
                Name = process.Name, 
                DateChecked = process.DateChecked
            }; 
            dto._etag = ComputeETag(process); 
            return dto; 
        } 
 
        public static string ComputeETag(csProcess entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Name ?? ""); 
            sb.Append('|').Append(entity.DateChecked.Ticks); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csProcess FromDto(this ProcessUpdateDto processDto, clsRequester requester) 
        { 
            if (processDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csProcess process = new csProcess(); 
            if (processDto.Id > 0) 
            { 
                clsFault fault = process.GetByID(processDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //process.ID = processDto.Id; //not transferred on purpose ! 
            process.Name = processDto.Name; 
            process.DateChecked = processDto.DateChecked; 
 
            return process; 
        } 
    } 
} 
