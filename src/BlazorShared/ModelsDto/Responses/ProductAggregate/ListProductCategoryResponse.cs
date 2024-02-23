using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class ListProductCategoryResponse : BaseResponse
    {
        public ListProductCategoryResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListProductCategoryResponse()
        {
        }

        public int Count { get; set; }
        public List<ProductCategoryDto> ProductCategories { get; set; } = new();
    }
}