using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARSphere.DAL;
using ARSphere.DTO;
using Microsoft.AspNetCore.SignalR;

namespace ARSphere.Hubs
{
    public partial class MasterHub : Hub<IClient>
    {
        private IAnchorService _anchorService;
        private Dictionary<string, Client> ClientMap;

        private Client CurrentClient => ClientMap[Context.ConnectionId];

        public MasterHub(
            IAnchorService anchorService)
        {
            _anchorService = anchorService;
            ClientMap = new Dictionary<string, Client>();
        }

        public string Ping(string message)
        {
            return message;
        }

        public override Task OnConnectedAsync()
        {
            ClientMap.Add(Context.ConnectionId, new Client
            {
                X = double.NaN,
                Y = double.NaN
            });
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ClientMap.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
