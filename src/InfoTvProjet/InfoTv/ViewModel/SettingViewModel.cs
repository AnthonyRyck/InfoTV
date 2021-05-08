﻿using InfoTv.Codes;
using InfoTv.Data;
using InfoTv.ModelsValidation;
using MatBlazor;
using Microsoft.AspNetCore.Components;
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

		private Action StateHasChanged;

		private NavigationManager navigationManager;
		private IHubService hubService;
		private IDataService ServiceData;
		private IMatToaster Toaster;

		public SettingViewModel(IDataService dataService, IMatToaster toaster, NavigationManager navigation, IHubService hubSvc)
		{
			ServiceData = dataService;
			Toaster = toaster;

			navigationManager = navigation;
			hubService = hubSvc;

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
				IMatFileUploadEntry fileMat = files.FirstOrDefault();

				var extensionFile = Path.GetExtension(fileMat.Name);

				if (extensionFile == ".mp4")
				{
					await ServiceData.SavePowerPointVideo(fileMat);

					DateInjectionPowerPoint = DateTime.Now.ToString("g", new CultureInfo("fr-FR"));
					StateHasChanged();

					// Envoie pour les autres clients
					var tempFileName = ServiceData.GetPowerPointFile();
					await hubService.SendAsync("SyncPowerPoint", tempFileName.NomFichier);

					await ServiceData.DeleteOldPowerPoint();
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

			//string fileVideoPowertPoint = StorageHelper.GetPathFileInCacheFolder(FILE_NAME_POWERPOINT);

			//string fileVideoPowertPoint = ServiceData.GetNameFilePowerPoint();
			//if (File.Exists(fileVideoPowertPoint))
			//{
			//	DateTime datelastWrite = File.GetLastWriteTime(fileVideoPowertPoint);
			//	DateInjectionPowerPoint = datelastWrite.ToString("g", new CultureInfo("fr-FR"));
			//}

			InfoPowerPointFile infoTemp = ServiceData.GetPowerPointFile();
			DateInjectionPowerPoint = infoTemp.DateLastWrite;

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

			// Envoie pour les autres clients
			await hubService.SendAsync("ReceiveNewMessage", messageInformation);
		}

		#region HubConnection

		public async Task InitHub()
		{
			hubService.InitHub(navigationManager.ToAbsoluteUri("/infohub"));
			await hubService.HubConnection.StartAsync();
		}

		public async Task DisposeHubConnection()
		{
			await hubService.DisposeAsync();
		}

		#endregion


		#endregion
	}
}
