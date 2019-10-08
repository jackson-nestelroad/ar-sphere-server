using ARSphere.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	public interface IUserService
	{
		public UserViewModel FindById(int id);
	}
}
