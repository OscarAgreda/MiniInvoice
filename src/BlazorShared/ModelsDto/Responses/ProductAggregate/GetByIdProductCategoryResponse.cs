using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class GetByIdProductCategoryResponse : BaseResponse
    {
        public GetByIdProductCategoryResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdProductCategoryResponse()
        {
        }

        public ProductCategoryDto ProductCategory { get; set; } = new();
    }
}