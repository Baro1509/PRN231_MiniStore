using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers
{
    public class WorkShiftsController : ODataController
    {
        private readonly IWorkShiftRepository _workShiftRepo;

        public WorkShiftsController(IWorkShiftRepository workShiftRepo)
        {
            _workShiftRepo = workShiftRepo;
        }
        public IActionResult Get([FromODataUri] int year, [FromODataUri] int month, [FromODataUri] int date)
        {
            return Ok(_workShiftRepo.GetAllByStartDate(new DateOnly(year, month, date)));
        }
        public IActionResult Post([FromODataUri] int year, [FromODataUri] int month, [FromODataUri] int date, [FromODataUri] string managerId)
        {
            _workShiftRepo.CreateDefaultWorkShiftsForDate(new DateOnly(year, month, date), managerId);
            return Ok();
        }
    }
}
