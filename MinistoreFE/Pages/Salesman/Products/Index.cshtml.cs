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
    public class IndexModel : PageModel
    {
        private readonly ODataClient _odataClient;
        public IndexModel()
        {
            _odataClient = OdataUtils.GetODataClient();
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //Product = new List<Product>();
            var temp = await _odataClient.For<Product>().FindEntriesAsync();
            Product = temp.ToList();
        }
    }
}
