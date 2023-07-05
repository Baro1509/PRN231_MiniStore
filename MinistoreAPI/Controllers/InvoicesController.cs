using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers {
    public class InvoicesController : ODataController {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository) {
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult Get() {
            return Ok(_invoiceRepository.GetAllInvoice());
        }
        
        public IActionResult Get([FromODataUri] int key) {
            return Ok(_invoiceRepository.Get(key));
        }

        public IActionResult Post([FromBody] Invoice invoice) {
            _invoiceRepository.Add(invoice);
            return Ok(new Invoice());
        }
        
        public IActionResult Delete([FromODataUri] int key) {
            _invoiceRepository.Delete(key);
            return Ok();
        }
    }
}
