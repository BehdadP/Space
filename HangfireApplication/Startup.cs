using Hangfire;
using HangfireApplication.Services;
using HangfireApplication.Services.Interfaces;
using HangfireApplication.Settings;
using IMDB_BAL.Interface;
using IMDB_BAL.Service;
using IMDB_BAL.Settings;
using IMDB_DAL.Data;
using IMDB_DAL.Interface;
using IMDB_DAL.Models;
using IMDB_DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HangfireApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddControllers();
			services.AddHttpClient(); services.AddTransient<IRepository<WatchList>, RepositoryWatchList>();
			services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
			services.Configure<IMDbSettings>(Configuration.GetSection("IMDbSettings"));
			services.Configure<JobSettings>(Configuration.GetSection("JobSettings"));

			services.AddTransient<IMailService, Services.MailService>();

			services.AddTransient<WatchListService, WatchListService>();
			services.AddTransient<IIMDBService, IMDBService>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "HangfireApplication", Version = "v1" });
			});
			
			services.AddScoped<IJobService, JobService>();

			services.AddHangfire(x =>
			{
				x.UseSqlServerStorage(Configuration.GetConnectionString("JobDBConnection"));
			});

			services.AddHangfireServer();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HangfireApplication v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseHangfireDashboard();
		}
	}
}
