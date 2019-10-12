using ARSphere.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	interface IAnchorService
	{
		public AnchorViewModel GetById(int id);
	}
}
