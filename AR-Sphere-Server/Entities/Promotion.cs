using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Entities
{
	/// <summary>
	/// <para>Represents one row in the Promotions table in the database.</para>
	/// </summary>
	public class Promotion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Sponsor")]
		public int Sponsor { get; set; }

		public string Title { get; set; }

		public string Url { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		// MSSQL cannot store arrays or lists
		// ModelIDs will be stored as a comma-separated list of Model IDs
		// Use Models to get and set the comma-separated list as a List<int>

		[ForeignKey("ARModel")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ModelIDs { get; private set;  }

		[NotMapped]
		public List<int> Models
		{
			get
			{
				return ModelIDs.Split(',').Select(int.Parse).ToList();
			}
			set
			{
				ModelIDs = string.Join(',', Models);
			}
		}
	}
}
