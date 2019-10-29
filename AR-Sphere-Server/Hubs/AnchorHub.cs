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

        public async Task CreateAnchor(NewAnchorModel anchor)
        {
            var newAnchor = await _anchorService.CreateAnchorAndGet(anchor, CurrentClient.UserId);
            DispatchAnchor(newAnchor);
        }

        private void DispatchAnchor(AnchorViewModel anchor)
        {
            var connectionsInRange = from kvp in ClientMap
                                     where kvp.Value.Location.IsWithinDistance(anchor.Location, 100)
                                     select kvp.Key;
            foreach (string connectionId in connectionsInRange)
            {
                Clients.Client(connectionId).NewNearbyAnchor(anchor);
            }
        }

        public IEnumerable<AnchorViewModel> GetNearbyAnchors(double longitude, double latitude)
        {
            CurrentClient.SetLocation(longitude, latitude);
            return _anchorService.GetAnchorsInRadius(new Point(longitude, latitude) { SRID = 4326 });
        }
    }
}
