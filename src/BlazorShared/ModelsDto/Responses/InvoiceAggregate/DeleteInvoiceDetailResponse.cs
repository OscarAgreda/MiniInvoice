using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class DeleteInvoiceDetailResponse : BaseResponse
    {
        public DeleteInvoiceDetailResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteInvoiceDetailResponse()
        {
        }
    }
}
