using ARSphere.DTO;
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

    }

    /// <summary>
    /// <para>Represents a single Client connection and associated identification data.</para>
    /// </summary>
    public class Client
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
