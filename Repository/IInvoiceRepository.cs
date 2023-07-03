using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public interface IInvoiceRepository {
        public void Add(Invoice invoice);
        public Invoice Get(int invoiceId);
        public void Update(Invoice invoice);
        public void Delete(int invoiceId);
        public List<Invoice> GetAllInvoice();
        public List<Invoice> GetAllInvoiceByStaffId(int staffId);
    }
}
