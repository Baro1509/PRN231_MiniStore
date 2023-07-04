using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers {
    public class InvoicesController : ODataController {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IProductRepository _productRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository, IStaffRepository staffRepository, IProductRepository productRepository) {
            _invoiceRepository = invoiceRepository;
            _staffRepository = staffRepository;
            _productRepository = productRepository;
        }


        public IActionResult Get() {
            return Ok(_invoiceRepository.GetAllInvoice());
        }
        
        public IActionResult Get([FromODataUri] int key) {
            return Ok(_invoiceRepository.Get(key));
        }

        public IActionResult Post([FromBody] Invoice invoice) {
            _invoiceRepository.Add(invoice);
            return Ok();
        }
        
        public IActionResult Delete([FromODataUri] int key) {
            _invoiceRepository.Delete(key);
            return Ok();
        }

        
        public ActionResult CreateNewInvoice(Invoice invoice)
        {
            if(invoice == null)
            {
                return BadRequest("There is no invoice");
            }
            Staff? staff = new Staff(); 

            if(invoice.StaffId != null)
            {
                staff = _staffRepository.GetStaff(invoice.StaffId);
            }
            var invoiceDetail = invoice.InvoiceDetails;
            if(invoiceDetail == null || invoiceDetail.Count == 0)
            {
                return BadRequest("Invoice must have invoice details");
            }
            List<InvoiceDetail> updatedInvoice = new List<InvoiceDetail>();
            Product product;
            decimal total = 0;
           
            foreach (var invoiceDT in invoiceDetail)
            {

                product = _productRepository.Get(invoiceDT.ProductId); 
                if(product.Status != 1)
                {
                    return BadRequest("Contain an invalid product");
                }
                if(invoiceDT.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than 0");
                }
                if(invoiceDT.Quantity > product.UnitsInStock)
                {
                    return BadRequest("There is no more product in stock");
                }
                else
                {
                    product.UnitsInStock -= invoiceDT.Quantity;
                    updatedInvoice.Add(invoiceDT);
                }
                invoiceDT.UnitPrice = product.UnitPrice;
                total += invoiceDT.Quantity * product.UnitPrice;
                
            }
            invoice.Total = total;
            _invoiceRepository.Add(invoice);
            foreach (Product p in updatedInvoice)
            {
                _invoiceRepository.Update(p);
            }
            return Ok();
        }
    }
}
