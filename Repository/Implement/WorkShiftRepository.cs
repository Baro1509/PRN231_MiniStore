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
        public void CreateWorkShift(WorkShift workShift)
        {
            if (workShift.Coefficient == 0)
            {
                workShift.Coefficient = 1;
            }
            workShift.Status = (byte?)DataAccess.Constants.Status.Available;
            _workshiftDAO.Create(workShift);
        }

        public void CreateDefaultWorkShiftsForDate(DateOnly date, string managerId)
        {
            WorkShift ws = DefaultWorkShift.DefaultShift1(date, managerId);
            _workshiftDAO.Create(ws);

            ws = DefaultWorkShift.DefaultShift2(date, managerId);
            _workshiftDAO.Create(ws);

            ws = DefaultWorkShift.DefaultShift3(date, managerId);
            _workshiftDAO.Create(ws);

            ws = DefaultWorkShift.DefaultShift4(date, managerId);
            _workshiftDAO.Create(ws);

            ws = DefaultWorkShift.DefaultShift5(date, managerId);
            _workshiftDAO.Create(ws);

            ws = DefaultWorkShift.DefaultShift6(date, managerId);
            _workshiftDAO.Create(ws);
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

        public void Update(WorkShift ws)
        {
            _workshiftDAO.Update(ws);
        }
    }
}
