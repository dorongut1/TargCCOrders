using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Cryptography; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace TargCCOrders.WebAPI.Dtos 
{ 
    public static class PermissionExtensions 
    { 
        public static PermissionDto ToDto(this csPermission permission) 
        { 
            if (permission is null) return null!; 
 
            var dto = new PermissionDto 
            { 
                Id = permission.ID, 
                FkProcessId = permission.ProcessID, 
                FkRoleId = permission.RoleID, 
                CanDo = permission.CanDo
            }; 
            dto._etag = ComputeETag(permission); 
            return dto; 
        } 
 
        public static string ComputeETag(csPermission entity) 
        { 
            var sb = new StringBuilder(); 
            sb.Append(entity.ID); 
            sb.Append('|').Append(entity.ProcessID); 
            sb.Append('|').Append(entity.RoleID); 
            sb.Append('|').Append(entity.CanDo); 
            using (var md5 = MD5.Create()) 
            { 
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())); 
                return Convert.ToBase64String(hash); 
            } 
        } 
 
        public static csPermission FromDto(this PermissionUpdateDto permissionDto, clsRequester requester) 
        { 
            if (permissionDto is null) return null!; 
            if (requester is null) return null!; 
 
            //get the real item 
            csPermission permission = new csPermission(); 
            if (permissionDto.Id > 0) 
            { 
                clsFault fault = permission.GetByID(permissionDto.Id, requester, vMustExist: true); 
                if (!fault.isOK) throw new Exception(fault.Message); 
            } 
 
 
            //Now transfer the data 
            //permission.ID = permissionDto.Id; //not transferred on purpose ! 
            permission.ProcessID = permissionDto.FkProcessId; 
            permission.RoleID = permissionDto.FkRoleId; 
            permission.CanDo = permissionDto.CanDo; 
 
            return permission; 
        } 
 
        public static void PopulateFKDisplayNames(this PermissionDto dto, clsRequester requester) 
        { 
            if (dto == null || requester == null) return; 
 
            if (dto.FkProcessId > 0) 
            { 
                try 
                { 
                    var pProcess = new csProcess(); 
                    var fault = pProcess.GetByID(dto.FkProcessId, requester); 
                    if (fault.isOK) dto.ProcessDisplayName = pProcess.Name; 
                } 
                catch { } 
            } 
            if (dto.FkRoleId > 0) 
            { 
                try 
                { 
                    var pRole = new csRole(); 
                    var fault = pRole.GetByID(dto.FkRoleId, requester); 
                    if (fault.isOK) dto.RoleDisplayName = pRole.Name; 
                } 
                catch { } 
            } 
        } 
    } 
} 
