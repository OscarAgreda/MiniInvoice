using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class AccountAdjustmentConfiguration
        : IEntityTypeConfiguration<AccountAdjustment>
    {
        public void Configure(EntityTypeBuilder<AccountAdjustment> builder)
        {
            builder.ToTable("AccountAdjustment", "dbo");
            builder.HasKey(t => t.AccountAdjustmentId);
            builder.Property(t => t.AccountAdjustmentId)
                .IsRequired()
                .HasColumnName("AccountAdjustmentId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.AdjustmentReason)
                .IsRequired()
                .HasColumnName("AdjustmentReason")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.TotalChargeCredited)
                .IsRequired()
                .HasColumnName("TotalChargeCredited")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalTaxCredited)
                .IsRequired()
                .HasColumnName("TotalTaxCredited")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");
            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("AccountId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Account)
                .WithMany(t => t.AccountAdjustments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_AccountAdjustment_Account");
        }
    }
}
