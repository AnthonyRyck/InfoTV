using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
    public interface IHubService
    {
        HubConnection HubConnection { get; set; }

        /// <summary>
        /// Initialise le HubConnection avec une URL donnée.
        /// </summary>
        /// <param name="url"></param>
        void InitHub(Uri url);

        /// <summary>
        /// Enregistre une Action qui sera appelé lorsque la méthode du hub,
        /// avec le nom de méthode spécifié est appelée,
        /// avec 1 paramètre sur l'Action
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="nomMethod"></param>
        /// <param name="handler"></param>
        void On<T1>(string nomMethod, Action<T1> handler);

        /// <summary>
        /// Enregistre une Action qui sera appelé lorsque la méthode du hub,
        /// avec le nom de méthode spécifié est appelée,
        /// avec 2 paramètres sur l'Action 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="nomMethod"></param>
        /// <param name="handler"></param>
        void On<T1, T2>(string nomMethod, Action<T1, T2> handler);

        /// <summary>
        /// Appelle une méthode du Hub sur le serveur à l'aide du nom de méthode 
        /// et des arguments spécifiés. N'attend pas une réponse du récepteur.
        /// Avec 1 paramètre
        /// </summary>
        /// <param name="nomMethod"></param>
        /// <param name="param1"></param>
        /// <returns></returns>
        Task SendAsync(string nomMethod, object param1);

        /// <summary>
        /// Appelle une méthode du Hub sur le serveur à l'aide du nom de méthode 
        /// et des arguments spécifiés. N'attend pas une réponse du récepteur.
        /// Avec 2 paramètres 
        /// </summary>
        /// <param name="nomMethod"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        Task SendAsync(string nomMethod, object param1, object param2);

        /// <summary>
        /// Appelle une méthode du Hub sur le serveur à l'aide du nom de méthode 
        /// et des arguments spécifiés. N'attend pas une réponse du récepteur.
        /// Avec 3 paramètres
        /// </summary>
        /// <param name="nomMethod"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <returns></returns>
        Task SendAsync(string nomMethod, object param1, object param2, object param3);

        /// <summary>
        /// Appelle une méthode du Hub sur le serveur à l'aide du nom de méthode 
        /// et des arguments spécifiés. N'attend pas une réponse du récepteur.
        /// Avec 4 paramètres
        /// </summary>
        /// <param name="nomMethod"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <returns></returns>
        Task SendAsync(string nomMethod, object param1, object param2, object param3, object param4);

        /// <summary>
        /// Dispose le HubConnection
        /// </summary>
        /// <returns></returns>
        Task DisposeAsync();
    }
}
