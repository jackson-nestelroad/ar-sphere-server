using ARSphere.DAL;
using ARSphere.DTO;
using ARSphere.Entities;
using ARSphere.Middleware.ExceptionHandling;
using ARSphere.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Controllers
{
	/// <summary>
	/// <para>API controller for interacting with User data.</para>
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	[Produces("application/json")]
	public class UsersController
	{
		private readonly IUserService _service;

		public UsersController(IUserService service)
		{
			_service = service;
		}

		[HttpGet("{id}")]
		public UserViewModel GetById(int id)
		{
			var entity = _service.GetById(id);

			if(entity == null)
			{
				throw new HttpStatusCodeException(404, $"User id = {id} does not exist.");
			}

			return entity;
		}

		[HttpPost]
		public User Test([FromBody] RegisterModel user)
		{
			return _service.RegisterUser(user);
		}
	}
}
