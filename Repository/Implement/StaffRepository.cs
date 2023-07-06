﻿using DataAccess;
using DataAccess.Models;

namespace Repository.Implement
{
    public class StaffRepository : IStaffRepository
    {
        private readonly StaffDAO _staffDAO;
        private readonly DutyDAO _dutyDAO;

        public StaffRepository(StaffDAO staffDAO, DutyDAO dutyDAO)
        {
            _staffDAO = staffDAO;
            _dutyDAO = dutyDAO;
        }
        public Staff Get(string id)
        {
            return _staffDAO.GetAll().Where(p => p.StaffId.Equals(id)).FirstOrDefault();
        }
        public Staff Login(string username, string password)
        {
            return _staffDAO.GetAll().Where(p => p.StaffId == username && p.Password == password).FirstOrDefault();
        }
        public Staff? GetStaff(string id)
        {
            return _staffDAO.GetStaff(id);
        }
        public bool Update(Staff staff)
        {
            var found = _staffDAO.GetStaff(staff.StaffId);
            if (found != null)
            {
                found.Status = staff.Status;
                _staffDAO.Update(found);
                return true;
            }
            return false;
        }
        public bool Create(Staff staff)
        {
            var found = _staffDAO.GetStaff(staff.StaffId);
            if (found == null)
            {
                _staffDAO.Create(staff);
                return true;
            }
            return false;
        }

        public bool Delete(string staffId)
        {
            var found = _staffDAO.GetStaff(staffId);
            if (found != null)
            {
                found.Status = (byte?)Status.Deleted;
                _staffDAO.Delete(found);
                return true;
            }
            return false;

        }

        public List<Staff> GetAll()
        {
            return _staffDAO.GetAll().OrderByDescending(s => s.Status).ToList();
        }
    }
}
