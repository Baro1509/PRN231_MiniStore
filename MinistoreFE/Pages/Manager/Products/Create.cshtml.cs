using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
    public class CreateModel : PageModel
    {
        private ODataClient _odataclient;

        public CreateModel() {
            _odataclient = OdataUtils.GetODataClient();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync() {

            var temp = await _odataclient.For<Category>().FindEntriesAsync();
            ViewData["Category"] = new SelectList(temp.ToList(), "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostCreate() {
            Product.Status = 1;
            Product.ProductStatus = 1;
            var temp = await _odataclient.For<Product>().Set(Product).InsertEntryAsync();
            return RedirectToPage("./Index");
        }
    }
}
