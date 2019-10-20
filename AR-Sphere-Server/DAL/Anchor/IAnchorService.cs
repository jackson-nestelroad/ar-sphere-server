using ARSphere.DTO;
using ARSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	public interface IAnchorService
	{
		public AnchorViewModel GetById(string id);
		public Task CreateAnchor(NewAnchorModel model);
        public AnchorViewModel GetLast();
	}
}
