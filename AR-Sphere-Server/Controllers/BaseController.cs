using ARSphere.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Controllers
{
	/// <summary>
	/// <para>Minimal API controller for receiving and sending responses to the user.</para>
	/// </summary>
	public class BaseController : ControllerBase
	{
		/// <summary>
		/// <para>Returns a default message saying the API was used incorretly.</para>
		/// </summary>
		/// <returns></returns>
		protected ResultViewModel IncorrectUsage()
		{
			return ErrorResult("Incorrect usage.");
		}

		/// <summary>
		/// <para>Returns an error message with a 404 response code.</para>
		/// </summary>
		/// <param name="message"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		protected ResultViewModel ErrorResult(string message = "Not found.", int code = 404)
		{
			return Result(message, false, code);
		}

		/// <summary>
		/// <para>Returns a custom message to the user.</para>
		/// </summary>
		/// <param name="message"></param>
		/// <param name="success"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		protected ResultViewModel MessageResult(string message, bool success = true, int code = 200)
		{
			return Result(message, success, code);
		}

		/// <summary>
		/// <para>Returns a single <c>ViewModel</c> to the user.</para>
		/// </summary>
		/// <param name="singleResult"></param>
		/// <returns></returns>
		protected ResultViewModel SingleResult(BaseViewModel singleResult)
		{
			return Result(singleResult);
		}

		/// <summary>
		/// <para>Returns multiple <c>ViewModel</c>'s to the user.</para>
		/// </summary>
		/// <param name="multipleResults"></param>
		/// <returns></returns>
		protected ResultViewModel MultipleResults(IEnumerable<BaseViewModel> multipleResults)
		{
			return Result(multipleResults);
		}

		/// <summary>
		/// <para>Sends a result directly, formatting it as a <c>ResultViewModel</c>.</para>
		/// </summary>
		/// <param name="result"></param>
		/// <param name="success"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		protected ResultViewModel Result(object result, bool success = true, int code = 200)
		{
			Response.StatusCode = code;
			return new ResultViewModel(success, result);
		}
	}
}
