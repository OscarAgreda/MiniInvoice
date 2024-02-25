using Autofac;

namespace DDDCleanArchStarter.Api.Strategies.Di
{
    public class UpdateRegistrationInvoiceDataStrategy : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationInvoiceDataStrategy>().As<IRegistrationInvoiceDataStrategy>().InstancePerLifetimeScope();
            builder.RegisterType<ProductFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceDetailFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddProductCommand>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddInvoiceDetailCommand>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AddInvoiceCommand2>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
