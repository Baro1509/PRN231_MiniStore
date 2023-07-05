﻿using System;
using System.Collections.Generic;

namespace MinistoreFE.Models
{
    public partial class Duty
    {
        public int DutyId { get; set; }
        public int ShiftId { get; set; }
        public int RoleId { get; set; }
        public string AssignedTo { get; set; } = null!;
        public byte? Status { get; set; }

        public virtual staff AssignedToNavigation { get; set; } = null!;
        public virtual WorkShift Shift { get; set; } = null!;
    }
}
