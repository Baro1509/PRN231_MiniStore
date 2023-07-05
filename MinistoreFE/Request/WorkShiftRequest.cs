namespace MinistoreFE.Request
{
	public class WorkShiftRequest
	{
		public DateTime Date { get; set; }
		public string ManagerId { get; set; }
		public int ShiftOrder { get; set; }
		public WorkShiftRequest(DateTime date, string managerId, int shiftOrder)
		{
			Date = date;
			ManagerId = managerId;
			ShiftOrder = shiftOrder;
		}
	}
}
