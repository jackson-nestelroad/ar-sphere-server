﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ARSphere.Hubs
{
	public partial class MasterHub : Hub<IClient>
	{
		public async Task Ping(string message)
		{
			await Clients.Caller.Pong(message);
		}
	}
}