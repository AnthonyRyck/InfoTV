using InfoTv.Data;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InfoTv.Hubs
{
	public class InfoHub : Hub
	{
        /// <summary>
        /// A la réception d'un nouveau fichier powerpoint
        /// </summary>
        /// <param name="nouvelleSource"></param>
        /// <returns></returns>
        public async Task SyncPowerPoint(string nouvelleSource)
        {
            await Clients.Others.SendAsync("SyncPowerPoint", nouvelleSource);
        }

        /// <summary>
        /// Quand un nouveau message est envoyé.
        /// </summary>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public async Task ReceiveNewMessage(MessageInformation newMessage)
        {
            await Clients.Others.SendAsync("ReceiveNewMessage", newMessage);
        }
    }
}
