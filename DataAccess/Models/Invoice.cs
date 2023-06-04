using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int InvoiceId { get; set; }
        public string StaffId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public byte? Status { get; set; }

        public virtual staff Staff { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
