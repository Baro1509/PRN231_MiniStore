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
    public class IndexModel : PageModel
    {
        private ODataClient _odataClient;
        public IndexModel() {
            _odataClient = OdataUtils.GetODataClient();
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync() {
            var temp = await _odataClient.For<Product>().Expand(p => p.Category).FindEntriesAsync();
            Product = temp.ToList();
            return Page();
        }
    }
}
