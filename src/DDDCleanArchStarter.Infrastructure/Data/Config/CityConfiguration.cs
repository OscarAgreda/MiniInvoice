using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CityConfiguration
        : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "dbo");
            builder.HasKey(t => t.CityId);
            builder.Property(t => t.CityId)
                .IsRequired()
                .HasColumnName("CityId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.CityName)
                .IsRequired()
                .HasColumnName("CityName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.StateId)
                .IsRequired()
                .HasColumnName("StateId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.State)
                .WithMany(t => t.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_City_State");
        }
    }
}
