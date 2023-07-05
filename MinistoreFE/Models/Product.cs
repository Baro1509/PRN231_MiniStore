using System;
using System.Collections.Generic;

namespace MinistoreFE.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int ProductId { get; set; }
        public string CategoryId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public byte? ProductStatus { get; set; }
        public byte? Status { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
