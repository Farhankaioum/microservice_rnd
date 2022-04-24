using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlatformServcice.AsyncDataServices;
using PlatformServcice.Data;
using PlatformServcice.SyncDataServices.Grpc;
using PlatformServcice.SyncDataServices.Http;
using System;
using System.IO;

namespace PlatformServcice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                Console.WriteLine("--> Using SqlServer db");
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                Console.WriteLine("--> Using InMemory db");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }

            services.AddScoped<IPlatformRepo, PlatformRepo>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddGrpc();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformServcice", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformServcice v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GrpcPlatformService>();

                endpoints.MapGet("/protos/platforms.proto", async context => 
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
                }); // optional
            });

            PrepDb.PrepPopulation(app, env.IsProduction());
        }
    }
}
