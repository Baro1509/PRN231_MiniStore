using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonthSalary
    {
        public int MonthSalaryId { get; set; }
        public string AssignedTo { get; set; } = null!;
        public string ApprovedBy { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? Status { get; set; }

        public virtual staff ApprovedByNavigation { get; set; } = null!;
        public virtual staff AssignedToNavigation { get; set; } = null!;
    }
}
