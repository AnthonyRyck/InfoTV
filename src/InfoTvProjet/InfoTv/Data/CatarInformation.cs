using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.Data
{
	public class CatarInformation
	{
		/// <summary>
		/// Sommaire du CATAR
		/// </summary>
		public string Sommaire { get; set; }

		/// <summary>
		/// Quand ne plus l'afficher
		/// </summary>
		public DateTime FinCatar { get; set; }
	}
}
