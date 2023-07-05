namespace DataAccess.Models
{
	public partial class Attendance
	{
		public int AttendanceId { get; set; }
		public string StaffId { get; set; } = null!;
		public DateTime CreatedTime { get; set; }
		public bool IsCheckIn { get; set; }
		public bool? Status { get; set; }

		public virtual Staff Staff { get; set; } = null!;
	}
}
