using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
	public class EditModel : PageModel
	{
		private ODataClient _odataClient;

		public EditModel()
		{
			_odataClient = OdataUtils.GetODataClient();
		}

		[BindProperty]
		public Product Product { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
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
			Product = await _odataClient.For<Product>().Key(id).Expand(p => p.Category).FindEntryAsync();
			var temp = await _odataClient.For<Category>().FindEntriesAsync();
			ViewData["Category"] = new SelectList(temp.ToList(), "CategoryId", "CategoryName");
			return Page();
		}

		public async Task<IActionResult> OnPostEdit()
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
			await _odataClient.For<Product>().Key(Product.ProductId).Set(Product).UpdateEntryAsync();
			return RedirectToPage("./Details", new { id = Product.ProductId.ToString() });
		}
	}
}
