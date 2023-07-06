using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinistoreFE.Pages.Manager
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }
    }
}
