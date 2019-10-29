using ARSphere.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Hubs
{
    public partial class MasterHub : Hub<IClient>
    {
        // TODO: Replace with token authentication on connect
        // Will authenticate user and get their user ID automatically
        // For now, send what user ID to use for prototyping
        public Task SetUserId(int id)
        {
            CurrentClient.UserId = id;
            return Task.CompletedTask;
        }

        public UserViewModelPrivate GetProfileData()
        {
            var data = _userService.GetPrivateById(CurrentClient.UserId);
            if(data == null)
            {
                throw new HubException($"User {CurrentClient.UserId} does not exist.");
            }
            return data;
        }
    }
}
