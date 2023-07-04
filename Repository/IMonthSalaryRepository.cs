using DataAccess.Models;

namespace Repository
{
	public interface IMonthSalaryRepository
	{
		public bool Create(string staffId, string managerId, int month, int year);
		public MonthSalary? Get(string staffId, int month, int year);
		public List<MonthSalary> Get(string staffId);

	}
}
