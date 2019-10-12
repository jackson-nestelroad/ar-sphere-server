using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
	/// <summary>
	/// <para>Public data format for presenting errors that occurred in model validation.</para>
	/// </summary>
	public class ValidationResultModel
	{
		public Dictionary<string, List<string>> Errors { get; set; }

		public ValidationResultModel(Dictionary<string, List<string>> errors)
		{
			Errors = errors;
		}
	}
}
