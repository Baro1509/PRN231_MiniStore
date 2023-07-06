using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace MinistoreFE.Pages
{
    public class CheckOutModel : PageModel
    {
        private HttpClient client;
        private string CheckInApi = "https://localhost:7036/CheckOut";
        private string StaffId;
        private string Token;

        public CheckOutModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> OnGetAsync()
        {
            StaffId = HttpContext.Session.GetString("Id");
            Token = HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            await client.GetAsync($"{CheckInApi}?id={StaffId}");
            return RedirectToPage("Index");
        }
    }
}
