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
        private readonly HubConnection Connection;
        private readonly string Url = "https://localhost:44336/connect";
        // private readonly string Url = "https://ar-sphere-server-2.azurewebsites.net/connect";

        private bool Connected = false;
        private readonly Dictionary<char, (string, Func<Task>)> Options = new Dictionary<char, (string, Func<Task>)>();
        
        public ClientTest()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl(Url)
                .Build();

            Connection.Closed += error =>
            {
                Console.WriteLine("Connection closed.");
                Connected = false;
                return Task.CompletedTask;
            };

            Connection.On("NewNearbyAnchor", (AnchorViewModel newAnchor) =>
            {
                Console.WriteLine($"\nNew nearby anchor! {newAnchor.Id}");
            });

            Connection.On("Ping", () =>
            {
                Console.WriteLine("Ping received.");
            });

            Options.Add('1', ("Ping", Ping));
            Options.Add('2', ("Get Nearby Anchors", GetNearbyAnchors));
            Options.Add('3', ("Create New Anchor", CreateNewAnchor));
        }

        public async Task Start()
        {
            await Connect();
            while (Connected)
            {
                try
                {
                    Console.WriteLine();
                    foreach((char key, var value) in Options)
                    {
                        Console.WriteLine($"{key} -- {value.Item1}");
                    }
                    while(Connected)
                    {
                        Console.Write("Next input: ");
                        char nextKey = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                        if (Options.ContainsKey(nextKey))
                        {
                            await Options[nextKey].Item2();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task Connect()
        {
            await Connection.StartAsync();
            await Connection.InvokeAsync("SetUserId", 0);
            Connected = true;
            Console.WriteLine("Connection started.");
        }

        private async Task Close()
        {
            await Connection.StopAsync();
            Connected = false;
        }

        private async Task Ping()
        {
            string message = "Ping!";
            string res = await Connection.InvokeAsync<string>("Ping", message);
            if(res != message)
            {
                throw new Exception("Ping failed.");
            }

            Console.WriteLine("Ping successful.");
        }

        private async Task CreateNewAnchor()
        {
            string newAnchorId = $"My_New_Anchor_{new Random().Next()}";
            await Connection.SendAsync("CreateAnchor", new NewAnchorModel
            {
                Id = newAnchorId,
                Longitude = 59,
                Latitude = 17,
                Model = 0
            });
            Console.WriteLine($"Created anchor {newAnchorId}.");
        }

        private async Task GetNearbyAnchors()
        {
            IEnumerable<AnchorViewModel> nearbyAnchors = await Connection.InvokeAsync<IEnumerable<AnchorViewModel>>("GetNearbyAnchors", 59, 17);
            Console.WriteLine($"Anchors found: {string.Join(", ", nearbyAnchors.Select(m => m.Id))}");
        }
    }
}
