using ARSphere.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Middleware.ExceptionHandling
{
	/// <summary>
	/// <para>Custom exception handler middleware for when the <code>HttpStatusCodeException</code> class is thrown
	/// from an API controller.</para>
	/// </summary>
	public class HttpStatusCodeExceptionHandler
	{
		private readonly RequestDelegate _next;

		public HttpStatusCodeExceptionHandler(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch(HttpStatusCodeException ex)
			{
				context.Response.Clear();
				context.Response.StatusCode = ex.StatusCode;
				context.Response.ContentType = ex.ContentType;

				await context.Response.WriteAsync(ex.Message);

				return;
			}
		}
	}
}
