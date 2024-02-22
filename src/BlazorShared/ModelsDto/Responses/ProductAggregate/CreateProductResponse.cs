using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Product
{
    public class CreateProductResponse : BaseResponse
    {
        public CreateProductResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateProductResponse()
        {
        }
        public ProductDto Product { get; set; } = new();
    }
}
