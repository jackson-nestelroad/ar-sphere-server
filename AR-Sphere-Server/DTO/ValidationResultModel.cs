using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
	public class ValidationResultModel
	{
		public Dictionary<string, List<string>> Errors { get; set; }

		public ValidationResultModel(Dictionary<string, List<string>> errors)
		{
			Errors = errors;
		}
	}
}
