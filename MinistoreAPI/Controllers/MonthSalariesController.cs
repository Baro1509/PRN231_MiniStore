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
	public class MonthSalariesController : ODataController
	{
		private readonly IMonthSalaryRepository _monthSalaryRepo;
		private readonly IStaffRepository _staffRepository;

		public MonthSalariesController(IMonthSalaryRepository monthSalaryRepo, IStaffRepository staffRepository)
		{
			_monthSalaryRepo = monthSalaryRepo;
			_staffRepository = staffRepository;
		}
		public IActionResult Post([FromBody] MonthSalaryRequest request)
		{
			return _monthSalaryRepo.Create(request.StaffId, request.ManagerId, request.Month, request.Year) ? Ok() : BadRequest();
		}
		[HttpGet("odata/MonthSalaries/Calculate")]
		public IActionResult CalculateMonthSalary([FromODataUri] int month, [FromODataUri] int year)
		{
			string managerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "StaffId")?.Value;
			var staff = _staffRepository.GetAll().Where(s => (byte)Status.Available == s.Status && !s.RoleId.StartsWith("M")).ToList();
			foreach (var s in staff)
			{
				_monthSalaryRepo.Create(s.StaffId, managerId, month, year);
			}
			return Ok();
		}
		[HttpGet("odata/MonthSalaries/Staff")]
		public IActionResult GetByStaff([FromODataUri] string staffId)
		{
			return Ok(_monthSalaryRepo.Get(staffId));
		}
		[EnableQuery]
		[HttpGet("odata/MonthSalaries")]
		public IActionResult GetAll()
		{
			return Ok(_monthSalaryRepo.GetAll());
		}
	}
}
