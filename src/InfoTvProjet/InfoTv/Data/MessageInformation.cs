using InfoTv.Codes;
using InfoTv.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.Data
{
	public class MessageInformation
	{
		/// <summary>
		/// Message à afficher
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Attention donnée au message
		/// </summary>
		public AttentionMessage Attention { get; set; }

		/// <summary>
		/// Quand ne plus l'afficher
		/// </summary>
		public DateTime FinAffichage { get; set; }

		public MessageInformation()
		{
			Message = string.Empty;
			Attention = AttentionMessage.Normal;
		}
	}
}
