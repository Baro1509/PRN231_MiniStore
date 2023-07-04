using DataAccess;
using DataAccess.Constants;
using DataAccess.Models;

namespace Repository.Implement
{
	public class ShiftSalaryRepository : IShiftSalaryRepository
	{
		private readonly ShiftSalaryDAO _shiftSalaryDAO;
		private readonly StaffDAO _staffDAO;


		public ShiftSalaryRepository(ShiftSalaryDAO shiftSalaryDAO, StaffDAO staffDAO)
		{
			_shiftSalaryDAO = shiftSalaryDAO;
			_staffDAO = staffDAO;
		}
		public bool Create(ShiftSalary shiftSalary)
		{
			var staff = _staffDAO.GetStaff(shiftSalary.AssignedTo);
			var manager = _staffDAO.GetStaff(shiftSalary.ApprovedBy);
			if (staff != null && manager != null && shiftSalary.Salary >= 0)
			{
				shiftSalary.EndTime = null;
				shiftSalary.Status = (byte?)Status.Available;
				_shiftSalaryDAO.Create(shiftSalary);
				return true;
			}
			return false;
		}

		public ShiftSalary? GetCurrentShiftSalary(string staffId)
		{
			return _shiftSalaryDAO.GetAll().OrderByDescending(s => s.StartTime).FirstOrDefault(s => s.AssignedTo.Equals(staffId) && s.StartTime <= DateTime.Now);
		}

		public bool Update(ShiftSalary shiftSalary)
		{
			if (shiftSalary.ShiftSalaryId != 0)
			{
				var found = _shiftSalaryDAO.Get(shiftSalary.ShiftSalaryId);
				if (found != null)
				{
					found.Salary = shiftSalary.Salary;
					found.StartTime = shiftSalary.StartTime;
					found.EndTime = shiftSalary.EndTime;
					_shiftSalaryDAO.Update(found);
					return true;
				}
				return false;
			}
			var currentSalary = GetCurrentShiftSalary(shiftSalary.AssignedTo);
			if (currentSalary == null)
			{
				return Create(shiftSalary);
			}
			if (shiftSalary.StartTime >= currentSalary.StartTime)
			{
				currentSalary.EndTime = shiftSalary.StartTime;
				_shiftSalaryDAO.Update(currentSalary);
				return Create(shiftSalary);
			}
			return false;
		}
		public bool Delete(int shiftSalaryId)
		{
			var found = _shiftSalaryDAO.Get(shiftSalaryId);
			DateTime now = DateTime.Now;
			DateTime minStartTime = new DateTime(now.Year, now.Month, 1);
			if (found != null && found.StartTime >= minStartTime)
			{
				_shiftSalaryDAO.Delete(found);
				return true;
			}
			return false;
		}

	}
}
