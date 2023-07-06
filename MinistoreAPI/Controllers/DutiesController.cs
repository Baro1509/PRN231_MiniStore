using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers
{
	[Authorize]
	public class DutiesController : ODataController
	{
		private readonly IDutyRepository _dutyRepo;

		public DutiesController(IDutyRepository dutyRepo)
		{
			_dutyRepo = dutyRepo;
		}
		[EnableQuery]

		public IActionResult Get([FromODataUri] int year, [FromODataUri] int month, [FromODataUri] int date, [FromODataUri] string staffId)
		{
			var res = _dutyRepo.GetStaffWeekDuties(staffId, new DateOnly(year, month, date));
			if (res == null)
			{
				return NotFound();
			}
			return Ok(res);
		}

		public IActionResult Post([FromBody] Duty duty)
		{
			return _dutyRepo.CreateDuty(duty.AssignedTo, duty.ShiftId) ? Ok() : NotFound();
		}
		[HttpPost("odata/Duties/Update")]
		public IActionResult PostUpdate([FromBody] Duty duty)
		{
			return _dutyRepo.UpdateDuty(duty.DutyId, duty.Status) ? Ok() : NotFound();
		}
		[HttpDelete("odata/Duties/{dutyId:int}")]
		public IActionResult Delete([FromRoute] int dutyId)
		{
			return _dutyRepo.DeleteDuty(dutyId) ? Ok() : NotFound();
		}
		[HttpPut("odata/Duties")]
		public IActionResult UpdateDutyStatus()
		{
			_dutyRepo.UpdateAllDutyStatus();
			return Ok();
		}
	}
}
