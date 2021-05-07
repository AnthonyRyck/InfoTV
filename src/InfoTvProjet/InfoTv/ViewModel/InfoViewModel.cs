using InfoTv.Codes;
using InfoTv.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
	public class InfoViewModel : IInfoViewModel
	{

		public string SourcePowerpoint { get; set; }

		public string ColorMessage { get; set; }

		public string Message { get; set; }

		public string Visibility { get; set; }


		private MessageInformation messageInformation;

		private IDataService DataService;

		public InfoViewModel(IDataService dataService)
		{
			DataService = dataService;
			
		}

		private const string FILE_NAME_POWERPOINT = "powerpoint.mp4";
		public async Task LoadMessage()
		{
			messageInformation = await DataService.GetMessage();
			Message = messageInformation.Message;
			SelectColor(messageInformation.Attention);
			SelectCssToDisplay(messageInformation.FinAffichage);


			// Chargement du MP4, s'il existe.
			string filePowerPoint = StorageHelper.GetPathFileInCacheFolder(FILE_NAME_POWERPOINT);

			if (File.Exists(filePowerPoint))
			{
				SourcePowerpoint = "../Cache/" + FILE_NAME_POWERPOINT;
			}
		}


		#region Private methods

		private void SelectColor(AttentionMessage attention)
		{
			switch (attention)
			{
				case AttentionMessage.Attention:
					ColorMessage = "orange";
					break;

				case AttentionMessage.Critique:
					ColorMessage = "red";
					break;

				case AttentionMessage.Normal:
				default:
					ColorMessage = string.Empty;
					break;
			}
		}

		private void SelectCssToDisplay(DateTime dateFinMessage)
		{
			DateTime dateNow = DateTime.Now;

			//Date Now est supérieur à la date de fin.
			Visibility = (dateNow.CompareTo(dateFinMessage) > 0)
			   ? "visibilityHidden"
			   : "visibilityDisplay";
		}

		#endregion
	}
}
