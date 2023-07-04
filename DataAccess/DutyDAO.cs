using DataAccess.Constants;
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
        public IQueryable<Duty> GetStaffSchedule(string staffId, DateTime from, DateTime to)
        {
            return GetAllDuties().Where(d => d.AssignedTo.Equals(staffId) && d.Shift.StartTime >= from && d.Shift.StartTime < to);
        }
        public IQueryable<Duty> GetStaffCompletedDuties(string staffId, DateTime from, DateTime to)
        {
            return GetAllDuties().Where(d => d.AssignedTo.Equals(staffId) && d.Shift.EndTime > from && d.Shift.EndTime <= to && d.Status == (byte?)DutyStatus.Present);
        }

        public IQueryable<Duty> GetDutiesByShift(int shiftId)
        {
            return GetAllDuties().Where(d => d.ShiftId == shiftId);
        }
        public Duty? GetById(int id)
        {
            return GetAllDuties().FirstOrDefault(d => d.DutyId == id);
        }
    }
}
