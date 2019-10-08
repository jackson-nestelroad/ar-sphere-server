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
		protected JsonResult IncorrectUsage()
		{
			return ErrorResult("Incorrect usage.");
		}

		protected JsonResult ErrorResult(string message = "Not found.", int code = 404)
		{
			return Result(message, false, code);
		}

		protected JsonResult MessageResult(string message, bool success = true, int code = 200)
		{
			return Result(message, success, code);
		}

		protected JsonResult SingleResult(BaseViewModel singleResult)
		{
			return Result(singleResult);
		}

		protected JsonResult MultipleResults(IEnumerable<BaseViewModel> multipleResults)
		{
			return Result(multipleResults);
		}

		protected JsonResult Result(object result, bool success = true, int code = 200)
		{
			Response.StatusCode = code;

			return new JsonResult(new
			{
				Success = false,
				Result = result
			});
		}
	}
}
