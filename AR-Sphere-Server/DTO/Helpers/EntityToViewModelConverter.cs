using ARSphere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO.Helpers
{
	public static class EntityToViewModelConverter
	{
		public static UserViewModel ToViewModel(this User user)
		{
			return new UserViewModel
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				RegisteredAt = user.RegisteredAt
			};
		}
	}
}
