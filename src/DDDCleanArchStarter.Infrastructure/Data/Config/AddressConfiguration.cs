using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class AddressConfiguration
        : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "dbo");
            builder.HasKey(t => t.AddressId);
            builder.Property(t => t.AddressId)
                .IsRequired()
                .HasColumnName("AddressId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.AddressStr)
                .IsRequired()
                .HasColumnName("AddressStr")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.ZipCode)
                .IsRequired()
                .HasColumnName("ZipCode")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CityId)
                .IsRequired()
                .HasColumnName("CityId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CountryId)
                .IsRequired()
                .HasColumnName("CountryId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.StateId)
                .IsRequired()
                .HasColumnName("StateId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.City)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Address_City");
            builder.HasOne(t => t.Country)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Address_Country");
            builder.HasOne(t => t.State)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Address_State");
        }
    }
}
