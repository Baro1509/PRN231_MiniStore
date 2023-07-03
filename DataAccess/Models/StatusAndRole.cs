using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models {
    public enum Status {
        Available = 1,
        Deleted = 0
    }

    public enum WorkshiftStatus {
        Absent = 0,
        Present = 1,
        Late = 2
    }

    public static class RoleId {
        public static string MA = "Manager";
        public static string SG = "Safe Guard";
        public static string SM = "Salesman";
    }
}
