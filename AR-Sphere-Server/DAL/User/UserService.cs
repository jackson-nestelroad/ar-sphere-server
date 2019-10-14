﻿using ARSphere.Context;
using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using ARSphere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	/// <summary>
	/// <para>API service to work with the Users table.</para>
	/// </summary>
	public class UserService : BaseService<User, UserViewModel>, IUserService
	{
		public UserService(DatabaseContext _context) : base(_context) { }

		public UserViewModel FindById(int id)
		{
			var selection = from user in _context.Users
							where user.Id == id
							select user;

			return selection.Any() ? selection.First().ToViewModel() : null;
		}
	}
}