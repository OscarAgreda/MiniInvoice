using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Invoice
{
    public class DeleteInvoiceResponse : BaseResponse
    {
        public DeleteInvoiceResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteInvoiceResponse()
        {
        }
    }
}
