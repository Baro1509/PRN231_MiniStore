using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;


namespace MinistoreAPI.Controllers
{
    public class StaffsController : ODataController
    {
        private readonly IStaffRepository _staffRepo;

        public StaffsController(IStaffRepository staffRepo)
        {
            _staffRepo = staffRepo;

        }
        public IActionResult Post([FromBody] Staff staff)
        {
            return _staffRepo.Create(staff) ? Ok() : NotFound();
        }
        [HttpDelete("odata/Staffs")]
        public IActionResult Delete([FromODataUri] string staffId)
        {
            return _staffRepo.Delete(staffId) ? Ok() : NotFound();
        }
        [HttpPut("odata/Staffs")]
        public IActionResult Put([FromODataUri] string id, [FromBody] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest();
            }
            return _staffRepo.Update(staff) ? Ok() : NotFound();
        }

    }
}
