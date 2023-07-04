using DataAccess;
using DataAccess.Constants;
using DataAccess.Models;

namespace Repository.Implement
{
    public class WorkShiftRepository : IWorkShiftRepository
    {
        private readonly WorkShiftDAO _workshiftDAO;
        private readonly DutyDAO _dutyDAO;


        public WorkShiftRepository(WorkShiftDAO workshiftDAO, DutyDAO dutyDAO)
        {
            _workshiftDAO = workshiftDAO;
            _dutyDAO = dutyDAO;
        }
        public void CreateDefaultWorkShiftsForDate(DateOnly date, string managerId)
        {
            List<WorkShift> workShifts = DefaultWorkShift.DefaultShifts(date, managerId);
            foreach (WorkShift ws in workShifts)
            {
                _workshiftDAO.Create(ws);
            }
        }
        public void CreateDefaultWorkShift(DateOnly date, string managerId, int shiftOrder)
        {
            List<WorkShift> workShifts = DefaultWorkShift.DefaultShifts(date, managerId);
            if (shiftOrder > 0 && shiftOrder <= workShifts.Count)
            {
                _workshiftDAO.Create(workShifts[shiftOrder - 1]);
            }
        }
        /*
         * Delete if workshift has no duty, else update workshift status
         */
        public bool DeleteWorkShift(int shiftId)
        {
            var ws = _workshiftDAO.Get(shiftId);
            if (ws != null)
            {
                var duty = _dutyDAO.GetDutiesByShift(shiftId);
                if (duty.Any())
                {
                    ws.Status = (byte?)DataAccess.Constants.Status.Deleted;
                    _workshiftDAO.Update(ws);
                    return true;
                }
                _workshiftDAO.Delete(ws);
                return true;
            }
            return false;
        }

        public List<WorkShift> GetAllByStartDate(DateOnly date)
        {
            return _workshiftDAO.GetAllByStartDate(date).ToList();
        }

        public bool Update(WorkShift ws)
        {
            var found = _workshiftDAO.Get(ws.ShiftId);
            if (found != null && ws.Coefficient > 0 && ws.Bonus >= 0 && ws.StartTime < ws.EndTime)
            {
                _workshiftDAO.Update(ws);
                return true;
            }
            return false;
        }
    }
}
