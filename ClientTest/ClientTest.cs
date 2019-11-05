using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ARSphere.Models;
using ARSphere.DTO;
using System.Linq;

namespace ClientTest
{
    class ClientTest
    {
        private HubConnection connection;
        private readonly string Url = "https://localhost:44336/connect";
        // private readonly string Url = "https://ar-sphere-server.azurewebsites.net/connect";
        private bool Connected = false;

        public ClientTest()
        {
            connection = new HubConnectionBuilder()
                .WithUrl(Url)
                .Build();

            connection.ServerTimeout = TimeSpan.FromSeconds(10);

            connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed.");
                await connection.StartAsync();
            };
        }

        public async Task Start()
        {
            await Connect();
            while (Connected)
            {
                await Invoke();
                if (ShouldClose())
                {
                    await Close();
                }
            }
        }

        private async Task Connect()
        {
            try
            {
                await connection.StartAsync();
                Connected = true;
                Console.WriteLine("Connection started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task Close()
        {
            await connection.StopAsync();
            Connected = false;
        }

        private bool ShouldClose()
        {
            Console.Write("Close connection? [Y/n]");
            char res = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return res == 'Y' || res == 'y';
        }

        private async Task Invoke()
        {
            try
            {
                string res = await connection.InvokeAsync<string>("Ping", "Hello world!");
                Console.WriteLine("Response: " + res);

                AnchorViewModel lastAnchor = await connection.InvokeAsync<AnchorViewModel>("GetLastAnchor");
                Console.WriteLine(lastAnchor.Id);

                IEnumerable<AnchorViewModel> nearbyAnchors = await connection.InvokeAsync<IEnumerable<AnchorViewModel>>("GetNearbyAnchors", 59, 17);
                Console.WriteLine(string.Join(", ", nearbyAnchors.Select(m => m.Id)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
