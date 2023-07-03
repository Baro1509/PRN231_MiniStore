namespace DataAccess.Constants
{
    public class Role
    {
        public static string SafeGuard = "SG";
        public static string SalesMan = "SM";
        public static string Manager = "MA";
        public static bool IsSalesMan(string role)
        {
            return SalesMan.Equals(role);
        }
        public static bool IsManager(string role)
        {
            return Manager.Equals(role);
        }
        public static bool IsSafeGuard(string role)
        {
            return SafeGuard.Equals(role);
        }
    }
    public class AttendanceStatus
    {
        public static bool CheckIn = true;
        public static bool CheckOut = false;
        public static bool IsValid = true;
        public static bool IsInvalid = false;
    }

    public enum Status
    {
        Available = 1,
        Deleted = 0
    }
    public enum ShiftSalaryStatus
    {
        Available = 1,
        Deleted = 0
    }
    public enum MonthSalaryStatus
    {
        NotPaid = 0,
        Paid = 1,
        Deleted = 2
    }
    public enum DutyStatus
    {
        Deleted = 0,
        Upcoming = 1,
        Present = 2,
        Absent = 3
    }
}
