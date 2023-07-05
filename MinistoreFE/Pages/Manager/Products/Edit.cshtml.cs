using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
    public class EditModel : PageModel
    {
        private ODataClient _odataclient;

        public EditModel() {
            _odataclient = OdataUtils.GetODataClient();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            Product = await _odataclient.For<Product>().Key(id).Expand(p => p.Category).FindEntryAsync();
            var temp = await _odataclient.For<Category>().FindEntriesAsync();
            ViewData["Category"] = new SelectList(temp.ToList(), "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostEdit() {
            Product.Status = 1;
            Product.ProductStatus = 1;
            await _odataclient.For<Product>().Key(Product.ProductId).Set(Product).UpdateEntryAsync();
            return RedirectToPage("./Details", new {id = Product.ProductId.ToString() });
        }
    }
}
