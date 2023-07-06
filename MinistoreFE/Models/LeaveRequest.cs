using System;
using System.Collections.Generic;

namespace MinistoreFE.Models
{
    public partial class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public string RequestedBy { get; set; } = null!;
        public string ApprovedBy { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte? Status { get; set; }

        public virtual Staff ApprovedByNavigation { get; set; } = null!;
        public virtual Staff RequestedByNavigation { get; set; } = null!;
    }
}
