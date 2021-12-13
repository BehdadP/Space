using FluentValidation.AspNetCore;
using IMDB_BAL.Interface;
using IMDB_BAL.Service;
using IMDB_BAL.Settings;
using IMDB_DAL.Data;
using IMDB_DAL.Interface;
using IMDB_DAL.Models;
using IMDB_DAL.Repository;
using IMDB_DAL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;


namespace IMDBWebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpClient();
            services.AddTransient<IRepository<WatchList>, RepositoryWatchList>();
            services.Configure<IMDbSettings>(_configuration.GetSection("IMDbSettings"));

            services.AddTransient<WatchListService, WatchListService>();
            services.AddTransient<IIMDBService, IMDBService>();

            services.AddControllers();

            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<AddWatchListValidator>();              
                options.ImplicitlyValidateChildProperties = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMDBWebAPI", Version = "v1" });


                c.AddFluentValidationRules();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMDBWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
