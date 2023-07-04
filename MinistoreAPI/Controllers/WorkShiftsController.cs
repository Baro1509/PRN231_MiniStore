using DataAccess.Models;
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
        public IActionResult Post([FromODataUri] int year, [FromODataUri] int month, [FromODataUri] int date, [FromODataUri] string managerId, [FromODataUri] int shiftOrderId)
        {
            _workShiftRepo.CreateDefaultWorkShift(new DateOnly(year, month, date), managerId, shiftOrderId);
            return Ok();
        }
        public IActionResult Put([FromBody] WorkShift workShift)
        {
            return _workShiftRepo.Update(workShift) ? Ok() : NotFound();
        }
        public IActionResult Delete([FromODataUri] int workShift)
        {
            return _workShiftRepo.DeleteWorkShift(workShift) ? Ok() : NotFound();
        }
    }
}
