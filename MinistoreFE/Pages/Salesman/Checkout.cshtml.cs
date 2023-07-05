using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MinistoreFE.Models;
using Simple.OData.Client;

namespace MinistoreFE.Pages.Salesman
{
    public class CheckoutModel : PageModel
    {
        private ODataClient _odataClient;
        public CheckoutModel()
        {
            _odataClient = OdataUtils.GetODataClient();
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Product = new List<Product>();
            var cart = HttpContext.Session.GetCustomObjectFromSession<Cart>("Cart");
            if (cart == null) {
                cart = Utils.CreateCart("1");
            }
            foreach (var item in cart.CartItems) {
                var temp = await _odataClient.For<Product>().Key(item.ProductId).Expand(p => p.Category).FindEntryAsync();
                temp.UnitsInStock = item.Quantity;
                Product.Add(temp);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmit() {
            var cart = HttpContext.Session.GetCustomObjectFromSession<Cart>("Cart");
            if (cart == null) {
                cart = Utils.CreateCart("1");
                return RedirectToPage("/Salesman/Products/Index");
            }

            Invoice invoice = new Invoice { StaffId = cart.StaffId, OrderDate = DateTime.Now, Status = 1, Total = 0, InvoiceDetails = new List<InvoiceDetail>() };
            foreach (var item in cart.CartItems) {
                var product = await _odataClient.For<Product>().Key(item.ProductId).Expand(p => p.Category).FindEntryAsync();
                if (product != null) {
                    var detail = new InvoiceDetail {
                        ProductId = product.ProductId,
                        Discount = item.Discount,
                        UnitPrice = product.UnitPrice,
                        Quantity = item.Quantity
                    };
                    invoice.InvoiceDetails.Add(detail);
                    invoice.Total += detail.Quantity * detail.UnitPrice * (decimal) (1 - detail.Discount);
                }
            }
            await _odataClient.For<Invoice>().Set(invoice).InsertEntryAsync();
            return RedirectToPage("/Salesman/Products/Index");
        }

        public async Task<IActionResult> OnPostRemove() {
            return RedirectToPage();
        }
    }
}
