using ARSphere.Context;
using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	public class UserService : IUserService
	{
		private readonly DatabaseContext _context;

		public UserService(DatabaseContext context)
		{
			_context = context;
		}

		public UserViewModel FindById(int id)
		{
			var selection = from user in _context.Users
							where user.Id == id
							select user;
			if(!selection.Any())
			{
				return null;
			}

			return selection.First().ToViewModel();
		}
	}
}
