using ARSphere.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Controllers
{
	public class BaseController : ControllerBase
	{
		protected ResultViewModel IncorrectUsage()
		{
			return ErrorResult("Incorrect usage.");
		}

		protected ResultViewModel ErrorResult(string message = "Not found.", int code = 404)
		{
			return Result(message, false, code);
		}

		protected ResultViewModel MessageResult(string message, bool success = true, int code = 200)
		{
			return Result(message, success, code);
		}

		protected ResultViewModel SingleResult(BaseViewModel singleResult)
		{
			return Result(singleResult);
		}

		protected ResultViewModel MultipleResults(IEnumerable<BaseViewModel> multipleResults)
		{
			return Result(multipleResults);
		}

		protected ResultViewModel Result(object result, bool success = true, int code = 200)
		{
			Response.StatusCode = code;
			return new ResultViewModel(success, result);
		}
	}
}
