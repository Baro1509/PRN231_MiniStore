using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers {
    public class ProductsController : ODataController { 
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository) {
            _repository = repository;
        }

        [EnableQuery]
        public IActionResult Get() {
            return Ok(_repository.GetAllProduct());
        }

        public IActionResult Get([FromODataUri] int key) {
            return Ok(_repository.Get(key));
        }
    }
}
