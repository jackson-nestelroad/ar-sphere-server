using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Entities
{
	/// <summary>
	/// <para>Represents one row in the Sponsors table in the database.</para>
	/// </summary>
	public class Sponsor
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
