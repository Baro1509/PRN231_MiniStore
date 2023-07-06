using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;

namespace MinistoreFE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token")))
            {
                return Page();
            }
            if (Utils.isSalesman(HttpContext.Session.GetString("Role")))
            {
                return RedirectToPage("/Salesman/Checkout");
            }
            if (Utils.isManager(HttpContext.Session.GetString("Role")))
            {
                return RedirectToPage("/Manager/Staff/Index");
            }
            return RedirectToPage("/Staff/Profile");
        }

        [BindProperty]
        public LoginCredentials Login { get; set; }

        public async Task<IActionResult> OnPostLogin()
        {
            Response res = new Response();
            var httpClient = _httpClientFactory.CreateClient("ministoreapi");
            var httpResponseMessage = await httpClient.PostAsync("Login", Utils.ConvertForPost<LoginCredentials>(Login));

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Login credentials are incorrect";
                return Page();
            }

            res = await httpResponseMessage.Content.ReadAsAsync<Response>();
            HttpContext.Session.SetString("Id", res.Id);
            HttpContext.Session.SetString("Token", res.Token);
            HttpContext.Session.SetString("Role", res.Role);
            //if (res.Role.Equals("Admin")) {
            //    return RedirectToPage("/Admin/Index");
            //}
            if (res.Role.Equals("MG"))
            {
                return RedirectToPage("/Manager/Staff/Index");
            }
            if (res.Role.Equals("SG"))
            {
                return RedirectToPage("/Staff/Profile");
            }
            HttpContext.Session.SetObjectInSession("Cart", new Cart { StaffId = res.Id, CartItems = new List<CartItem>() });
            return RedirectToPage("/Salesman/Products/Index");
        }
    }

    public class Response
    {
        public string Role { get; set; }
        public string Id { get; set; }
        public string Token { get; set; }
    }

    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
