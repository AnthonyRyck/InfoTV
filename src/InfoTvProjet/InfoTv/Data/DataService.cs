using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System;
using InfoTv.Codes;

namespace InfoTv.Data
{
    /// <summary>
    /// Service de donnée de l'application
    /// </summary>
    public class DataService : IDataService
    {
        #region Properties

        /// <summary>
        /// Nom du fichier contenant le message d'info
        /// </summary>
        private const string NAME_FILE_MESSAGE = "messageData.json";

        /// <summary>
        /// Message d'information pour les sites
        /// </summary>
		public MessageInformation Message { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Met à jour le message d'information.
        /// </summary>
        /// <param name="messageInformation"></param>
        /// <returns></returns>
		public async Task SetNewMessage(MessageInformation messageInformation)
        {
            Message = messageInformation;
            string contentJson = JsonConvert.SerializeObject(this.Message);
            await StorageHelper.SaveContentInCacheFolder(NAME_FILE_MESSAGE, contentJson);
        }

        /// <summary>
        /// Récupère le message d'information
        /// </summary>
        /// <returns></returns>
        public async Task<MessageInformation> GetMessage()
        {
            string contentJson = await StorageHelper.GetFileInCache(NAME_FILE_MESSAGE);
            if (string.IsNullOrEmpty(contentJson))
            {
                Message = new MessageInformation();
            }
            else
            {
                Message = JsonConvert.DeserializeObject<MessageInformation>(contentJson);
            }

            return Message;
        }

		#endregion


	}
}