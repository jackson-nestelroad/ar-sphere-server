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

        public IEnumerable<AnchorViewModel> GetNearbyAnchors(double longitude, double latitude)
        {
            CurrentClient.SetLocation(longitude, latitude);
            return _anchorService.GetInRadius(CurrentClient.Location, Radius);
        }

        public async Task CreateAnchor(NewAnchorModel anchor)
        {
            var newAnchor = await _anchorService.CreateAndGet(anchor, CurrentClient.UserId);
            Dispatch(newAnchor.Location, c => c.NewNearbyAnchor(newAnchor));
        }

        public void ToggleLikeAnchor(string anchorId)
        {
            var updateView = _anchorService.ToggleLike(anchorId, CurrentClient.UserId);
            Dispatch(updateView.Location, c => c.UpdateAnchorLikes(updateView));
        }

        public void DeleteAnchor(string anchorId)
        {
            var deleteView = _anchorService.Delete(anchorId);
            Dispatch(deleteView.Location, c => c.AnchorDeleted(deleteView));
        }
    }
}
