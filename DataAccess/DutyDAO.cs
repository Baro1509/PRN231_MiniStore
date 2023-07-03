using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DutyDAO : DataAccessBase<Duty>
    {
        public IQueryable<Duty> GetAllDuties()
        {
            return GetAll().Include(d => d.Shift);
        }
        public IQueryable<Duty> GetDutiesInRange(DateTime startTime, DateTime endTime)
        {
            return GetAllDuties().Where(d => d.Shift.StartTime >= startTime && d.Shift.EndTime <= endTime);
        }
        public IQueryable<Duty> GetDutiesByStaff(string staffId)
        {
            return GetAllDuties().Where(d => d.AssignedTo.Equals(staffId));
        }
        public IQueryable<Duty> GetDutiesByShift(int shiftId)
        {
            return GetAllDuties().Where(d => d.ShiftId == shiftId);
        }
    }
}
