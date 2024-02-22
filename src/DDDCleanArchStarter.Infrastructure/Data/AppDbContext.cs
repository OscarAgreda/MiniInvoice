using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel;
using MassTransit.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// make sure package Microsoft.EntityFrameworkCore.Design
//--RUN THIS FROM DDDCleanArchStarter.Api project folder   << ---------

// -c	The DbContext class to use. Class name only or fully qualified with namespaces. If this option is omitted, EF Core will find the context class. If there are multiple context classes, this option is required.
// -p	Relative path to the project folder of the target project. Default value is the current folder.
// -s	Relative path to the project folder of the startup project. Default value is the current folder.
// -o	The directory to put files in. Paths are relative to the project directory.


// to drop the database
// dotnet ef database drop -c appdbcontext -p ../DDDCleanArchStarter.Infrastructure/DDDCleanArchStarter.Infrastructure.csproj -f -v

// dotnet ef migrations add initialDDDCleanArchStarterAppMigration -c appdbcontext -p ../DDDCleanArchStarter.Infrastructure/DDDCleanArchStarter.Infrastructure.csproj -s DDDCleanArchStarter.Api.csproj -o Data/Migrations

// dotnet ef database update -c appdbcontext --project ../DDDCleanArchStarter.Infrastructure/DDDCleanArchStarter.Infrastructure.csproj -s DDDCleanArchStarter.Api.csproj

// then look at AppDbContextSeed
namespace DDDCleanArchStarter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly Guid _tenantId = Guid.Parse("424CB1C1-8C9E-70BE-71B5-64B48B9769F8");
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
            : base(options)
        {
            SavedChanges += PublishEvent;
            _mediator = mediator;
        }
        private static void PublishEvent(object sender, SavedChangesEventArgs e)
        {
            var aa = e;
        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountAdjustment> AccountAdjustments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerPhoneNumber> CustomerPhoneNumbers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<State> States { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Cascade;
            }
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            if (_mediator == null)
            {
                return result;
            }
            //var entitiesWithEventsTracked = ChangeTracker
            //    .Entries()
            //    .Select(e => e.Entity as BaseEntityEv<Guid>)
            //    .Where(e => e?.Events != null && e.Events.Any())
            //    .ToArray();
            //var entitiesAll = ChangeTracker
            //    .Entries()
            //    .ToList();
            //foreach (var entity in entitiesWithEventsTracked)
            //{
            //    var events = entity.Events.ToArray();
            //    entity.Events.Clear();
            //    foreach (var domainEvent in events)
            //    {
            //        await _mediator.Publish(domainEvent).ConfigureAwait(false);
            //    }
            //}
            return result;
        }
        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var aa = 5;
            return base.AddAsync(entity, cancellationToken);
        }
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }
        public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        {
            return base.Attach(entity);
        }
        public override EntityEntry Attach(object entity)
        {
            return base.Attach(entity);
        }
        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            return base.Update(entity);
        }
    }
}