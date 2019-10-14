using ARSphere.DTO;
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

		public BaseService(DatabaseContext context)
		{
			_context = context;
		}
	}
}
