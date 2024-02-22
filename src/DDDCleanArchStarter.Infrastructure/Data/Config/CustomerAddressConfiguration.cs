using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CustomerAddressConfiguration
        : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddress", "dbo");
            builder.HasKey(t => t.RowId);
            builder.Property(t => t.RowId)
                .IsRequired()
                .HasColumnName("RowId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
            builder.Property(t => t.AddressId)
                .IsRequired()
                .HasColumnName("AddressId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.AddressTypeId)
                .IsRequired()
                .HasColumnName("AddressTypeId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Address)
                .WithMany(t => t.CustomerAddresses)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_CustomerAddress_Address");
            builder.HasOne(t => t.Customer)
                .WithMany(t => t.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerAddress_Customer");
        }
    }
}
