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
        public AnchorService(DatabaseContext _context) : base(_context) { }

        public AnchorViewModel GetById(string id)
        {
            var selection = from anchor in _context.Anchors
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

            return selection.Any() ? selection.First() : null;
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
            if (_context.Anchors.Any())
            {
                var max = _context.Anchors.Select(anchor => anchor.CreatedAt).Max();
                var s = _context.Anchors.First(anchor => anchor.CreatedAt == max);
                return GetById(s.Id);
            }
            else
            {
                return null;
            }

        }

        /*
         * Returns list of AnchorViewModels near a radius. 
         * Should theoretically work.
         */
        public List<AnchorViewModel> GetAnchorsNear(Location loc, double rad)
        {
            List<AnchorViewModel> near = new List<AnchorViewModel>();

            if (_context.Anchors.Any())
            {
                _context.Anchors.ToList().ForEach(anchor =>
                {
                    if (anchor.Location.IsWithinDistance(anchor.Location, rad))
                    {
                        near.Add(GetById(anchor.Id));
                    }
                });

                return near;
            }
            else return null;
            
        }
    }
}
