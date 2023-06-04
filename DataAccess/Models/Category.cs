using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
