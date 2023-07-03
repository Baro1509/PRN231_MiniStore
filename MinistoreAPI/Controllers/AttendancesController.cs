using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers
{
    public class AttendancesController : ODataController
    {
        private readonly IStaffRepository _staffRepo;
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly IDutyRepository _dutyRepo;

        public AttendancesController(IStaffRepository staffRepo, IAttendanceRepository attendanceRepo, IDutyRepository dutyRepo)
        {
            _staffRepo = staffRepo;
            _attendanceRepo = attendanceRepo;
            _dutyRepo = dutyRepo;
        }

        [HttpGet("CheckIn")]
        public IActionResult CheckIn([FromODataUri] string id)
        {
            var staff = _staffRepo.GetStaff(id);
            if (staff != null)
            {
                _attendanceRepo.CreateCheckIn(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("CheckOut")]
        public IActionResult CheckOut([FromODataUri] string id)
        {
            var staff = _staffRepo.GetStaff(id);
            if (staff != null)
            {
                bool res = _attendanceRepo.CreateCheckOut(id);
                if (res)
                {
                    _dutyRepo.UpdateLatestDutyStatus(id);
                }
                return Ok();
            }
            return NotFound();
        }
    }
}
