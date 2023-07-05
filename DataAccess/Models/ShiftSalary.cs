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
        public DateTime CreatedTime { get; set; }
        public byte? Status { get; set; }

        public virtual staff ApprovedByNavigation { get; set; } = null!;
        public virtual staff AssignedToNavigation { get; set; } = null!;
    }
}
