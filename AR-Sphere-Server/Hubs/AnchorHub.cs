using ARSphere.DAL;
using ARSphere.DTO;
using ARSphere.Middleware.Validation;
using ARSphere.Models;
using Microsoft.AspNetCore.SignalR;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Hubs
{
    public partial class MasterHub : Hub<IClient>
    {
        public Task CreateAnchor(NewAnchorModel model)
        {
            return _anchorService.CreateAnchor(model);
        }

        public AnchorViewModel GetAnchor(string id)
        {
            var anchor = _anchorService.GetById(id);
            if (anchor == null)
            {
                throw new HubException($"Could not locate anchor id = {id}.");
            }
            return anchor;
        }

        public AnchorViewModel GetLastAnchor()
        {
            var anchor = _anchorService.GetLast();
            if (anchor == null)
            {
                throw new HubException($"No last anchor exists.");
            }
            return anchor;
        }

        public List<AnchorViewModel> GetAnchorsNear(Coordinate loc, double rad)
        {
            var res = _anchorService.GetAnchorsNear(loc, rad);
            if (res != null) 
                return res;
            else 
                throw new HubException("No anchors near this location.");
        }
    }
}
