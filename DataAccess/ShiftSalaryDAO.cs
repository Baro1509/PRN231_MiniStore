using DataAccess.Models;

namespace DataAccess
{
    public class ShiftSalaryDAO : DataAccessBase<ShiftSalary>
    {
        public ShiftSalary? Get(int id)
        {
            return GetAll().FirstOrDefault(s => s.ShiftSalaryId == id);
        }
        public IQueryable<ShiftSalary> GetByStaff(string staffId)
        {
            return GetAll().Where(s => s.AssignedTo.Equals(staffId));
        }
        public IQueryable<ShiftSalary> GetStaffSalaryByTime(string staffId, DateTime to)
        {
            return GetAll().OrderByDescending(s => s.CreatedTime).Where(s => s.AssignedTo.Equals(staffId) && s.CreatedTime < to);
        }
    }
}
