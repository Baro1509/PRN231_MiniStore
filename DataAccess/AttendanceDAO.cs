using DataAccess.Models;

namespace DataAccess
{
    public class AttendanceDAO : DataAccessBase<Attendance>
    {
        public Attendance? GetLatestAttendance(string staffId)
        {
            return GetAll().OrderBy(a => a.AttendanceId).LastOrDefault(a => a.StaffId.Equals(staffId) && a.Status == true);
        }
        public Attendance? GetLatestCheckIn(string staffId)
        {
            return GetAll().OrderBy(a => a.AttendanceId).LastOrDefault(a => a.StaffId.Equals(staffId) && a.Status == true && a.IsCheckIn);
        }
    }
}
