using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;
using Simple.OData.Client;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Staff
{
	public class SalaryReportModel : PageModel
	{
		private ODataClient _odataClient;
		private HttpClient client;
		private string MonthSalaryApi = "https://localhost:7036/odata/MonthSalaries";
		private string ManagerId;

		public SalaryReportModel()
		{
			_odataClient = OdataUtils.GetODataClient();
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
		}
		public List<MonthSalary> Salaries { get; set; }
		public DateTime Date { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Date = DateTime.Now;

			if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
			{
				return RedirectToPage("/Login");
			}
			ManagerId = HttpContext.Session.GetString("Id");
			_odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
			var temp = await _odataClient.For<MonthSalary>().QueryOptions($"$filter=AssignedTo eq '{HttpContext.Session.GetString("Id")}'").Expand(d => d.AssignedToNavigation).FindEntriesAsync();
			Salaries = temp.ToList();

			return Page();
		}
	}
}
