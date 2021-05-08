using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
    public class HubService : IHubService
    {
		public HubConnection HubConnection { get; set; }

		public async Task DisposeAsync()
        {
            await HubConnection.DisposeAsync();
        }

        public void InitHub(Uri url)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }

        /// <see cref="IHubService.On{T1}(string, Action{T1})"/>
        public void On<T1>(string nomMethod, Action<T1> handler)
        {
            HubConnection.On<T1>(nomMethod, handler);
        }

        /// <see cref="IHubService.On{T1, T2}(string, Action{T1, T2})"/>
        public void On<T1, T2>(string nomMethod, Action<T1, T2> handler)
        {
            HubConnection.On<T1, T2>(nomMethod, handler);
        }

        /// <see cref="IHubService.SendAsync(string, object)"/>
        public async Task SendAsync(string nomMethod, object param1)
        {
            await HubConnection.SendAsync(nomMethod, param1);
        }

        /// <see cref="IHubService.SendAsync(string, object, object)"/>
        public async Task SendAsync(string nomMethod, object param1, object param2)
        {
            await HubConnection.SendAsync(nomMethod, param1, param2);
        }

        /// <see cref="IHubService.SendAsync(string, object, object, object)"/>
        public async Task SendAsync(string nomMethod, object param1, object param2, object param3)
        {
            await HubConnection.SendAsync(nomMethod, param1, param2, param3);
        }

        /// <see cref="IHubService.SendAsync(string, object, object, object, object)"/>
        public async Task SendAsync(string nomMethod, object param1, object param2, object param3, object param4)
        {
            await HubConnection.SendAsync(nomMethod, param1, param2, param3, param4);
        }
    }
}
