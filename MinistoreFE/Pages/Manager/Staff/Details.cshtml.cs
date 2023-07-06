using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Simple.OData.Client;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Manager.Staff
{
    public class DetailsModel : PageModel
    {
        private ODataClient _odataClient;
        private HttpClient client;

        private string api = "https://localhost:7036/odata/Staffs";
        public DetailsModel()
        {
            _odataClient = OdataUtils.GetODataClient();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public Models.Staff staff { get; set; }
        public decimal salary { get; set; }
        public string role { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            staff = await _odataClient.For<Models.Staff>().QueryOptions($"staffId={id}").FindEntryAsync();
            if (staff == null)
            {
                return NotFound();
            }
            var salaryGet = await _odataClient.For<Models.ShiftSalary>().QueryOptions($"staffId={id}").FindEntryAsync();
            salary = salaryGet.Salary;
            role = Constants.Role.GetStatus(staff.RoleId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var staffTemp = await _odataClient.For<Models.Staff>().QueryOptions($"$filter=StaffId eq {id}").FindEntryAsync();
            if (staffTemp == null)
            {
                return NotFound();
            }
            if (staffTemp.Status == 0)
            {
                staffTemp.Status = 1;
            }
            else
            {
                staffTemp.Status = 0;
            }

            var myContent = JsonConvert.SerializeObject(staffTemp);
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await client.PutAsync(api + $"?id={id}", byteContent);

            return RedirectToPage("./Index");

        }
    }
}
