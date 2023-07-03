using DataAccess;
using DataAccess.Constants;
using DataAccess.Models;

namespace Repository.Implement
{
    public class DutyRepository : IDutyRepository
    {
        private readonly AttendanceDAO _attendanceDAO;
        private readonly DutyDAO _dutyDAO;

        public DutyRepository(AttendanceDAO attendanceDAO, DutyDAO dutyDAO)
        {
            _attendanceDAO = attendanceDAO;
            _dutyDAO = dutyDAO;
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
