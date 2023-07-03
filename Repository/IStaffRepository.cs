using DataAccess.Models;

namespace Repository
{
    public interface IStaffRepository
    {
        public Staff? GetStaff(string id);
    }
}
