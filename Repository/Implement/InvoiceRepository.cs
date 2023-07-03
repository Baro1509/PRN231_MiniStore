using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement {
    public class InvoiceRepository : IInvoiceRepository {
        private readonly InvoiceDAO _invoiceDAO;

        public InvoiceRepository(InvoiceDAO invoiceDAO) {
            _invoiceDAO = invoiceDAO;
        }

        public void Add(Invoice invoice) {
            _invoiceDAO.Create(invoice);
        }

        public void Delete(int invoiceId) {
            var invoice = Get(invoiceId);
            if (invoice == null) {
                return;
            }
            invoice.Status = 0;
            _invoiceDAO.Update(invoice);
        }

        public Invoice Get(int invoiceId) {
            return _invoiceDAO.GetAll().Where(p => p.InvoiceId == invoiceId && p.Status == 1).Include(p => p.InvoiceDetails).FirstOrDefault();
        }

        public List<Invoice> GetAllInvoice() {
            return _invoiceDAO.GetAll().Where(p => p.Status == 1).Include(p => p.InvoiceDetails).ToList();
        }

        public List<Invoice> GetAllInvoiceByStaffId(int staffId) {
            return _invoiceDAO.GetAll().Where(p => p.StaffId.Equals(staffId) && p.Status == 1).Include(p => p.InvoiceDetails).ToList();
        }

        public void Update(Invoice invoice) {
            _invoiceDAO.Update(invoice);
        }
    }
}
