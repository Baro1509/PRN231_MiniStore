using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
