using InfoTv.Codes;
using InfoTv.Data;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.Pages
{
	public partial class Settings : ComponentBase
	{

		private const string FILE_NAME_POWERPOINT = "powerpoint.mp4";

		#region Properties

		
		private IMatFileUploadEntry _fileMat;

		[Inject]
		protected IMatToaster Toaster { get; set; }

		[Inject]
		protected DataService ServiceData {get; set;}

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

		private MessageModel messageModel = new MessageModel();

		#endregion

		#region Constructeur

		public Settings()
		{
			DateInjection = "Aucune injection de faite";
			DateInjectionPowerPoint = "Aucune injection de faite";
		}

		#endregion

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

		#endregion

		#region Protected Methods

		/// <summary>
		/// A l'initialisation de la page
		/// </summary>
		/// <returns></returns>
		protected override async Task OnInitializedAsync()
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
			if(File.Exists(fileVideoPowertPoint))
			{
				DateTime datelastWrite = File.GetLastWriteTime(fileVideoPowertPoint);
				DateInjectionPowerPoint = datelastWrite.ToString("g", new CultureInfo("fr-FR"));
			}

			// Affichage Message Info
			messageModel.MessageToDisplay = ServiceData.Message.Message;
			messageModel.SelectedAttention = ServiceData.Message.Attention;
			messageModel.DateFinAffichageMessage = ServiceData.Message.FinAffichage;
		}

		#endregion





		private async void HandleValidMessage()
        {
			DateTime dateFrance = messageModel.DateFinAffichageMessage.ToLocalTime();

			MessageInformation messageInformation = new MessageInformation()
			{
				Message = messageModel.MessageToDisplay,
				Attention = messageModel.SelectedAttention,
				FinAffichage = dateFrance
			};

			await ServiceData.SetNewMessage(messageInformation);
		}
	}

	public class MessageModel
    {
		/// <summary>
		/// Message a afficher
		/// </summary>
		[Required]
		public string MessageToDisplay { get; set; }

		/// <summary>
		/// Choix de l'attention.
		/// </summary>
		[Required]
		public AttentionMessage SelectedAttention { get; set; }

		/// <summary>
		/// Date de fin d'affichage du message
		/// </summary>
		[Required]
		public DateTime DateFinAffichageMessage { get; set; }
	}
}
