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

    public enum Status
    {
        Available = 1,
        Deleted = 0
    }

    public enum AttendanceStatus
    {
        CheckIn = 0,
        CheckOut = 1,
        InvalidCheckin = 2,
        InvalidCheckout = 3
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
}
