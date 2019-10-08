﻿using System;
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

		public string Username { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Date)]
		public DateTime RegisteredAt { get; set; }
	}
}
