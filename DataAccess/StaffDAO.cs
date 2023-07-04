using DataAccess.Models;

namespace DataAccess
{
	public class StaffDAO : DataAccessBase<Staff>
	{
		public Staff? GetStaff(string id)
		{
			return GetAll().FirstOrDefault(s => s.StaffId.Equals(id));
		}
	}
}
