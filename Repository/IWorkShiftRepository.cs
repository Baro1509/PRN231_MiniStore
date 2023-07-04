using DataAccess.Models;

namespace Repository
{
    public interface IWorkShiftRepository
    {
        public void CreateDefaultWorkShiftsForDate(DateOnly date, string managerId);
        public void CreateDefaultWorkShift(DateOnly date, string managerId, int shiftOrder);
        public bool Update(WorkShift workShift);
        public bool DeleteWorkShift(int shiftId);
        public List<WorkShift> GetAllByStartDate(DateOnly date);
    }
}
