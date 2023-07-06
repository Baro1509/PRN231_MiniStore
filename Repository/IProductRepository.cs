using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public interface IProductRepository {
        public void Add(Product product);
        public Product Update(Product product);
        public void Delete(int productId);
        public Product Get(int productId);
        public Product GetNewestProduct();
        public List<Product> GetAllProduct();

        public List<Category> GetAllCategory();

        public List<Product> GetAllProductManager();
    }
}
