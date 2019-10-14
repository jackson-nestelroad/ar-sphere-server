using ARSphere.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Hubs
{
	public interface IClient
	{
		Task Pong(string message);
		Task AnchorData(IEnumerable<AnchorViewModel> anchors);
	}
}
