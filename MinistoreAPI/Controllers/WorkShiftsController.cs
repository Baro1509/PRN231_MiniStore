using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MinistoreAPI.Request;
using Repository;

namespace MinistoreAPI.Controllers
{
	[Authorize]
	public class WorkShiftsController : ODataController
	{
		private readonly IWorkShiftRepository _workShiftRepo;

		public WorkShiftsController(IWorkShiftRepository workShiftRepo)
		{
			_workShiftRepo = workShiftRepo;
		}
		[EnableQuery]
		public IActionResult Get([FromODataUri] int year, [FromODataUri] int month, [FromODataUri] int date)
		{
			return Ok(_workShiftRepo.GetAllByStartDate(new DateOnly(year, month, date)));
		}
		public IActionResult Post([FromBody] WorkShiftRequest request)
		{
			if (request.ShiftOrder == 0)
			{
				_workShiftRepo.CreateDefaultWorkShiftsForDate(DateOnly.FromDateTime(request.Date), request.ManagerId);
			}
			else
			{
				_workShiftRepo.CreateDefaultWorkShift(DateOnly.FromDateTime(request.Date), request.ManagerId, request.ShiftOrder);
			}
			return Ok();
		}
		[HttpPut("odata/WorkShifts/{id:int}")]
		public IActionResult Put([FromRoute] int id, [FromBody] WorkShift workShift)
		{
			if (id != workShift.ShiftId)
			{
				return BadRequest();
			}
			return _workShiftRepo.Update(workShift) ? Ok() : NotFound();
		}
		[HttpDelete("odata/WorkShifts/{workShift:int}")]
		public IActionResult Delete([FromRoute] int workShift)
		{
			return _workShiftRepo.DeleteWorkShift(workShift) ? Ok() : NotFound();
		}
	}
}
