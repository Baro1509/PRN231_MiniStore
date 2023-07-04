using DataAccess.Models;

namespace Repository
{
	public interface IDutyRepository
	{
		public bool UpdateLatestDutyStatus(string staffId);
		public bool CreateDuty(string staffId, int workShiftId);
		public List<Duty>? GetStaffWeekDuties(string staffId, DateOnly dateOnly);
		public bool UpdateDuty(Duty duty);
		public bool DeleteDuty(int dutyId);
	}
}
