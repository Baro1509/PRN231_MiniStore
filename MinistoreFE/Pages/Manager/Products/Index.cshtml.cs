using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
	public class IndexModel : PageModel
	{
		private ODataClient _odataClient;
		public IndexModel()
		{
			_odataClient = OdataUtils.GetODataClient();
		}

		public IList<Product> Product { get; set; } = default!;

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
			_odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
			var temp = await _odataClient.For<Product>().Expand(p => p.Category).FindEntriesAsync();
			Product = temp.ToList();
			return Page();
		}
	}
}
