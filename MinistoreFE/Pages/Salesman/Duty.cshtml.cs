using Frontend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;
using Simple.OData.Client;
using System.Globalization;

namespace MinistoreFE.Pages.Salesman
{
    public class DutyModel : PageModel
    {
        private ODataClient _odataClient;
        private string StaffId = "VyLT";
        public DutyModel()
        {
            _odataClient = OdataUtils.GetODataClient();
            Date = DateTime.Now;
        }

        public List<DateOnly> Dates { get; set; }
        public List<Duty> Duties { get; set; }
        public DateTime Date { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            CalcDate();
            var temp = await _odataClient.For<Duty>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}&staffId={StaffId}").Expand(d => d.Shift).FindEntriesAsync();
            Duties = temp.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string date)
        {
            Date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            CalcDate();
            var temp = await _odataClient.For<Duty>().QueryOptions($"year={Date.Year}&month={Date.Month}&date={Date.Day}&staffId={StaffId}").Expand(d => d.Shift).FindEntriesAsync();
            Duties = temp.ToList();
            return Page();
        }
        public void CalcDate()
        {
            int dayOfWeek = -(int)Date.DayOfWeek;
            DateOnly date = DateOnly.FromDateTime(Date).AddDays(dayOfWeek - 1);
            Dates = new List<DateOnly>();
            for (int i = dayOfWeek; i < 7 + dayOfWeek; i++)
            {
                date = date.AddDays(1);
                Dates.Add(date);
            }
        }
    }
}
