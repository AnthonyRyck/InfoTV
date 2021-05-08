using InfoTv.ModelsValidation;
using MatBlazor;
using System;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
	public interface ISettingViewModel
	{

		MessageModel MessageModel { get; set; }

		string DateInjection { get; }

		string DateInjectionPowerPoint { get; }

		void UploadFiles(IMatFileUploadEntry[] files);

		Task OnInitialized();

		void HandleValidMessage();

		void SetStateHasChanged(Action state);

		Task InitHub();

		Task DisposeHubConnection();
	}
}
