namespace DataAccess.Models
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
}
