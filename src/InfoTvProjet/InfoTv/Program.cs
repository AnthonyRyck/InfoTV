using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InfoTv.Codes;
using InfoTv.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfoTv
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string pathLog = Path.Combine(AppContext.BaseDirectory, "Logs");
			if (!Directory.Exists(pathLog))
			{
				Directory.CreateDirectory(pathLog);
			}

			try
			{
				var host = CreateHostBuilder(args).Build();

				// Création du répertoire pour la base de donnée
				string pathDatabase = Path.Combine(AppContext.BaseDirectory, "Database");
				if (!Directory.Exists(pathDatabase))
					Directory.CreateDirectory(pathDatabase);

				var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
				using (var scope = scopeFactory.CreateScope())
				{
					var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
					var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
					var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

					if (db.Database.EnsureCreated())
					{
						DataInitializer.InitData(roleManager, userManager).Wait();
					}
				}

				host.Run();
			}
			catch (Exception ex)
			{
				
			}
		}



		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
