using DataAccess.Models;

namespace Repository
{
    public interface IAttendanceRepository
    {
        public bool CreateCheckIn(string staffId);
        public bool CreateCheckOut(string staffId);
        public List<Attendance> GetAttendancesByStaff(string staffId);
    }
}
