namespace MinistoreAPI.Request
{
	public class WorkShiftRequest
	{
		public DateTime Date { get; set; }
		public string ManagerId { get; set; }
		public int ShiftOrder { get; set; }
	}
}
