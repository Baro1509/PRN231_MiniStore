using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple.OData.Client;
using System.Net.Http.Headers;
namespace MinistoreFE.Pages.Manager.Staff
{
    public class IndexModel : PageModel
    {
        private ODataClient _odataClient;
        private HttpClient client;


        public IndexModel()
        {
            _odataClient = OdataUtils.GetODataClient();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

        }
        public IList<Models.Staff> StaffS { get; set; } = default!;
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
            var token = HttpContext.Session.GetString("Token");
            _odataClient = OdataUtils.GetODataClient(token);
            var staffs = await _odataClient.For<Models.Staff>().FindEntriesAsync();
            StaffS = staffs.ToList();
            return Page();
        }

    }
}
