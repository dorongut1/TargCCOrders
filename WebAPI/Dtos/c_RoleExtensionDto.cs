using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class RoleExtensions 
    { 
        public static RoleDto ToDto(this csRole role) 
        { 
            if (role is null) return null!; 
 
            var dto = new RoleDto 
            { 
                Id = role.ID, 
                Name = role.Name, 
                FkBaseRoleId = role.BaseRoleID
            }; 
            dto._etag = ComputeETag(role); 
            return dto; 
        } 
 
        public static string ComputeETag(csRole entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.Name ?? ""); 
            sb.Append('|').Append(entity.BaseRoleID); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csRole FromDto(this RoleUpdateDto roleDto, clsRequester requester) 
        { 
            if (roleDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csRole role = new csRole(); 
            if (roleDto.Id > 0) 
            { 
                clsFault fault = role.GetByID(roleDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //role.ID = roleDto.Id; //not transferred on purpose ! 
            role.Name = roleDto.Name; 
            role.BaseRoleID = roleDto.FkBaseRoleId; 
 
            return role; 
        } 
 
        public static void PopulateFKDisplayNames(this RoleDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkBaseRoleId > 0) 
            { 
                try 
                { 
                    var pRole = new csRole(); 
                    var fault = pRole.GetByID(dto.FkBaseRoleId, requester); 
                    if (fault.isOK) dto.BaseRoleDisplayName = pRole.Name; 
                } 
                catch { } 
            } 
        } 
    } 
} 
