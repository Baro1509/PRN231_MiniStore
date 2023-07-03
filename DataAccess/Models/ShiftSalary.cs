using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ShiftSalary
    {
        public int ShiftSalaryId { get; set; }
        public string AssignedTo { get; set; } = null!;
        public string ApprovedBy { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? Status { get; set; }

        public virtual Staff ApprovedByNavigation { get; set; } = null!;
        public virtual Staff AssignedToNavigation { get; set; } = null!;
    }
}
