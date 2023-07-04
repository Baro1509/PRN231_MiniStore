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
        public ShiftSalary? GetStaffSalaryByTime(string staffId, DateTime time)
        {
            return GetAll().OrderByDescending(s => s.StartTime).FirstOrDefault(s => s.AssignedTo.Equals(staffId) && s.StartTime < time);
        }
    }
}
