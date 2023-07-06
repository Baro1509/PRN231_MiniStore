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
    public class DetailsModel : PageModel
    {
        private ODataClient _odataClient;

        public DetailsModel() {
            _odataClient = OdataUtils.GetODataClient();
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token"))) {
                return RedirectToPage("/Login");
            }
            if (!Utils.isSalesman(HttpContext.Session.GetString("Role"))) {
                return RedirectToPage("/Login");
            }
            _odataClient = OdataUtils.GetODataClient(HttpContext.Session.GetString("Token"));
            Product = await _odataClient.For<Product>().Key(id).Expand(p => p.Category).FindEntryAsync();
            return Page();
        }
    }
}
