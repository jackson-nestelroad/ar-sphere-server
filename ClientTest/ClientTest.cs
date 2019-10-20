using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ARSphere.Models;
using ARSphere.DTO;

namespace ClientTest
{
	class ClientTest
	{
		private HubConnection connection;
		// private string Url = "https://localhost:44336/connect";
		private string Url = "https://ar-sphere-server.azurewebsites.net/connect";
		private bool Connected = false;

		public ClientTest()
		{
			connection = new HubConnectionBuilder()
				.WithUrl(Url)
				.Build();

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
				if(ShouldClose())
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
			catch(Exception ex) {
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

				//NewAnchorModel newAnchor = new NewAnchorModel
				//{
				//	Id = "SAMPLE_ID",
				//	X = 0,
				//	Y = 0,
				//	Model = 0,
				//	Creator = 0
				//};

				//await connection.InvokeAsync("CreateAnchor", newAnchor);

				AnchorViewModel m = await connection.InvokeAsync<AnchorViewModel>("GetLastAnchor");
				Console.WriteLine(m.Id);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
