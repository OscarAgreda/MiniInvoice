using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.ProductCategory
{
    public class DeleteProductCategoryResponse : BaseResponse
    {
        public DeleteProductCategoryResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteProductCategoryResponse()
        {
        }
    }
}
