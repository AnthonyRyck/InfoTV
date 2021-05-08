using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InfoTv.Areas.Identity;
using InfoTv.Data;
using MatBlazor;
using Microsoft.Extensions.FileProviders;
using System.IO;
using InfoTv.ViewModel;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using InfoTv.Hubs;

namespace InfoTv
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("ConnectionSqlite")));

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMatToaster(config =>
			{
				config.Position = MatToastPosition.BottomRight;
				config.PreventDuplicates = true;
				config.NewestOnTop = true;
				config.ShowCloseButton = true;
				config.MaximumOpacity = 95;
				config.VisibleStateDuration = 3000;
			});

			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

			services.AddSingleton<IDataService, DataService>();
			services.AddScoped<IInfoViewModel, InfoViewModel>();
			services.AddScoped<ISettingViewModel, SettingViewModel>();

			services.AddSignalR();
			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});

			services.AddTransient<IHubService, HubService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
		{
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			string pathMyStaticFiles = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyStaticFiles");
			if (!Directory.Exists(pathMyStaticFiles))
				Directory.CreateDirectory(pathMyStaticFiles);

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(pathMyStaticFiles),
				RequestPath = "/MyStaticFiles"
			});

			string pathCache = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache");
			if (!Directory.Exists(pathCache))
				Directory.CreateDirectory(pathCache);

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(pathCache),
				RequestPath = "/Cache"
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapBlazorHub();
				// pour SignalR
				endpoints.MapHub<InfoHub>("/infohub");
				endpoints.MapFallbackToPage("/_Host");
			});
		}

	}
}
