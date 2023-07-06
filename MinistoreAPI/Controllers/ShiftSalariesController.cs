using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MinistoreAPI.Request;
using Repository;

namespace MinistoreAPI.Controllers
{
	[Authorize]
	public class ShiftSalariesController : ODataController
	{
		private readonly IShiftSalaryRepository _shiftSalaryRepo;

		public ShiftSalariesController(IShiftSalaryRepository shiftSalaryRepo)
		{
			_shiftSalaryRepo = shiftSalaryRepo;
		}
		public IActionResult Get([FromODataUri] string staffId)
		{
			var res = _shiftSalaryRepo.GetCurrentShiftSalary(staffId);
			return res != null ? Ok(res) : NotFound();
		}
		public IActionResult Post([FromBody] ShiftSalaryRequest request)
		{
			ShiftSalary shiftSalary = new ShiftSalary();
			shiftSalary.ShiftSalaryId = request.ShiftSalaryId;
			shiftSalary.AssignedTo = request.AssignedTo;
			shiftSalary.ApprovedBy = request.ApprovedBy;
			shiftSalary.Salary = request.Salary;
			if (request.StartTime == null)
			{
				shiftSalary.CreatedTime = DateTime.Now;
			}
			return _shiftSalaryRepo.Update(shiftSalary) ? Ok() : BadRequest();
		}

		public IActionResult Delete([FromODataUri] int shiftSalaryId)
		{
			return _shiftSalaryRepo.Delete(shiftSalaryId) ? Ok() : BadRequest();
		}
	}
}
