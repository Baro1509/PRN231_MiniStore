namespace Repository
{
    public interface IAttendanceRepository
    {
        public bool CreateCheckIn(string staffId);
        public bool CreateCheckOut(string staffId);
    }
}
