using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models
{
	public class UserModel
	{
		[Required(ErrorMessage = "Username required.")]
		[StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Email address required.")]
		[RegularExpression("[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*",
			ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password required.")]
		[MaxLength(30, ErrorMessage = "Password cannot exceed 30 characters.")]
		[StringLength(31, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters.")]
		public string Password { get; set; }
	}
}
