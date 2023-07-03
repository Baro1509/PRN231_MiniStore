using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DutyDAO : DataAccessBase<Duty>
    {
        public new IQueryable<Duty> GetAll()
        {
            return GetAll().Include(d => d.Shift);
        }
        public IQueryable<Duty> GetDutiesInRange(DateTime startTime, DateTime endTime)
        {
            return GetAll().Where(d => d.Shift.StartTime >= startTime && d.Shift.EndTime <= endTime);
        }
    }
}
