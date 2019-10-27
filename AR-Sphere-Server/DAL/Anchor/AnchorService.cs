using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using ARSphere.Entities;
using ARSphere.Middleware.Validation;
using ARSphere.Models;
using ARSphere.Models.Helpers;
using ARSphere.Persistent;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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
        public AnchorService(DatabaseContext _context, IValidationService _validation) : base(_context, _validation) { }

        public AnchorViewModel GetById(string id)
        {
            var query = from anchor in _context.Anchors
                            .Where(a => a.Id == id)
                        from user in _context.Users
                            .Where(u => u.Id == anchor.CreatedBy)
                            .DefaultIfEmpty()
                        from model in _context.ARModels
                            .Where(m => m.Id == anchor.Model)
                            .DefaultIfEmpty()
                        from promotion in _context.Promotions
                            .Where(p => p.Id == model.Promotion)
                            .DefaultIfEmpty()
                        from sponsor in _context.Sponsors
                            .Where(s => s.Id == promotion.Sponsor)
                            .DefaultIfEmpty()
                        select anchor.ToViewModel(user, model, promotion, sponsor);
            return query.FirstOrDefault();
        }

        public async Task CreateAnchor(NewAnchorModel model)
        {
            _context.Anchors.Add(model.ToEntity());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public AnchorViewModel GetLast()
        {
            if (!_context.Anchors.Any())
            {
                return null;
            }

            var mostRecent = _context.Anchors.Max(anchor => anchor.CreatedAt);

            var query = from anchor in _context.Anchors
                            .Where(a => a.CreatedAt == mostRecent)
                        from user in _context.Users
                            .Where(u => u.Id == anchor.CreatedBy)
                            .DefaultIfEmpty()
                        from model in _context.ARModels
                            .Where(m => m.Id == anchor.Model)
                            .DefaultIfEmpty()
                        from promotion in _context.Promotions
                            .Where(p => p.Id == model.Promotion)
                            .DefaultIfEmpty()
                        from sponsor in _context.Sponsors
                            .Where(s => s.Id == promotion.Sponsor)
                            .DefaultIfEmpty()
                        select anchor.ToViewModel(user, model, promotion, sponsor);
            return query.FirstOrDefault();
        }

        public IEnumerable<AnchorViewModel> GetAnchorsInRadius(Point location, double radius)
        {
            var query = from anchor in _context.Anchors
                            .Where(a => a.Location.IsWithinDistance(location, radius))
                        from user in _context.Users
                            .Where(u => u.Id == anchor.CreatedBy)
                            .DefaultIfEmpty()
                        from model in _context.ARModels
                            .Where(m => m.Id == anchor.Model)
                            .DefaultIfEmpty()
                        from promotion in _context.Promotions
                            .Where(p => p.Id == model.Promotion)
                            .DefaultIfEmpty()
                        from sponsor in _context.Sponsors
                            .Where(s => s.Id == promotion.Sponsor)
                            .DefaultIfEmpty()
                        select anchor.ToViewModel(user, model, promotion, sponsor);
            return query.ToList();
        }
    }
}
