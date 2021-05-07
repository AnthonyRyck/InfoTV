using InfoTv.Codes;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Threading;

namespace InfoTv.Pages
{
	public partial class InfoPlateau : ComponentBase
	{

		[Inject]
		public NavigationManager UriHelper { get; set; }
		//protected DataService ServiceData { get; set; }


		#region Properties

		/// <summary>
		/// Source pour le fichier GIF dans le projet.
		/// </summary>
		public string SourceGif { get; private set; }

		/// <summary>
		/// Nom du fichier PowerPoint.
		/// </summary>
		private const string FILE_NAME_POWERPOINT = "powerpoint.mp4";

		///// <summary>
		///// Message à afficher
		///// </summary>
		//public string MessageToDisplay { get; private set; }

		///// <summary>
		///// Couleur d'affichage du message.
		///// </summary>
		//public string ColorDisplay { get; private set; }

		#endregion

		#region Constructeur



		#endregion

		#region protected methods

		protected override void OnInitialized()
		{
			//if (!ServiceData.IsLoaded())
			//{
			//	await ServiceData.LoadData();
			//}

			// Affichage Message Info
			//MessageToDisplay = ServiceData.Message.Message;
			//SelectColor(ServiceData.Message.Attention);
			//DateFinAffichageMessage = ServiceData.Message.FinAffichage;


			// Chargement du MP4, s'il existe.
			string filePowerPoint = StorageHelper.GetPathFileInCacheFolder(FILE_NAME_POWERPOINT);

			if (File.Exists(filePowerPoint))
			{
				SourceGif = "../Cache/" + FILE_NAME_POWERPOINT;

				StateHasChanged();
			}
		}

		protected override void OnAfterRender(bool firstRender)
		{
			if (firstRender)
			{
				int fiveMinutesInMilliseconds = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);

				var timer = new Timer(new TimerCallback(_ =>
				{
					UriHelper.NavigateTo(UriHelper.Uri, forceLoad: true);
				}), null, fiveMinutesInMilliseconds, fiveMinutesInMilliseconds);
			}
		}


		#endregion

		#region Private methods

		//private void SelectColor(AttentionMessage attention)
		//{
		//	switch (attention)
		//	{
		//		case AttentionMessage.Attention:
		//			ColorDisplay = "orange";
		//			break;

		//		case AttentionMessage.Critique:
		//			ColorDisplay = "red";
		//			break;

		//		case AttentionMessage.Normal:
		//		default:
		//			ColorDisplay = string.Empty;
		//			break;
		//	}
		//}

		#endregion
	}
}
