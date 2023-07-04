using DataAccess;
using DataAccess.Constants;
using DataAccess.Models;

namespace Repository.Implement
{
    public class DutyRepository : IDutyRepository
    {
        private readonly AttendanceDAO _attendanceDAO;
        private readonly DutyDAO _dutyDAO;
        private readonly StaffDAO _staffDAO;
        private readonly WorkShiftDAO _workShiftDAO;

        public DutyRepository(AttendanceDAO attendanceDAO, DutyDAO dutyDAO, StaffDAO staffDAO, WorkShiftDAO workShiftDAO)
        {
            _attendanceDAO = attendanceDAO;
            _dutyDAO = dutyDAO;
            _staffDAO = staffDAO;
            _workShiftDAO = workShiftDAO;
        }

        public bool CreateDuty(string staffId, int workShiftId)
        {
            var staff = _staffDAO.GetStaff(staffId);
            var shift = _workShiftDAO.Get(workShiftId);
            if (staff != null && shift != null)
            {
                Duty duty = new Duty();
                duty.Status = (byte?)DutyStatus.Upcoming;
                duty.ShiftId = workShiftId;
                duty.AssignedTo = staffId;
                duty.RoleId = DataAccess.Constants.Role.IsSalesMan(staff.RoleId) ? 1 : 0;
                _dutyDAO.Create(duty);
                return true;
            }
            return false;
        }

        public bool DeleteDuty(int dutyId)
        {
            var duty = _dutyDAO.GetById(dutyId);
            if (duty != null)
            {
                _dutyDAO.Delete(duty);
                return true;
            }
            return false;
        }

        public List<Duty>? GetStaffWeekDuties(string staffId, DateOnly dateOnly)
        {
            var staff = _staffDAO.GetStaff(staffId);
            if (staff != null)
            {
                List<Duty> duties = new List<Duty>();
                int dayOfWeek = (int)dateOnly.DayOfWeek;
                TimeOnly time = new TimeOnly(0, 0);
                DateTime from = dateOnly.AddDays(-dayOfWeek).ToDateTime(time);
                DateTime to = dateOnly.AddDays(7 - dayOfWeek).ToDateTime(time);
                var found = _dutyDAO.GetDutiesByStaffInRange(staffId, from, to);
                if (found.Any())
                {
                    duties = found.ToList();
                }
                return duties;
            }
            return null;
        }

        public bool UpdateDuty(Duty duty)
        {
            var found = _dutyDAO.GetById(duty.DutyId);
            if (found != null)
            {
                var staff = _staffDAO.GetStaff(duty.AssignedTo);
                if (staff != null)
                {
                    found.AssignedTo = duty.AssignedTo;
                    found.Status = duty.Status;
                    _dutyDAO.Update(found);
                }
                return true;
            }
            return false;
        }

        /*
        * 1. Get latest checkin and checkout attendace with given staffId
        * 2. Calculate valid start time by substracting 10 mins from checkin time
        * 3. Calculate valid end time by adding 10 mins from checkout time
        * 4. Find all duty of given staff where shift start time and end time is between the range
        * 5. Update status of each found duty as Present
        */
        public bool UpdateLatestDutyStatus(string staffId)
        {
            var latestCheckIn = _attendanceDAO.GetLatestCheckIn(staffId);
            if (latestCheckIn != null)
            {
                var latestAttendance = _attendanceDAO.GetLatestAttendance(staffId);
                if (latestAttendance.IsCheckIn == AttendanceStatus.CheckOut)
                {
                    DateTime validStartTime = latestCheckIn.CreatedTime.AddMinutes(-10);
                    DateTime validEndTime = latestAttendance.CreatedTime.AddMinutes(10);
                    List<Duty> duties = _dutyDAO.GetDutiesInRange(validStartTime, validEndTime).ToList();
                    foreach (Duty d in duties)
                    {
                        d.Status = (byte?)DutyStatus.Present;
                        _dutyDAO.Update(d);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
