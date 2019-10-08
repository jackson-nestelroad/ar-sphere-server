using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Entities
{
	public class User
	{	
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[Required(ErrorMessage = "Username required.")]
		[StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters.")]
		public string Username { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email address required.")]
		[RegularExpression("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\x01 -\x08\x0b\x0c\x0e -\x1f\x21\x23 -\x5b\x5d -\x7f] |\\[\x01-\x09\x0b\x0c\x0e-\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\\])",
			ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }
		
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password required.")]
		[MaxLength(30, ErrorMessage = "Password cannot exceed 30 characters.")]
		[StringLength(31, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters.")]
		public string Password { get; set; }

		[DataType(DataType.Date)]
		public DateTime RegisteredAt { get; set; }
	}
}
