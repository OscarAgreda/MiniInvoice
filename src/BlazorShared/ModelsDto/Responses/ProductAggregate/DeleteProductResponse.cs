using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Product
{
    public class DeleteProductResponse : BaseResponse
    {
        public DeleteProductResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteProductResponse()
        {
        }
    }
}
