using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
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

        [EnableQuery]
        public IActionResult Get([FromODataUri] int key) {
            return Ok(_repository.Get(key));
        }
        
        [EnableQuery]
        public IActionResult Post([FromBody] Product product) {
            _repository.Add(product);
            return Ok(_repository.GetNewestProduct());
        }
        
        [EnableQuery]
        public IActionResult Patch([FromODataUri] int key, [FromBody] Delta<Product> delta) {
            var db = _repository.Get(key);
            delta.Patch(db);
            return Ok(_repository.Update(db));
        }
    }
}
