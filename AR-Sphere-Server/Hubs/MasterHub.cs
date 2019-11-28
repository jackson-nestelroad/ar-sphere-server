using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARSphere.DAL;
using ARSphere.DTO;
using Microsoft.AspNetCore.SignalR;
using NetTopologySuite.Geometries;

namespace ARSphere.Hubs
{
    public partial class MasterHub : Hub<IClient>
    {
        private readonly IAnchorService _anchorService;
        private readonly IUserService _userService;
        private static readonly Dictionary<string, Client> ClientMap = new Dictionary<string, Client>();

        private Client CurrentClient => ClientMap[Context.ConnectionId];

        private readonly int Radius = 100;

        public MasterHub(
            IAnchorService anchorService,
            IUserService userService)
        {
            _anchorService = anchorService;
            _userService = userService;
        }

        public string Ping(string message)
        {
            return message;
        }

        public override Task OnConnectedAsync()
        {
            ClientMap.Add(Context.ConnectionId, new Client());
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ClientMap.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        private IEnumerable<string> ConnectionsInRange(Point center)
        {
            return from kvp in ClientMap
                   where kvp.Value.Location.IsWithinDistance(center, Radius)
                   select kvp.Key;
        }

        private void Dispatch(Point center, Action<IClient> dispatch)
        {
            var connections = ConnectionsInRange(center);
            foreach (string connectionId in connections)
            {
                dispatch.Invoke(Clients.Client(connectionId));
            }
        }
    }
}
