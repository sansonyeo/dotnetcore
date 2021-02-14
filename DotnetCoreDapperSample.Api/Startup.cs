using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DotnetCoreDapperSample.Api.Databases;
using DotnetCoreDapperSample.Api.Filters;
using DotnetCoreDapperSample.Api.Middlewares;
using DotnetCoreDapperSample.Api.Repositories;
using Oracle.ManagedDataAccess.Client;

namespace DotnetCoreDapperSample.Api
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped(sp => new AppDbConnectionProvider(new OracleConnection(connectionString)));
            services.AddScoped<BlogRepository>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers(options => options.Filters.Add(typeof(ValidateModelFilter)))
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); // デフォルトのモデル検証を無効化します
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetCoreDapperSample.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetCoreDapperSample.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandler();

            app.UseAccessLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
