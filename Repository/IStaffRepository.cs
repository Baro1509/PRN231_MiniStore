using DataAccess.Models;

namespace Repository
{
	public interface IStaffRepository
	{
		public Staff Login(string username, string password);
		public Staff? GetStaff(string id);
	}
}
