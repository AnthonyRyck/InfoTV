using InfoTv.Codes;
using System;
using System.ComponentModel.DataAnnotations;

namespace InfoTv.ModelsValidation
{
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
