using ARSphere.DTO;
using ARSphere.Middleware.Validation;
using ARSphere.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
    /// <summary>
    /// <para>Minimal implementation of an API service to be used by API controllers and hubs.</para>
    /// </summary>
    public abstract class BaseService
    {
        protected readonly DatabaseContext _context;
        protected readonly IValidationService _validation;

        public BaseService(DatabaseContext context, IValidationService validation)
        {
            _context = context;
            _validation = validation;
        }
    }
}
