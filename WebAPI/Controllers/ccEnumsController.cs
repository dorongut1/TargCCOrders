using Microsoft.AspNetCore.Mvc; 
using TargCCOrders.DataController; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
 
namespace TargCCOrders.WebAPI.Controllers 
{ 
    [Route("api")] 
    [ApiController] 
    public class EnumsController : ControllerBase 
    { 
        // GET api/enums 
        [Route("enums")] 
        [HttpGet] 
        public ActionResult GetAllEnums() 
        { 
            var result = new List<object>(); 
            var enumsType = typeof(clsEnums); 
            var nestedTypes = enumsType.GetNestedTypes() 
                .Where(t => t.IsEnum && t.Name.StartsWith("enm")); 
 
            foreach (var enumType in nestedTypes) 
            { 
                var enumName = enumType.Name.Substring(3); // Remove "enm" prefix 
                var values = Enum.GetValues(enumType) 
                    .Cast<object>() 
                    .Select(v => new { value = (int)Convert.ChangeType(v, typeof(int)), label = v.ToString() }) 
                    .ToList(); 
 
                result.Add(new { enumType = enumName, values }); 
            } 
 
            return Ok(result); 
        } 
    } 
} 
