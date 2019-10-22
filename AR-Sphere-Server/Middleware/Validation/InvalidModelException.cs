using ARSphere.Middleware.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Middleware.Validation
{
    /// <summary>
    /// <para>Exception to be thrown when model validation in the <code>ValidationService</code> fails.</para>
    /// </summary>
    public class InvalidModelException : HttpStatusCodeException
    {
        public InvalidModelException(ValidationService validationService)
            : base(StatusCodes.Status422UnprocessableEntity, validationService.ToObject())
        { }
    }
}
