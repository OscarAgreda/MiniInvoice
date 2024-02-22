using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CustomerAccountConfiguration
        : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.ToTable("CustomerAccount", "dbo");
            builder.HasKey(t => t.RowId);
            builder.Property(t => t.RowId)
                .IsRequired()
                .HasColumnName("RowId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("AccountId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Account)
                .WithMany(t => t.CustomerAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_CustomerAccount_Account");
            builder.HasOne(t => t.Customer)
                .WithMany(t => t.CustomerAccounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerAccount_Customer");
        }
    }
}
