
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;
using MinistoreFE.Request;
using Newtonsoft.Json;
using Simple.OData.Client;
using System.Globalization;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Manager.Duty
{
    public class IndexModel : PageModel
    {
        private ODataClient _odataClient;
        private HttpClient client;
        private string WorkShiftApi = "https://localhost:7036/odata/WorkShifts";
        private string ManagerId = "VyLT1";
        public IndexModel()
        {
            _odataClient = OdataUtils.GetODataClient();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            Date = DateTime.Now;
        }

        public List<DateOnly> Dates { get; set; }
        public List<WorkShift> WorkShifts { get; set; }
        public DateTime Date { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
            WorkShifts = temp.ToList();
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
        public async Task<IActionResult> OnPostAsync(string date)
        {
            Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var temp = await _odataClient.For<WorkShift>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}").Expand(d => d.Duties).FindEntriesAsync();
            WorkShifts = temp.ToList();
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
    }
}
