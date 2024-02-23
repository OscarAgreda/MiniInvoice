using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class UpdateProductCategoryResponse : BaseResponse
    {
        public UpdateProductCategoryResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateProductCategoryResponse()
        {
        }

        public ProductCategoryDto ProductCategory { get; set; } = new();
    }
}