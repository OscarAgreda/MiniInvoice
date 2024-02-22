using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class AddressTypeConfiguration
        : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.ToTable("AddressType", "dbo");
            builder.HasKey(t => t.AddressTypeId);
            builder.Property(t => t.AddressTypeId)
                .IsRequired()
                .HasColumnName("AddressTypeId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.AddressTypeName)
                .IsRequired()
                .HasColumnName("AddressTypeName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
        }
    }
}
