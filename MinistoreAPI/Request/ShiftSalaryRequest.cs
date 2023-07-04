namespace MinistoreAPI.Request
{
	public class ShiftSalaryRequest
	{
		public int ShiftSalaryId { get; set; } = 0;
		public string AssignedTo { get; set; } = null!;
		public string ApprovedBy { get; set; } = null!;
		public decimal Salary { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public byte? Status { get; set; }
	}
}
