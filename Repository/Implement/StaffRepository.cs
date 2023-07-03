using DataAccess;
using DataAccess.Models;

namespace Repository.Implement
{
    public class StaffRepository : IStaffRepository
    {
        private readonly StaffDAO _staffDAO;

        public StaffRepository(StaffDAO staffDAO)
        {
            _staffDAO = staffDAO;
        }
        public Staff? GetStaff(string id)
        {
            return _staffDAO.GetStaff(id);
        }
    }
}
