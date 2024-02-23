using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class UpdateProductCategoryRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
        public int RowId { get; set; }
        public Guid TenantId { get; set; }

        public static UpdateProductCategoryRequest FromDto(ProductCategoryDto productCategoryDto)
        {
            return new UpdateProductCategoryRequest
            {
                RowId = productCategoryDto.RowId,
                ProductId = productCategoryDto.ProductId,
                TenantId = productCategoryDto.TenantId
            };
        }
    }
}