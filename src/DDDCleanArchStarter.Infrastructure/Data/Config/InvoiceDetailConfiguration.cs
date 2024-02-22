using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class InvoiceDetailConfiguration
        : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetail", "dbo");
            builder.HasKey(t => t.InvoiceDetailId);
            builder.Property(t => t.InvoiceDetailId)
                .IsRequired()
                .HasColumnName("InvoiceDetailId")
                .HasColumnType("uniqueidentifier").ValueGeneratedNever();
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.ProductName)
                .IsRequired()
                .HasColumnName("ProductName")
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255);
            builder.Property(t => t.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.LineSale)
                .HasColumnName("LineSale")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.LineTax)
                .HasColumnName("LineTax")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.LineDiscount)
                .HasColumnName("LineDiscount")
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.InvoiceId)
                .IsRequired()
                .HasColumnName("InvoiceId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("ProductId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Invoice)
                .WithMany(t => t.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceDetail_Invoice");
            builder.HasOne(t => t.Product)
                .WithMany(t => t.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_InvoiceDetail_Product");
        }
    }
}
