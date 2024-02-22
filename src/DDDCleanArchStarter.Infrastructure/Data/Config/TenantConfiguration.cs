using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class TenantConfiguration
        : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant", "dbo");
            builder.HasKey(t => t.TenantId);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.Logo)
                .HasColumnName("Logo")
                .HasColumnType("varbinary(max)");
            builder.Property(t => t.ContactPerson)
                .HasColumnName("ContactPerson")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.BillingFrequency)
                .HasColumnName("BillingFrequency")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.NextBillingDate)
                .HasColumnName("NextBillingDate")
                .HasColumnType("datetime2");
            builder.Property(t => t.PaymentMethod)
                .HasColumnName("PaymentMethod")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.IsSuspended)
                .IsRequired()
                .HasColumnName("IsSuspended")
                .HasColumnType("bit");
            builder.Property(t => t.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.SettingsJson)
                .HasColumnName("SettingsJson")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");
        }
    }
}
