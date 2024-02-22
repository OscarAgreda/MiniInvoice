using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDInvoicingClean.Domain.Entities;
namespace SaamApp.Infrastructure.Data.Config
{
    public  class ProductCategoryConfiguration
        : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory", "dbo");
            builder.HasKey(t => t.RowId);
            builder.Property(t => t.RowId)
                .IsRequired()
                .HasColumnName("RowId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
            builder.Property(t => t.TenantId)
                .IsRequired()
                .HasColumnName("TenantId")
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("ProductId")
                .HasColumnType("uniqueidentifier");
            builder.HasOne(t => t.Product)
                .WithMany(t => t.ProductCategories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductCategory_Product");
        }
    }
}
