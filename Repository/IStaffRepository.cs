﻿using DataAccess.Models;

namespace Repository
{
    public interface IStaffRepository
    {
        public Staff Login(string username, string password);
        public Staff? GetStaff(string id);
        public List<Staff> GetAll();
        public bool Update(Staff staff);
        public bool Create(Staff staff);
        public bool Delete(string staffid);
    }
}
