using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
	public class CreateModel : PageModel
	{
		private ODataClient _odataClient;

		public CreateModel()
		{
			_odataClient = OdataUtils.GetODataClient();
		}

		[BindProperty]
		public Product Product { get; set; } = default!;

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
			var temp = await _odataClient.For<Category>().FindEntriesAsync();
			ViewData["Category"] = new SelectList(temp.ToList(), "CategoryId", "CategoryName");
			return Page();
		}

		public async Task<IActionResult> OnPostCreate()
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

			Product.Status = 1;
			Product.ProductStatus = 1;
			var temp = await _odataClient.For<Product>().Set(Product).InsertEntryAsync();
			return RedirectToPage("./Index");
		}
	}
}
