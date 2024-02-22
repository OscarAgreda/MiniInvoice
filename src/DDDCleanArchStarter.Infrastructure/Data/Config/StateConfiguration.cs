using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class StateConfiguration
        : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("State", "dbo");
            builder.HasKey(t => t.StateId);
            builder.Property(t => t.StateId)
                .IsRequired()
                .HasColumnName("StateId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.StateName)
                .IsRequired()
                .HasColumnName("StateName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CountryId)
                .IsRequired()
                .HasColumnName("CountryId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Country)
                .WithMany(t => t.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_State_Country");
        }
    }
}
