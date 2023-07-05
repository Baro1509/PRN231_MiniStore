using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Manager.Products
{
    public class DeleteModel : PageModel
    {
        private ODataClient _odataclient;

        public DeleteModel() {
            _odataclient = OdataUtils.GetODataClient();
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            Product = await _odataclient.For<Product>().Key(id).Expand(p => p.Category).FindEntryAsync();
            return Page();
        }
        
        public async Task<IActionResult> OnPostDelete(int? productId) {
            await _odataclient.For<Product>().Key(productId).DeleteEntryAsync();
            return RedirectToPage("./Index");
        }
    }
}
