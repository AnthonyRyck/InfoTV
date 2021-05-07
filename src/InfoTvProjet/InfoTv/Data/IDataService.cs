using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.Data
{
	public interface IDataService
	{
		/// <summary>
		/// Message d'information pour les sites
		/// </summary>
		MessageInformation Message { get; set; }

		/// <summary>
		/// Met à jour le message d'information.
		/// </summary>
		/// <param name="messageInformation"></param>
		/// <returns></returns>
		Task SetNewMessage(MessageInformation messageInformation);

		/// <summary>
		/// Récupère le message d'information
		/// </summary>
		/// <returns></returns>
		Task<MessageInformation> GetMessage();
	}
}
