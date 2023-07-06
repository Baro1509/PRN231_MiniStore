using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Request;
using Newtonsoft.Json;
using Simple.OData.Client;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages.Manager.Staff
{
    public class CreateModel : PageModel
    {
        private ODataClient _odataClient;
        private HttpClient client;
        private string api = "https://localhost:7036/odata/Staffs";
        private string apiShift = "https://localhost:7036/odata/ShiftSalaries";


        private string manaId = "VyLT";

        public Models.Staff Staff { get; set; } = default!;

        public Models.ShiftSalary shiftSalary { get; set; } = default!;





        public CreateModel()
        {
            _odataClient = OdataUtils.GetODataClient();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> OnPost(string StaffId, string Password, string StaffName, string role, decimal Salary)
        {
            var Staff = new Models.Staff();
            ; StaffId = StaffId.Trim();
            if (StaffId.Length < 3)
            {
                ViewData["Message"] = "ID characters >= 3";
                return Page();
            }
            var listStaff = await _odataClient.For<Models.Staff>().FindEntriesAsync();
            bool checkStaff = listStaff.Select(x => x.StaffId).Any(x => x.ToLower().Equals(StaffId.ToLower()));
            if (checkStaff)
            {
                ViewData["Message"] = "Duplicate ID";
                return Page();
            }
            Staff.StaffId = StaffId;
            Staff.Password = Password;
            Staff.StaffName = StaffName;
            Staff.RoleId = role;
            Staff.Status = 1;

            var myContent = JsonConvert.SerializeObject(Staff);
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await client.PostAsync(api, byteContent);


            var shiftRequest = new ShiftSalaryRequest();
            shiftRequest.AssignedTo = Staff.StaffId;
            shiftRequest.ApprovedBy = manaId;
            shiftRequest.Salary = Salary;
            var myContent1 = JsonConvert.SerializeObject(shiftRequest);
            var byteContent1 = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(myContent1));
            byteContent1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await client.PostAsync(apiShift, byteContent1);

            return RedirectToPage("./Index");
        }
    }
}
