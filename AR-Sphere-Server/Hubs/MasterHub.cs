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
        public MasterHub(
            IAnchorService anchorService)
        {
            _anchorService = anchorService;
        }
    }
}
