using System.IO;
using System.Text.Json.Serialization;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.Data;
using ItemsArchiveService.ExceptionHandler;
using ItemsArchiveService.Helpers;
using ItemsArchiveService.Repository;
using ItemsArchiveService.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace ItemsArchiveService
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

            services.AddCors();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddAutoMapper(typeof(Startup));
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<ILog, LogNLog>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IThirdPartyRepository, ThirdPartyRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddHostedService<ConfigureIndexes>();

            #region 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Items Archive Service", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                            },
                            new string[] { }
                        }
                    });
            });
            #endregion
            //var path = Path.Combine(System.IO.Directory.GetCurrent‌​Directory(), "..\\..\\App\\ManageItems\\dist");
            // In production, the Angular files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = path;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ItemsArchiveService v1"));
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());


            app.ConfigureExceptionHandler(logger);
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // var path = Path.Combine(System.IO.Directory.GetCurrent‌​Directory(), "..\\..\\App\\ManageItems");

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "/../../App/ManageItems";
            //     //if (env.IsDevelopment())
            //     //{
            //     spa.UseAngularCliServer(npmScript: "start");
            //     //}
            // });
        }
    }
}
