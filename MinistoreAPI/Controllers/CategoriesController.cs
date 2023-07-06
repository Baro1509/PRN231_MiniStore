using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers {
    [Authorize]
    public class CategoriesController : ODataController {
        private readonly IProductRepository _productRepository;

        public CategoriesController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        [EnableQuery]
        public IActionResult Get() {
            return Ok(_productRepository.GetAllCategory());
        }
    }
}
