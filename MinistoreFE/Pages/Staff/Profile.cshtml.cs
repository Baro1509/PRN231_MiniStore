using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple.OData.Client;
namespace MinistoreFE.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private ODataClient _odataClient;
        private string StaffId;
        public ProfileModel()
        {
            _odataClient = OdataUtils.GetODataClient();

        }
        public Models.Staff staff { get; set; }
        public decimal salary { get; set; }
        public string role { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
            {
                return RedirectToPage("/Login");
            }
            StaffId = HttpContext.Session.GetString("Id");
            string token = HttpContext.Session.GetString("Token");
            _odataClient = OdataUtils.GetODataClient(token);

            var staffs = await _odataClient.For<Models.Staff>().FindEntriesAsync();
            staff = staffs.FirstOrDefault(s => s.StaffId.ToLower().Equals(StaffId.ToLower()));
            if (staff == null)
            {
                return NotFound();
            }
            var salaryGet = await _odataClient.For<Models.ShiftSalary>().QueryOptions($"staffId={StaffId}").FindEntryAsync();
            salary = salaryGet.Salary;
            role = Constants.Role.GetStatus(staff.RoleId);
            return Page();
        }
    }
}
