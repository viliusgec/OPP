using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    public class SingletonConnection
    {
        private static SingletonConnection instance = null;
        private static HubConnection connection;

        private SingletonConnection()
        {
            this.Connect();
        }

        public static SingletonConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new SingletonConnection();
            }

            return instance;
        }
        public void Connect()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44361/chathub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        public HubConnection GetConnection()
        {
            return connection;
        }

    }
}
