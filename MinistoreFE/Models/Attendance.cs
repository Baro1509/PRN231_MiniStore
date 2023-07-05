using System;
using System.Collections.Generic;

namespace MinistoreFE.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public string StaffId { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public bool? IsCheckIn { get; set; }

        public virtual Staff Staff { get; set; } = null!;
    }
}
