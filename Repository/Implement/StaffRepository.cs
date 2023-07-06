using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement {
    public class StaffRepository : IStaffRepository {
        private readonly StaffDAO _staffDAO;

        public StaffRepository(StaffDAO staffDAO) {
            _staffDAO = staffDAO;
        }

        public staff Get(string id) {
            return _staffDAO.GetAll().Where(p => p.StaffId.Equals(id)).FirstOrDefault();
        }

        public staff Login(string username, string password) {
            return _staffDAO.GetAll().Where(p => p.StaffId == username && p.Password == password).FirstOrDefault();
        }
    }
}
