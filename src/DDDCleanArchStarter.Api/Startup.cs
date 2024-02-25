using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using BlazorShared;
using Confluent.Kafka;
using DDDCleanArchStarter.Api.Strategies.Di;
using DDDCleanArchStarter.Domain.Interfaces;
using DDDCleanArchStarter.Infrastructure;
using DDDCleanArchStarter.Infrastructure.Data;
using DDDCleanArchStarter.Infrastructure.Messaging;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace DDDCleanArchStarter.Api
{
    public class Startup
    {
        public const string CORS_POLICY = "CorsPolicy";
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureProductionServices(services);
        }
        public void ConfigureDockerServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
        }
        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseInMemoryDatabase("AppDb"));
            ConfigureServices(services);
        }
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            ConfigureServices(services);
        }
        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMemoryCache();
            services.AddSingleton(typeof(IApplicationSettings), typeof(OfficeSettings));
            var baseUrlConfig = new BaseUrlConfiguration();
            Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);
            services.AddCors(options => options.AddPolicy(CORS_POLICY,
                builder =>
                {
                    builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost")
                        .TrimEnd('/'));
                    builder.SetIsOriginAllowed(origin => true);
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                }));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(AppDbContext).Assembly,
                typeof(Invoice).Assembly
            };
            services.AddMediatR(assemblies);
            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" }));
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGenCustom();
            services.AddSingleton<KafkaProducerService>();
            services.Configure<ProducerConfig>(Configuration.GetSection("KafkaProducerConfig"));
            services.AddSingleton<IProducer<string, string>>(sp =>
            {
                var producerConfig = sp.GetRequiredService<IOptions<ProducerConfig>>().Value;
                return new ProducerBuilder<string, string>(producerConfig).Build();
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var isDevelopment = _env.EnvironmentName == "Development";
            builder.RegisterModule(new DefaultInfrastructureModule(connectionString,isDevelopment, Assembly.GetExecutingAssembly()));

            builder.RegisterModule(new UpdateRegistrationInvoiceDataStrategy());

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors(CORS_POLICY);
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDDCleanArchStarter V1");
                    c.RoutePrefix = string.Empty;
                    c.DocExpansion(DocExpansion.None);
                    c.EnableFilter("Customer");
                    c.EnableTryItOutByDefault();
                });
            }
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}