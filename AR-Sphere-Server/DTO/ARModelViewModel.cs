using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
	/// <summary>
	/// <para>Public data format for presenting an ARModel entity.</para>
	/// </summary>
	public class ARModelViewModel : BaseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public PromotionViewModel Promotion { get; set; }
	}
}
