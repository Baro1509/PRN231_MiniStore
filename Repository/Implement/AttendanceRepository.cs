using DataAccess;
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
            return true;
        }
        /*
         * 1. Find latest attendance has given staffId
         * 2. If found attendance has checkout status
         *      => Create new attendance with status is Checkout and Invalid & Return
         * 3. If found attendance has checkin status but created time is more than 16 hours ago
         *      => Update that attendance as Checkin and Invalid
         *      => Create new attendance with status is Checkout and Invalid & Return
         * 4. Create new attendance with status is Checkout & Return
         */
        public bool CreateCheckOut(string staffId)
        {
            return true;

        }
    }
}
