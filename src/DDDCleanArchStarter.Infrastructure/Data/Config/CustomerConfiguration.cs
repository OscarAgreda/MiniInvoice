using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class CustomerConfiguration
        : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "dbo");
            builder.HasKey(t => t.CustomerId);
            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.CustomerFirstName)
                .IsRequired()
                .HasColumnName("CustomerFirstName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerLastName)
                .IsRequired()
                .HasColumnName("CustomerLastName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerBirthDate)
                .HasColumnName("CustomerBirthDate")
                .HasColumnType("datetime2");
            builder.Property(t => t.CustomerWebsite)
                .HasColumnName("CustomerWebsite")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CreditLimit)
                .HasColumnName("CreditLimit")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.Notes)
                .HasColumnName("Notes")
                .HasColumnType("nvarchar(999)")
                .HasMaxLength(999);
            builder.Property(t => t.CustomerMiddleName)
                .HasColumnName("CustomerMiddleName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerTitle)
                .HasColumnName("CustomerTitle")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
            builder.Property(t => t.CustomerGender)
                .HasColumnName("CustomerGender")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);
            builder.Property(t => t.CustomerCompanyName)
                .HasColumnName("CustomerCompanyName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerJobTitle)
                .HasColumnName("CustomerJobTitle")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.Industry)
                .HasColumnName("Industry")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.PreferredContactMethod)
                .HasColumnName("PreferredContactMethod")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
            builder.Property(t => t.CustomerStatus)
                .HasColumnName("CustomerStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
            builder.Property(t => t.LoyaltyPoints)
                .HasColumnName("LoyaltyPoints")
                .HasColumnType("int");
            builder.Property(t => t.Source)
                .HasColumnName("Source")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.LastPurchaseDate)
                .HasColumnName("LastPurchaseDate")
                .HasColumnType("datetime2");
            builder.Property(t => t.TotalPurchases)
                .HasColumnName("TotalPurchases")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit");
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CustomerDecimal)
                .HasColumnName("CustomerDecimal")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.CustomerInt)
                .IsRequired()
                .HasColumnName("CustomerInt")
                .HasColumnType("int");
            builder.Property(t => t.CustomerDateTime2)
                .IsRequired()
                .HasColumnName("CustomerDateTime2")
                .HasColumnType("datetime2");
            builder.Property(t => t.CustomerDateTime)
                .IsRequired()
                .HasColumnName("CustomerDateTime")
                .HasColumnType("datetime");
            builder.Property(t => t.CustomerBit)
                .IsRequired()
                .HasColumnName("CustomerBit")
                .HasColumnType("bit");
            builder.Property(t => t.CustomerFloat)
                .HasColumnName("CustomerFloat")
                .HasColumnType("float");
            builder.Property(t => t.CustomerReal)
                .HasColumnName("CustomerReal")
                .HasColumnType("real");
            builder.Property(t => t.CustomerBigInt)
                .HasColumnName("CustomerBigInt")
                .HasColumnType("bigint");
            builder.Property(t => t.CustomerSmallInt)
                .HasColumnName("CustomerSmallInt")
                .HasColumnType("smallint");
            builder.Property(t => t.CustomerTinyInt)
                .HasColumnName("CustomerTinyInt")
                .HasColumnType("tinyint");
            builder.Property(t => t.CustomerChar)
                .HasColumnName("CustomerChar")
                .HasColumnType("char(10)")
                .HasMaxLength(10);
            builder.Property(t => t.CustomerVarChar)
                .HasColumnName("CustomerVarChar")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerText)
                .HasColumnName("CustomerText")
                .HasColumnType("text");
            builder.Property(t => t.CustomerNChar)
                .HasColumnName("CustomerNChar")
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
            builder.Property(t => t.CustomerNVarChar)
                .HasColumnName("CustomerNVarChar")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerNText)
                .HasColumnName("CustomerNText")
                .HasColumnType("ntext");
            builder.Property(t => t.CustomerBinary)
                .HasColumnName("CustomerBinary")
                .HasColumnType("binary(50)")
                .HasMaxLength(50);
            builder.Property(t => t.CustomerVarBinary)
                .HasColumnName("CustomerVarBinary")
                .HasColumnType("varbinary(255)")
                .HasMaxLength(255);
            builder.Property(t => t.CustomerImage)
                .HasColumnName("CustomerImage")
                .HasColumnType("image");
            builder.Property(t => t.CustomerMoney)
                .HasColumnName("CustomerMoney")
                .HasColumnType("money");
            builder.Property(t => t.CustomerSmallMoney)
                .HasColumnName("CustomerSmallMoney")
                .HasColumnType("smallmoney");
            builder.Property(t => t.CustomerTimestamp)
                .IsRowVersion()
                .HasColumnName("CustomerTimestamp")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();
            builder.Property(t => t.CustomerUniqueIdentifier)
                .HasColumnName("CustomerUniqueIdentifier")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.CustomerXml)
                .HasColumnName("CustomerXml")
                .HasColumnType("xml");
        }
    }
}
