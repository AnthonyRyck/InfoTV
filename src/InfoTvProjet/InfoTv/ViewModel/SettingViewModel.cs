using InfoTv.Codes;
using InfoTv.Data;
using InfoTv.ModelsValidation;
using MatBlazor;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
	public class SettingViewModel : ISettingViewModel
	{
		/// <summary>
		/// Indique la dernière injection de fichier.
		/// </summary>
		/// <value></value>
		public string DateInjection { get; private set; }

		/// <summary>
		/// Indique la dernière injection de fichier.
		/// </summary>
		/// <value></value>
		public string DateInjectionPowerPoint { get; private set; }

		private IMatFileUploadEntry _fileMat;
		private Action StateHasChanged;

		private const string FILE_NAME_POWERPOINT = "powerpoint.mp4";

		private IDataService ServiceData;
		private IMatToaster Toaster;

		public SettingViewModel(IDataService dataService, IMatToaster toaster)
		{
			ServiceData = dataService;
			Toaster = toaster;

			MessageModel = new MessageModel();
		}

		public MessageModel MessageModel { get; set; }

		#region Public Methods

		/// <summary>
		/// Recoit les fichiers qui sont Uploader par l'utilisateur
		/// </summary>
		/// <param name="files">Liste des fichiers</param>
		/// <returns></returns>
		public async void UploadFiles(IMatFileUploadEntry[] files)
		{
			if (files.Count() >= 1)
			{
				_fileMat = files.FirstOrDefault();

				var extensionFile = Path.GetExtension(_fileMat.Name);

				if (extensionFile == ".mp4")
				{
					string pathFile = StorageHelper.CreatePathFileInCacheFolder(FILE_NAME_POWERPOINT);

					using (var fileStream = File.Create(pathFile))
					{
						await _fileMat.WriteToStreamAsync(fileStream);
					}

					DateInjectionPowerPoint = DateTime.Now.ToString("g", new CultureInfo("fr-FR"));
					StateHasChanged();
				}
			}
		}

		/// <summary>
		/// A l'initialisation de la page
		/// </summary>
		/// <returns></returns>
		public async Task OnInitialized()
		{

			//DateTime? dateLastInjection = ServiceData.GetLastInjection();
			//if (dateLastInjection == null)
			//{
			//	DateInjection = "Aucune injection de faite";
			//}
			//else
			//{
			//	DateInjection = dateLastInjection?.ToString("g", new CultureInfo("fr-FR"));
			//}

			string fileVideoPowertPoint = StorageHelper.GetPathFileInCacheFolder(FILE_NAME_POWERPOINT);
			if (File.Exists(fileVideoPowertPoint))
			{
				DateTime datelastWrite = File.GetLastWriteTime(fileVideoPowertPoint);
				DateInjectionPowerPoint = datelastWrite.ToString("g", new CultureInfo("fr-FR"));
			}

			// Affichage Message Info
			var message = await ServiceData.GetMessage();

			MessageModel.MessageToDisplay = message.Message;
			MessageModel.SelectedAttention = message.Attention;
			MessageModel.DateFinAffichageMessage = message.FinAffichage;
		}

		public void SetStateHasChanged(Action state)
		{
			StateHasChanged = state;
		}

		public async void HandleValidMessage()
		{
			DateTime dateFrance = MessageModel.DateFinAffichageMessage.ToLocalTime();

			MessageInformation messageInformation = new MessageInformation()
			{
				Message = MessageModel.MessageToDisplay,
				Attention = MessageModel.SelectedAttention,
				FinAffichage = dateFrance
			};

			await ServiceData.SetNewMessage(messageInformation);
		}

		#endregion
	}
}
