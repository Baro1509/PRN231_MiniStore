using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple.OData.Client;
namespace MinistoreFE.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private ODataClient _odataClient;
        private string StaffId = "VyLT";
        public ProfileModel()
        {
            _odataClient = OdataUtils.GetODataClient();

        }
        public Models.Staff staff { get; set; }
        public decimal salary { get; set; }
        public string role { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            staff = await _odataClient.For<Models.Staff>().QueryOptions($"staffId={StaffId}").FindEntryAsync();
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
