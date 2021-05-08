using InfoTv.Codes;
using InfoTv.Composants;
using InfoTv.Data;
using Microsoft.AspNetCore.Components;
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

		public RenderFragment RenderVideo { get; set; }


		private NavigationManager navigationManager;
		private IHubService hubService;
		private Action StateHasChanged;
		private IDataService DataService;

		public InfoViewModel(IDataService dataService, NavigationManager navigation, IHubService hubSvc)
		{
			DataService = dataService;

			navigationManager = navigation;
			hubService = hubSvc;
		}

		public void SetStateHasChanged(Action state)
		{
			StateHasChanged = state;
		}


		public async Task LoadMessage()
		{
			var messageInformation = await DataService.GetMessage();
			SetMessage(messageInformation);
		}

		public void LoadPowerPoint()
		{
			InfoPowerPointFile sourceFile = DataService.GetPowerPointFile();
			SourcePowerpoint = "../Cache/" + sourceFile.NomFichier;

			RenderFragment CreateCompo() => builder =>
			{
				builder.OpenComponent(0, typeof(VideoDisplay));
				builder.AddAttribute(1, "SourcePowerpoint", SourcePowerpoint);
				builder.CloseComponent();
			};
			RenderVideo = CreateCompo();
		}

		#region Hub Connection

		public async Task InitHub()
		{
			hubService.InitHub(navigationManager.ToAbsoluteUri("/infohub"));

			hubService.On<string>("SyncPowerPoint", (nouveauPpt) =>
			{
				SourcePowerpoint = "../Cache/" + nouveauPpt;
				RenderVideo = null;
				StateHasChanged.Invoke();

				RenderFragment CreateCompo() => builder =>
				{
					builder.OpenComponent(0, typeof(VideoDisplay));
					builder.AddAttribute(1, "SourcePowerpoint", SourcePowerpoint);
					builder.CloseComponent();
				};
				RenderVideo = CreateCompo();

				StateHasChanged.Invoke();
			});

			hubService.On<MessageInformation>("ReceiveNewMessage", (newMessage) =>
			{
				SetMessage(newMessage);
				StateHasChanged.Invoke();
			});

			await hubService.HubConnection.StartAsync();
		}

		public async Task DisposeHubConnection()
		{
			await hubService.DisposeAsync();
		}

		#endregion

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

		private void SetMessage(MessageInformation messageInformation)
		{
			Message = messageInformation.Message;
			SelectColor(messageInformation.Attention);
			SelectCssToDisplay(messageInformation.FinAffichage);
		}

		#endregion
	}
}
