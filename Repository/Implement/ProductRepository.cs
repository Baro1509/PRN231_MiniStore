using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement {
    public class ProductRepository : IProductRepository {
        private readonly ProductDAO _productDAO;
        private readonly CategoryDAO _categoryDAO;

        public ProductRepository(ProductDAO productDAO, CategoryDAO categoryDAO) {
            _productDAO = productDAO;
            _categoryDAO = categoryDAO;
        }

        public void Delete(int productId) {
            var product = Get(productId);
            if (product == null) {
                return;
            }
            product.Status = 0;
            Update(product);
        }

        public Product Get(int productId) {
            return _productDAO.GetAll().Where(p => p.ProductId == productId && p.Status == 1).Include(p => p.Category).FirstOrDefault();
        }

        public List<Category> GetAllCategory() {
            return _categoryDAO.GetAll().Where(p => p.Status == 1).ToList();
        }

        public List<Product> GetAllProduct() {
            return _productDAO.GetAll().Where(p => p.Status == 1).Include(p => p.Category).ToList();
        }

        public void Update(Product product) {
            _productDAO.Update(product);
        }
    }
}
