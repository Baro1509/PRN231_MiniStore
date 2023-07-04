using DataAccess.Models;

namespace Repository
{
	public interface IShiftSalaryRepository
	{
		public ShiftSalary? GetCurrentShiftSalary(string staffId);
		public bool Update(ShiftSalary shiftSalary);
		public bool Create(ShiftSalary shiftSalary);
		public bool Delete(int shiftSalaryId);
	}
}
