using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.ProductCategory
{
    public class CreateProductCategoryResponse : BaseResponse
    {
        public CreateProductCategoryResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateProductCategoryResponse()
        {
        }
        public ProductCategoryDto ProductCategory { get; set; } = new();
    }
}
