using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTv.Codes
{
	public static class StorageHelper
	{
		private const string CACHE_FOLDER = "Cache";

		private const string MYSTATICFILES_FOLDER = "MyStaticFiles";

		#region Internal Methods

		internal static string CreatePathFileInImageFolder(string fileName)
		{
			string cacheFolder = GetImageFolder();
			return Path.Combine(cacheFolder, fileName);
		}

		internal static string CreatePathFileInCacheFolder(string fileName)
		{
			string cacheFolder = GetCacheFolder();
			return Path.Combine(cacheFolder, fileName);
		}



		internal static string CreatePathFileInMyStaticFolder(string fileName)
		{
			string myStaticFolder = GetMyStaticFilesFolder();
			return Path.Combine(myStaticFolder, fileName);
		}


		/// <summary>
		/// Retourne le chemin du cache avec le nom du fichier.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		internal static string GetFileInImages(string fileName)
		{
			string pathImage = GetImageFolder();
			return Path.Combine(pathImage, fileName);
		}

		internal static string GetPathFileInCacheFolder(string fileName)
		{
			string cacheFolder = GetCacheFolder();
			return Path.Combine(cacheFolder, fileName);
		}

		/// <summary>
		/// Retourne le chemin du cache avec le nom du fichier.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		internal async static Task<string> GetFileInCache(string fileName)
		{
			string pathCache = GetCacheFolder();
			string pathFile = Path.Combine(pathCache, fileName);

			string content = string.Empty;

			if (File.Exists(pathFile))
			{
				content = await File.ReadAllTextAsync(pathFile);
			}

			return content;
		}

		/// <summary>
		/// Retourne le chemin du cache avec le nom du fichier.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		internal static string GetFileInMyStaticFilesFolder(string fileName)
		{
			string pathCache = GetMyStaticFilesFolder();
			return Path.Combine(pathCache, fileName);
		}

		/// <summary>
		/// Fait la sauvegarde du contenu dans le répertoire de cache.
		/// </summary>
		/// <param name="nameFile">Nom du fichier</param>
		/// <param name="contentJson">Contenu sous forme JSON</param>
		/// <returns></returns>
		internal async static Task SaveContentInCacheFolder(string nameFile, string contentJson)
		{
			string pathFileData = CreatePathFileInCacheFolder(nameFile);
			await File.WriteAllTextAsync(pathFileData, contentJson);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Retourne le chemin du répertoire Cache, crée le chemin si non existant
		/// </summary>
		/// <returns></returns>
		private static string GetCacheFolder()
		{
			string pathCache = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CACHE_FOLDER);

			// Si répertoire n'existe pas.
			if (!Directory.Exists(pathCache))
			{
				Directory.CreateDirectory(pathCache);
			}

			return pathCache;
		}

		/// <summary>
		/// Retourne le chemin du répertoire Cache, crée le chemin si non existant
		/// </summary>
		/// <returns></returns>
		private static string GetMyStaticFilesFolder()
		{
			string pathMyStaticFiles = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MYSTATICFILES_FOLDER);

			// Si répertoire n'existe pas.
			if (!Directory.Exists(pathMyStaticFiles))
			{
				Directory.CreateDirectory(pathMyStaticFiles);
			}

			return pathMyStaticFiles;
		}


		private static string GetImageFolder()
		{
			string pathImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "Images");

			// Si répertoire n'existe pas.
			if (!Directory.Exists(pathImage))
			{
				Directory.CreateDirectory(pathImage);
			}

			return pathImage;
		}


		

		#endregion

	}
}
