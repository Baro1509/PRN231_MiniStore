using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Salesman.Products
{
    public class DetailsModel : PageModel
    {
        private ODataClient _odataclient;

        public DetailsModel()
        {
            _odataclient = OdataUtils.GetODataClient();
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Product = await _odataclient.For<Product>().Key(id).Expand(p => p.Category).FindEntryAsync();
            return Page();
        }
    }
}
