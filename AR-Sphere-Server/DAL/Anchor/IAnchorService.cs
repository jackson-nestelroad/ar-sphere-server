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
        public Task CreateAnchor(NewAnchorModel model, int creatorId);
        public Task<AnchorViewModel> CreateAnchorAndGet(NewAnchorModel anchor, int creatorId);
        public AnchorViewModel GetLast();
        public IEnumerable<AnchorViewModel> GetAnchorsInRadius(Point location, double radius = 100);
	}
}
