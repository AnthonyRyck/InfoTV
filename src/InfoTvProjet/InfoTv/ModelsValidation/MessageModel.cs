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
		[Required(ErrorMessage = "Il faut un message à afficher")]
		public string MessageToDisplay { get; set; }

		/// <summary>
		/// Choix de l'attention.
		/// </summary>
		[Required(ErrorMessage = "Sélectionner une attention")]
		public AttentionMessage SelectedAttention { get; set; }

		/// <summary>
		/// Date de fin d'affichage du message
		/// </summary>
		[Required(ErrorMessage = "Sélectionner une date de fin d'affichage")]
		public DateTime DateFinAffichageMessage { get; set; }
	}
}
