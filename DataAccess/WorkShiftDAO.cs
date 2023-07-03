using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class WorkShiftDAO : DataAccessBase<WorkShift>
    {
        public WorkShift? Get(int id)
        {
            return GetAll().Include(ws => ws.Duties).FirstOrDefault(ws => ws.ShiftId == id);
        }
        public IQueryable<WorkShift> GetAllByStartDate(DateOnly date)
        {
            DateTime from = date.ToDateTime(new TimeOnly(0, 0));
            DateTime to = from.AddDays(1);
            return GetAll().Include(ws => ws.Duties).Where(ws => ws.StartTime >= from && ws.StartTime <= to);
        }
    }
}
