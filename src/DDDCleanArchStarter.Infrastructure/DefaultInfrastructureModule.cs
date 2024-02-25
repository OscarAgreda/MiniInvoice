using System.Collections.Generic;
using System.Reflection;
using Autofac;
using DDDCleanArchStarter.Domain.Interfaces;
using DDDCleanArchStarter.Infrastructure.Data;
using DDDCleanArchStarter.Infrastructure.Messaging;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Module = Autofac.Module;
namespace DDDCleanArchStarter.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly string _sqlConnectionString;
        private readonly List<Assembly> _assemblies = new();
        private readonly bool _isDevelopment;
        public DefaultInfrastructureModule(string sqlConnectionString, bool isDevelopment, Assembly callingAssembly = null)
        {
            _sqlConnectionString = sqlConnectionString;
            _isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(Invoice));
            _assemblies.Add(coreAssembly);
            var infrastructureAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            _assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }
        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }
        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CachedRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterType<RabbitApplicationMessagePublisher>()
                .As<IApplicationMessagePublisher>()
                .InstancePerLifetimeScope();

         

            var logger = new LoggerConfiguration().Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(_sqlConnectionString, new MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    SchemaName = "dbo",
                    AutoCreateSqlTable = true
                }).CreateLogger();
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();

            builder.RegisterGeneric(typeof(AppLoggerService<>))
              .As(typeof(IAppLoggerService<>))
              .InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterType<EmailSender>().As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
        }
        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
        }
    }
}