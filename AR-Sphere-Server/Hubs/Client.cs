using ARSphere.DTO;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Hubs
{
    /// <summary>
    /// <para>List of methods to be called by SingalR on the client.</para>
    /// </summary>
    public interface IClient
    {
        Task NewNearbyAnchor(AnchorViewModel anchor);
    }

    /// <summary>
    /// <para>Represents a single Client connection and associated identification data.</para>
    /// </summary>
    public class Client
    {
        public Point Location { get; set; }
        public int UserId { get; set; }

        public void SetLocation(double longitude, double latitude)
        {
            Location.X = longitude;
            Location.Y = latitude;
        }
    }
}
