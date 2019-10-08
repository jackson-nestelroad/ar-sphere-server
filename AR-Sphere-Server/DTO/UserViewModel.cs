using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
	public class UserViewModel : BaseViewModel
	{
		public long Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime RegisteredAt { get; set; }
	}
}
