using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;


namespace MinistoreAPI.Controllers
{
    public class StaffsController : Controller
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
        [HttpDelete("odata/Staffs/{staffid:string}")]
        public IActionResult Delete([FromRoute] string staffId)
        {
            return _staffRepo.Delete(staffId) ? Ok() : NotFound();
        }
        [HttpPut("odata/Duties/{id:int}")]
        public IActionResult Put([FromRoute] string id, [FromBody] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest();
            }
            return _staffRepo.Update(staff) ? Ok() : NotFound();
        }

    }
}
