using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Product
{
    public class GetByIdProductResponse : BaseResponse
    {
        public GetByIdProductResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdProductResponse()
        {
        }

        public ProductDto Product { get; set; } = new();
    }
}