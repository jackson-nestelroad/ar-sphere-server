using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Username required.")]
		[StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Email address required.")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password required.")]
		[MaxLength(30, ErrorMessage = "Password cannot exceed 30 characters.")]
		[StringLength(31, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters.")]
		public string Password { get; set; }
	}
}
