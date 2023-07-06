namespace MinistoreAPI.Request
{
	public class MonthSalaryRequest
	{
		public string StaffId { get; set; }
		public string ManagerId { get; set; }
		public int Month { get; set; } = 0;
		public int Year { get; set; } = 0;
	}
}
