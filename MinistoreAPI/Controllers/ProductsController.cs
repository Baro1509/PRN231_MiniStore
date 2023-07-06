using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace MinistoreAPI.Controllers {
    [Authorize]
    public class ProductsController : ODataController { 
        private readonly IProductRepository _repository;
        private readonly IStaffRepository _staffRepository;

        public ProductsController(IProductRepository repository, IStaffRepository staffRepository) {
            _repository = repository;
            _staffRepository = staffRepository;
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
            if (!isManager()) return Unauthorized();
            _repository.Add(product);
            return Ok(_repository.GetNewestProduct());
        }
        
        [EnableQuery]
        public IActionResult Patch([FromODataUri] int key, [FromBody] Delta<Product> delta) {
            if (!isManager()) return Unauthorized();
            var db = _repository.Get(key);
            delta.Patch(db);
            return Ok(_repository.Update(db));
        }

        private bool isManager() {
            var user = HttpContext.User;
            var id = user.Claims.FirstOrDefault(p => p.Type == "StaffId").Value;
            var staff = _staffRepository.Get(id);
            return "MG".Equals(staff.RoleId);
        }
    }
}
