namespace DataAccess.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Attendances = new HashSet<Attendance>();
            Duties = new HashSet<Duty>();
            Invoices = new HashSet<Invoice>();
            LeaveRequestApprovedByNavigations = new HashSet<LeaveRequest>();
            LeaveRequestRequestedByNavigations = new HashSet<LeaveRequest>();
            MonthSalaryApprovedByNavigations = new HashSet<MonthSalary>();
            MonthSalaryAssignedToNavigations = new HashSet<MonthSalary>();
            MonthlyBonuApprovedByNavigations = new HashSet<MonthlyBonus>();
            MonthlyBonuAssignedToNavigations = new HashSet<MonthlyBonus>();
            ShiftSalaryApprovedByNavigations = new HashSet<ShiftSalary>();
            ShiftSalaryAssignedToNavigations = new HashSet<ShiftSalary>();
            WorkShifts = new HashSet<WorkShift>();
        }

        public string StaffId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte? Status { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Duty> Duties { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequestApprovedByNavigations { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequestRequestedByNavigations { get; set; }
        public virtual ICollection<MonthSalary> MonthSalaryApprovedByNavigations { get; set; }
        public virtual ICollection<MonthSalary> MonthSalaryAssignedToNavigations { get; set; }
        public virtual ICollection<MonthlyBonus> MonthlyBonuApprovedByNavigations { get; set; }
        public virtual ICollection<MonthlyBonus> MonthlyBonuAssignedToNavigations { get; set; }
        public virtual ICollection<ShiftSalary> ShiftSalaryApprovedByNavigations { get; set; }
        public virtual ICollection<ShiftSalary> ShiftSalaryAssignedToNavigations { get; set; }
        public virtual ICollection<WorkShift> WorkShifts { get; set; }
    }
}
