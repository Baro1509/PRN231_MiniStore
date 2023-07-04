using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MinistoreAPI.Request;
using Repository;

namespace MinistoreAPI.Controllers
{
	public class MonthSalariesController : ODataController
	{
		private readonly IMonthSalaryRepository _monthSalaryRepo;

		public MonthSalariesController(IMonthSalaryRepository monthSalaryRepo)
		{
			_monthSalaryRepo = monthSalaryRepo;
		}
		public IActionResult Post([FromBody] MonthSalaryRequest request)
		{
			return _monthSalaryRepo.Create(request.StaffId, request.ManagerId, request.Month, request.Year) ? Ok() : BadRequest();
		}
		[EnableQuery]
		public IActionResult Get([FromODataUri] string staffId)
		{
			return Ok(_monthSalaryRepo.Get(staffId));
		}
	}
}
