using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;
using Simple.OData.Client;
using System.Globalization;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Manager
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
			if (!Utils.isManager(HttpContext.Session.GetString("Role")))
			{
				return RedirectToPage("/Login");
			}
			ManagerId = HttpContext.Session.GetString("Id");
			_odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
			var temp = await _odataClient.For<MonthSalary>().Expand(d => d.AssignedToNavigation).FindEntriesAsync();
			Salaries = temp.ToList();

			return Page();
		}
		public async Task<IActionResult> OnPostAsync(string date)
		{
			Date = DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
			if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
			{
				return RedirectToPage("/Login");
			}
			if (!Utils.isManager(HttpContext.Session.GetString("Role")))
			{
				return RedirectToPage("/Login");
			}
			ManagerId = HttpContext.Session.GetString("Id");

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
			await client.GetAsync($"{MonthSalaryApi}/Calculate?month={Date.Month}&year={Date.Year}");
			return await OnGetAsync();
		}
	}
}
