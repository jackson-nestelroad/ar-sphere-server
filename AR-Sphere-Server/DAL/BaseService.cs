using ARSphere.Context;
using ARSphere.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	/// <summary>
	/// <para>Minimal implementation of an API service to be used by API controllers and hubs.</para>
	/// </summary>
	/// <typeparam name="Entity">Database Entity (from DatabaseContext) this service works with.</typeparam>
	/// <typeparam name="ViewModel">ViewModel equivalent to Entity</typeparam>
	public abstract class BaseService<Entity, ViewModel> where ViewModel : BaseViewModel
	{
		protected readonly DatabaseContext _context;

		public BaseService(DatabaseContext context)
		{
			_context = context;
		}
	}
}
