using System;
using System.Collections.Generic;

namespace MinistoreFE.Models
{
    public partial class MonthlyBonu
    {
        public int MonthlyBonusId { get; set; }
        public string AssignedTo { get; set; } = null!;
        public string ApprovedBy { get; set; } = null!;
        public decimal Bonus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? Status { get; set; }

        public virtual Staff ApprovedByNavigation { get; set; } = null!;
        public virtual Staff AssignedToNavigation { get; set; } = null!;
    }
}
