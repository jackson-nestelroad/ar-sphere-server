﻿using ARSphere.DAL;
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

        public IEnumerable<AnchorViewModel> GetNearbyAnchors(double longitude, double latitude)
        {
            return _anchorService.GetAnchorsInRadius(new Point(longitude, latitude));
        }
    }
}
