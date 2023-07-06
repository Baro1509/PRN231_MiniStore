
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Constants;
using MinistoreFE.Models;
using MinistoreFE.Request;
using Newtonsoft.Json;
using Simple.OData.Client;
using System.Globalization;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Manager
{
	public class DutyModel : PageModel
	{
		private ODataClient _odataClient;
		private HttpClient client;
		private string WorkShiftApi = "https://localhost:7036/odata/WorkShifts";
		private string DutyApi = "https://localhost:7036/odata/Duties";
		private string ManagerId;
		public DutyModel()
		{
			_odataClient = OdataUtils.GetODataClient();
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			Date = DateTime.Now;
		}

		public List<DateOnly> Dates { get; set; }
		public List<WorkShift> WorkShifts { get; set; }
		public List<Models.Staff> AllStaffs { get; set; }
		public List<Models.Staff> FreeStaffs { get; set; } = new List<Models.Staff>();
		public WorkShift CurrentShift { get; set; }
		public DateTime Date { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
			{
				return RedirectToPage("/Login");
			}
			if (!Utils.isManager(HttpContext.Session.GetString("Role")))
			{
				return RedirectToPage("/Login");
			}
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
			_odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
			ManagerId = HttpContext.Session.GetString("Id");
			var temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
			WorkShifts = temp.ToList();
			var staffTmp = await _odataClient.For<Models.Staff>().FindEntriesAsync();
			AllStaffs = staffTmp.Where(s => s.Status == (byte)Status.Available && !Role.IsManager(s.RoleId)).ToList();
			if (WorkShifts.Count == 0)
			{
				WorkShiftRequest request = new WorkShiftRequest(Date, ManagerId, 0);
				var myContent = JsonConvert.SerializeObject(request);
				var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				await client.PostAsync(WorkShiftApi, byteContent);
				temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
				WorkShifts = temp.ToList();
			}
			return Page();
		}
		public async Task<IActionResult> OnPost(string date)
		{
			if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
			{
				return RedirectToPage("/Login");
			}
			if (!Utils.isManager(HttpContext.Session.GetString("Role")))
			{
				return RedirectToPage("/Login");
			}
			_odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
			ManagerId = HttpContext.Session.GetString("Id");

			Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			var temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
			WorkShifts = temp.ToList();
			var staffTmp = await _odataClient.For<Models.Staff>().FindEntriesAsync();
			AllStaffs = staffTmp.Where(s => s.Status == (byte)Status.Available && !Role.IsManager(s.RoleId)).ToList();
			if (WorkShifts.Count == 0)
			{
				WorkShiftRequest request = new WorkShiftRequest(Date, ManagerId, 0);
				var myContent = JsonConvert.SerializeObject(request);
				var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
				await client.PostAsync(WorkShiftApi, byteContent);
				temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
				WorkShifts = temp.ToList();
			}
			return Page();
		}
		public async Task<IActionResult> OnPostUpdate(DateTime date, int dutyId, byte status)
		{
			if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
			{
				return RedirectToPage("/Login");
			}
			if (!Utils.isManager(HttpContext.Session.GetString("Role")))
			{
				return RedirectToPage("/Login");
			}
			ManagerId = HttpContext.Session.GetString("Id");

			Models.Duty duty = new Models.Duty { DutyId = dutyId, Status = status };
			var myContent = JsonConvert.SerializeObject(duty);
			var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
			await client.PostAsync($"{DutyApi}/Update", byteContent);
			return await OnPost(date.ToString("dd/MM/yyyy"));
		}
		public async Task<IActionResult> OnPostDelete(DateTime date, int dutyId)
		{
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
			await client.DeleteAsync($"{DutyApi}/{dutyId}");
			return await OnPost(date.ToString("dd/MM/yyyy"));
		}
		public async Task<IActionResult> OnPostCreate(DateTime date, int shiftId, string[] staffId)
		{
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

			Models.Duty duty;
			foreach (string id in staffId)
			{
				duty = new Models.Duty { ShiftId = shiftId, AssignedTo = id };
				var myContent = JsonConvert.SerializeObject(duty);
				var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				await client.PostAsync(DutyApi, byteContent);
			}
			return await OnPost(date.ToString("dd/MM/yyyy"));
		}
	}
}
