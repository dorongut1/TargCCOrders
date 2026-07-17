using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class IndexFragmentationExtensions 
    { 
        public static IndexFragmentationDto ToDto(this csIndexFragmentation indexFragmentation) 
        { 
            if (indexFragmentation is null) return null!; 
 
            var dto = new IndexFragmentationDto 
            { 
                Id = indexFragmentation.ID, 
                TableName = indexFragmentation.TableName, 
                IndexName = indexFragmentation.IndexName, 
                IndexType = indexFragmentation.IndexType, 
                FragmentationPct = indexFragmentation.FragmentationPct, 
                PageCount = indexFragmentation.PageCount
            }; 
            dto._etag = ComputeETag(indexFragmentation); 
            return dto; 
        } 
 
        public static string ComputeETag(csIndexFragmentation entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.TableName ?? ""); 
            sb.Append('|').Append(entity.IndexName ?? ""); 
            sb.Append('|').Append(entity.IndexType ?? ""); 
            sb.Append('|').Append(entity.FragmentationPct); 
            sb.Append('|').Append(entity.PageCount); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csIndexFragmentation FromDto(this IndexFragmentationUpdateDto indexFragmentationDto, clsRequester requester) 
        { 
            if (indexFragmentationDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csIndexFragmentation indexFragmentation = new csIndexFragmentation(); 
            if (indexFragmentationDto.Id > 0) 
            { 
                clsFault fault = indexFragmentation.GetByID(indexFragmentationDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //indexFragmentation.ID = indexFragmentationDto.Id; //not transferred on purpose ! 
            indexFragmentation.TableName = indexFragmentationDto.TableName; 
            indexFragmentation.IndexName = indexFragmentationDto.IndexName; 
            indexFragmentation.IndexType = indexFragmentationDto.IndexType; 
            indexFragmentation.FragmentationPct = indexFragmentationDto.FragmentationPct; 
            indexFragmentation.PageCount = indexFragmentationDto.PageCount; 
 
            return indexFragmentation; 
        } 
    } 
} 
