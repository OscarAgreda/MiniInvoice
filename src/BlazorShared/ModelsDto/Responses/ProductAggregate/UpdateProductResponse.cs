using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Product
{
    public class UpdateProductResponse : BaseResponse
    {
        public UpdateProductResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateProductResponse()
        {
        }
        public ProductDto Product { get; set; } = new();
    }
}
