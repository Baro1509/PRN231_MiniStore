﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class WorkShift
    {
        public WorkShift()
        {
            Duties = new HashSet<Duty>();
        }
        [Key]
        public int ShiftId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double? Coefficient { get; set; }
        public decimal? Bonus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public byte? Status { get; set; }

        public virtual Staff CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<Duty> Duties { get; set; }
    }
}
