using ARSphere.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Middleware.Validation
{
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var errorList = context.ModelState
					.Where(kvp => kvp.Value.Errors.Count > 0)
					.ToDictionary(
						kvp => kvp.Key,
						kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
					);
				context.HttpContext.Response.StatusCode = 422;
				context.Result = new ObjectResult(new ResultViewModel(false, new ValidationResultModel(errorList)));
			}
		}
	}
}
