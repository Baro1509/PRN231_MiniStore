using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Salesman.Products
{
    public class IndexModel : PageModel
    {
        private ODataClient _odataClient;
        public IndexModel()
        {
            _odataClient = OdataUtils.GetODataClient();
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token"))) {
                return RedirectToPage("/Login");
            }
            if (!Utils.isSalesman(HttpContext.Session.GetString("Role"))) {
                return RedirectToPage("/Login");
            }
            Product = new List<Product>();
            var token = HttpContext.Session.GetString("Token");
            _odataClient = OdataUtils.GetODataClient(token);
            var temp = await _odataClient.For<Product>().Expand(p => p.Category).FindEntriesAsync();
            Product = temp.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAdd(int productId) {
            if (!Utils.isLogin(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("Role"), HttpContext.Session.GetString("Token"))) {
                return RedirectToPage("/Login");
            }
            if (!Utils.isSalesman(HttpContext.Session.GetString("Role"))) {
                return RedirectToPage("/Login");
            }

            var cart = HttpContext.Session.GetCustomObjectFromSession<Cart>("Cart");
            if (cart == null) {
                cart = Utils.CreateCart(HttpContext.Session.GetString("Id"));
            }
            var item = cart.CartItems.Find(p => p.ProductId == productId);
            if (item == null) {
                cart.CartItems.Add(new CartItem { ProductId = productId, Discount = 0, Quantity = 1 });
            } else {
                item.Quantity += 1;
            }
            HttpContext.Session.SetObjectInSession("Cart", cart);
            return RedirectToPage();
        }
    }
}
