using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CustomerPhoneNumberConfiguration
        : IEntityTypeConfiguration<CustomerPhoneNumber>
    {
        public void Configure(EntityTypeBuilder<CustomerPhoneNumber> builder)
        {
            builder.ToTable("CustomerPhoneNumber", "dbo");
            builder.HasKey(t => t.RowId);
            builder.Property(t => t.RowId)
                .IsRequired()
                .HasColumnName("RowId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.PhoneNumberId)
                .IsRequired()
                .HasColumnName("PhoneNumberId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.PhoneHasBeenVerified)
                .IsRequired()
                .HasColumnName("PhoneHasBeenVerified")
                .HasColumnType("bit");
            builder.Property(t => t.PhoneNumberTypeId)
                .IsRequired()
                .HasColumnName("PhoneNumberTypeId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Customer)
                .WithMany(t => t.CustomerPhoneNumbers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerPhoneNumber_Customer");
            builder.HasOne(t => t.PhoneNumber)
                .WithMany(t => t.CustomerPhoneNumbers)
                .HasForeignKey(d => d.PhoneNumberId)
                .HasConstraintName("FK_CustomerPhoneNumber_PhoneNumber");
        }
    }
}
