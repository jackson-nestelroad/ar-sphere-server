using ARSphere.DTO;
using ARSphere.Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
    public interface IAnchorService
    {
        public AnchorViewModel GetById(string id);
        public Task Create(NewAnchorModel model, int creatorId);
        public Task<AnchorViewModel> CreateAndGet(NewAnchorModel anchor, int creatorId);
        public AnchorViewModel GetLast();
        public IEnumerable<AnchorViewModel> GetInRadius(Point location, double radius);
        public AnchorLikedViewModel ToggleLike(string anchorId, int userId);
        public AnchorDeletedViewModel Delete(string id);
	}
}
