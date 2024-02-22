using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class AccountConfiguration
        : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "dbo");
            builder.HasKey(t => t.AccountId);
            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("AccountId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.AccountNumber)
                .IsRequired()
                .HasColumnName("AccountNumber")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.AccountName)
                .IsRequired()
                .HasColumnName("AccountName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.AccountDescription)
                .IsRequired()
                .HasColumnName("AccountDescription")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.AccountTypeId)
                .IsRequired()
                .HasColumnName("AccountTypeId")
                .HasColumnType("uniqueidentifier");
        }
    }
}
