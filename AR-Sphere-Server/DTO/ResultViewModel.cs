using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
	/// <summary>
	/// <para>Public data format for all REST API responses.</para>
	/// <code>
	/// {
	///		Success: boolean,
	///		<br></br>
	///		Result: object
	/// }
	/// </code>
	/// </summary>
	public class ResultViewModel
	{
		public bool Success { get; set; }
		public object Result { get; set; }

		public ResultViewModel(bool success, object result)
		{
			Success = success;
			Result = result;
		}
	}
}
