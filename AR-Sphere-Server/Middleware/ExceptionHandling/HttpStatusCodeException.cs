using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ARSphere.Middleware.ExceptionHandling
{
	/// <summary>
	/// <para>Exceptions that occur in the API Controllers that change the response's status code and output.</para>
	/// </summary>
	public class HttpStatusCodeException : Exception
	{
		public int StatusCode { get; set; }
		public string ContentType { get; set; } = @"text/plain";

		public HttpStatusCodeException(int statusCode)
		{
			StatusCode = statusCode;
		}

		public HttpStatusCodeException(int statusCode, string message) : base(message)
		{
			StatusCode = statusCode;
		}

		public HttpStatusCodeException(int statusCode, object errorObject) : this(statusCode, JsonSerializer.Serialize(errorObject))
		{
			ContentType = @"application/json";
		}
	}
}
