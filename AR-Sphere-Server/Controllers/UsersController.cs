using ARSphere.Context;
using ARSphere.DAL;
using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using ARSphere.Entities;
using ARSphere.Middleware.Validation;
using ARSphere.Models;
using ARSphere.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Produces("application/json")]
	public class UsersController : BaseController
	{
		private readonly IUserService _service;

		public UsersController(IUserService service)
		{
			_service = service;
		}

		[HttpGet("{id}")]
		public ResultViewModel GetById(int id)
		{
			var entity = _service.FindById(id);

			if(entity == null)
			{
				return ErrorResult($"User id = {id} does not exist.");
			}

			return SingleResult(entity);
		}

		[HttpPost]
		[ValidateModel]
		public ResultViewModel Test([FromForm] UserModel user)
		{
			return Result(user.ToEntity());
		}
	}
}
