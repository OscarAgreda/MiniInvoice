using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CountryConfiguration
        : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country", "dbo");
            builder.HasKey(t => t.CountryId);
            builder.Property(t => t.CountryId)
                .IsRequired()
                .HasColumnName("CountryId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.CountryName)
                .IsRequired()
                .HasColumnName("CountryName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CountryCode)
                .IsRequired()
                .HasColumnName("CountryCode")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
        }
    }
}
