using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
	public interface IInfoViewModel
	{
		string SourcePowerpoint { get; set; }

		string ColorMessage { get; set; }

		string Message { get; set; }

		string Visibility { get; set; }

		void SetStateHasChanged(Action state);

		Task LoadMessage();

		void LoadPowerPoint();

		Task InitHub();

		Task DisposeHubConnection();
	}
}
