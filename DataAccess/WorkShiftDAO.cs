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
            return GetAll().Include(ws => ws.Duties).Where(ws => DateOnly.FromDateTime(ws.StartTime).CompareTo(date) == 0);
        }
    }
}
