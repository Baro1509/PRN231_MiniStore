namespace MinistoreFE.Request
{
	public class MonthSalaryRequest
	{
		public string StaffId { get; set; }
		public string ManagerId { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
	}
}
