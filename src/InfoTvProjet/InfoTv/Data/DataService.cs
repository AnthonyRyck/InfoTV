using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System;
using InfoTv.Codes;
using System.Globalization;

namespace InfoTv.Data
{
    /// <summary>
    /// Service de donnée de l'application
    /// </summary>
    public class DataService : IDataService
    {
        #region Properties

        private const string FILE_NAME_POWERPOINT = "powerpoint.mp4";

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

        public string GetNameFilePowerPoint()
		{
            return StorageHelper.GetPathFileInCacheFolder(FILE_NAME_POWERPOINT);
        }


        public InfoPowerPointFile GetPowerPointFile()
		{
            InfoPowerPointFile infoFile = new InfoPowerPointFile();

            string fileName = GetFileNamePowerPoint();
            string fileVideoPowertPoint = StorageHelper.GetPathFileInCacheFolder(fileName);
            
            if (File.Exists(fileVideoPowertPoint))
            {
                DateTime datelastWrite = File.GetLastWriteTime(fileVideoPowertPoint);
                infoFile.DateLastWrite = datelastWrite.ToString("g", new CultureInfo("fr-FR"));

                infoFile.NomFichier = fileName;
            }

            return infoFile;
        }

        public string GetNextFileName()
		{
            string fileName = GetFileNamePowerPoint();
			if (!string.IsNullOrEmpty(fileName))
			{
                int indexFile = Convert.ToInt32(fileName.Remove(1));
                int nextIndex = GetNextIndex(indexFile);
                return StorageHelper.CreatePathFileInCacheFolder(nextIndex + FILE_NAME_POWERPOINT);
			}
            else
			{
                return StorageHelper.CreatePathFileInCacheFolder(1 + FILE_NAME_POWERPOINT);
			}
        }

        #endregion

        #region Private methods

        private string GetFileNamePowerPoint()
		{
            var cacheFolder = StorageHelper.GetCacheFolder();

            var directoryInfo = new DirectoryInfo(cacheFolder);
            var filesInfo = directoryInfo.GetFiles()
                                        .Where(x => x.Name.Contains(FILE_NAME_POWERPOINT))
                                        .ToList();

            string nameFile = string.Empty;
            DateTime lastWrite = new DateTime();

			foreach (var item in filesInfo)
			{
                if(item.LastWriteTime.CompareTo(lastWrite) > 0)
				{
                    lastWrite = item.LastWriteTime;
                    nameFile = item.Name;
                }
			}

            return nameFile;
        }

        private int GetNextIndex(int currentIndex)
		{
            if (currentIndex == 9)
                return 1;

            return ++currentIndex;
        }

		#endregion


	}
}