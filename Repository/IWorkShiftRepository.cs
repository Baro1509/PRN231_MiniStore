using DataAccess.Models;

namespace Repository
{
    public interface IWorkShiftRepository
    {
        public void CreateWorkShiftForDate(DateOnly date, string managerId);
        public void CreateWorkShift(WorkShift workShift);
        public void Update(WorkShift workShift);
        public bool DeleteWorkShift(int shiftId);
        public List<WorkShift> GetAllByStartDate(DateOnly date);
    }
}
