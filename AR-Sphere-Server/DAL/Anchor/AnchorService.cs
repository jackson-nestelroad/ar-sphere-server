using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using ARSphere.Entities;
using ARSphere.Middleware.Validation;
using ARSphere.Models;
using ARSphere.Models.Helpers;
using ARSphere.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
	/// <summary>
	/// <para>API service to work with the Anchors table.</para>
	/// </summary>
	public class AnchorService : BaseService, IAnchorService
	{
		public AnchorService(DatabaseContext _context) : base(_context) { }

		public AnchorViewModel GetById(int id)
		{
			var selection = from anchor in _context.Anchors
							where anchor.Id == id
							join user in _context.Users on anchor.CreatedBy equals user.Id
							join model in _context.ARModels on anchor.Model equals model.Id
							join promotion in _context.Promotions on model.Promotion equals promotion.Id
							join sponsor in _context.Sponsors on promotion.Sponsor equals sponsor.Id
							select anchor.ToViewModel(user, model, promotion, sponsor);

			return selection.Any() ? selection.First() : null;
		}

		public async Task CreateAnchor(NewAnchorModel model)
		{
			_context.Anchors.Add(model.ToEntity());
			await _context.SaveChangesAsync();
		}
	}
}
