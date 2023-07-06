using DataAccess;
using DataAccess.Constants;
using DataAccess.Models;

namespace Repository.Implement
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceDAO _attendanceDAO;

        public AttendanceRepository(AttendanceDAO attendanceDAO)
        {
            _attendanceDAO = attendanceDAO;
        }

        /*
         *  1. Find latest attendance has given staffId
         *  2. If found attendance has checkin status => Update that attendance as Invalid
         *  3. Create new attendance with given staffId and attendance status is Checkin & Return
         */
        public bool CreateCheckIn(string staffId)
        {
            Attendance a = new Attendance();
            a.StaffId = staffId;
            a.CreatedTime = DateTime.Now;
            a.IsCheckIn = AttendanceStatus.CheckIn;
            a.Status = AttendanceStatus.IsValid;

            // GetCurrentShiftSalary latest attendance
            var latestAttendance = _attendanceDAO.GetLatestAttendance(staffId);
            if (latestAttendance != null && latestAttendance.IsCheckIn)
            {
                latestAttendance.Status = AttendanceStatus.IsInvalid;
                _attendanceDAO.Update(latestAttendance);
            }
            _attendanceDAO.Create(a);
            return true;
        }
        /*
         * 1. Find latest attendance has given staffId
         * 2. If found attendance has checkout status
         *      => Create new attendance with status is Checkout and Invalid & Return false
         * 3. If found attendance has checkin status but created time is more than 16 hours ago
         *      => Update that attendance as Checkin and Invalid
         *      => Create new attendance with status is Checkout and Invalid & Return false
         * 4. Create new attendance with status is Checkout & Return true
         */
        public bool CreateCheckOut(string staffId)
        {
            Attendance a = new Attendance();
            a.StaffId = staffId;
            a.CreatedTime = DateTime.Now;
            a.IsCheckIn = AttendanceStatus.CheckOut;
            a.Status = AttendanceStatus.IsValid;

            // GetCurrentShiftSalary latest attendance
            var latestAttendance = _attendanceDAO.GetLatestAttendance(staffId);
            if (latestAttendance != null)
            {
                if (latestAttendance.IsCheckIn == AttendanceStatus.CheckOut)
                {
                    a.Status = AttendanceStatus.IsInvalid;
                    _attendanceDAO.Create(a);
                    return false;
                }
                TimeSpan timeSpan = a.CreatedTime - latestAttendance.CreatedTime;
                if (latestAttendance.IsCheckIn && timeSpan.TotalHours >= 16)
                {
                    latestAttendance.Status = AttendanceStatus.IsInvalid;
                    _attendanceDAO.Update(latestAttendance);

                    a.Status = AttendanceStatus.IsInvalid;
                    _attendanceDAO.Create(a);
                    return false;
                }
            }
            _attendanceDAO.Create(a);
            return true;
        }

        public List<Attendance> GetAttendancesByStaff(string staffId)
        {
            return _attendanceDAO.GetAll().Where(a => a.StaffId.Equals(staffId)).ToList();
        }


    }
}
