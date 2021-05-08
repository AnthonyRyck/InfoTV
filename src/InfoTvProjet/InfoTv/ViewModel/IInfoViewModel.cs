using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.ViewModel
{
	public interface IInfoViewModel
	{
		string ColorMessage { get; set; }

		string Message { get; set; }

		string Visibility { get; set; }

		RenderFragment RenderVideo { get; set; }

		void SetStateHasChanged(Action state);

		Task LoadMessage();

		void LoadPowerPoint();

		Task InitHub();

		Task DisposeHubConnection();


		void SetInvocateur(Func<Task> invoke);
	}
}
