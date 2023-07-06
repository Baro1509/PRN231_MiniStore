namespace DataAccess.Constants
{
    public class Role
    {
        public static string SafeGuard = "SG";
        public static string SalesMan = "SA";
        public static string Manager = "MG";
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
        public static string GetStatus(string role)
        {
            switch (role)
            {
                case "SG":
                    return "Safeguard";
                case "SA":
                    return "Salesman";
                case "MG":
                    return "Manager";
                default:
                    return "";
            }
        }
    }
    public class AttendanceStatus
    {
        public static bool CheckIn = true;
        public static bool CheckOut = false;
        public static bool IsValid = true;
        public static bool IsInvalid = false;
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
    public class DutyStatus
    {
        public static byte Upcoming { get; } = 0;
        public static byte Present { get; } = 1;

        public static byte Absent { get; } = 2;
        public static string GetStatus(byte status)
        {
            switch (status)
            {
                case 1:
                    return "Present";
                case 2:
                    return "Absent";
                default:
                    return "Upcoming";
            }
        }

    }
}
