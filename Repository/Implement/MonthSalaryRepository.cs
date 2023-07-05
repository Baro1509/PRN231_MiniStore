using DataAccess;
using DataAccess.Models;

namespace Repository.Implement
{
	public class MonthSalaryRepository : IMonthSalaryRepository
	{
		private readonly DutyDAO _dutyDAO;
		private readonly StaffDAO _staffDAO;
		private readonly ShiftSalaryDAO _shiftSalaryDAO;
		private readonly MonthSalaryDAO _monthSalaryDAO;


		public MonthSalaryRepository(DutyDAO dutyDAO, StaffDAO staffDAO, ShiftSalaryDAO shiftSalaryDAO, MonthSalaryDAO monthSalaryDAO)
		{
			_dutyDAO = dutyDAO;
			_staffDAO = staffDAO;
			_shiftSalaryDAO = shiftSalaryDAO;
			_monthSalaryDAO = monthSalaryDAO;
		}
		public bool Create(string staffId, string managerId, int month, int year)
		{
			DateTime from = new DateTime(year, month, 1);
			DateTime to = from.AddMonths(1);
			var shiftSalary = _shiftSalaryDAO.GetStaffSalaryByTime(staffId, from);
			var staff = _staffDAO.GetStaff(staffId);
			var manager = _staffDAO.GetStaff(managerId);
			if (shiftSalary != null && staff != null && manager != null)
			{
				var found = Get(staffId, month, year);
				decimal salary = shiftSalary.Salary;
				var duties = _dutyDAO.GetStaffCompletedDuties(staffId, from, to);
				if (duties.Any())
				{
					decimal sum = 0;
					foreach (Duty d in duties)
					{
						sum += (salary * (decimal)d.Shift.Coefficient) + d.Shift.Bonus;
					}
					if (found == null)
					{
						MonthSalary monthSalary = new MonthSalary();
						monthSalary.AssignedTo = staffId;
						monthSalary.ApprovedBy = managerId;
						monthSalary.StartTime = from;
						monthSalary.EndTime = to;
						monthSalary.Status = (byte?)DataAccess.Constants.Status.Available;
						monthSalary.Salary = sum;
						_monthSalaryDAO.Create(monthSalary);
					}
					else
					{
						if (found.Salary != sum)
						{
							found.Salary = sum;
							_monthSalaryDAO.Update(found);
						}
					}
					return true;
				}
			}
			return false;
		}
		public bool Update(MonthSalary monthSalary)
		{
			var staff = _staffDAO.GetStaff(monthSalary.AssignedTo);
			var manager = _staffDAO.GetStaff(monthSalary.ApprovedBy);
			if (staff != null && manager != null && monthSalary.StartTime < monthSalary.EndTime)
			{
				_monthSalaryDAO.Update(monthSalary);
				return true;
			}
			return false;
		}

		public MonthSalary? Get(string staffId, int month, int year)
		{
			DateTime from = new DateTime(year, month, 1);
			DateTime to = from.AddMonths(1);
			return _monthSalaryDAO.GetAll().FirstOrDefault(m => m.AssignedTo.Equals(staffId) && m.StartTime == from && m.EndTime == to);
		}

		public List<MonthSalary> Get(string staffId)
		{
			return _monthSalaryDAO.GetAll().Where(m => m.AssignedTo.Equals(staffId)).ToList();
		}
	}
}
