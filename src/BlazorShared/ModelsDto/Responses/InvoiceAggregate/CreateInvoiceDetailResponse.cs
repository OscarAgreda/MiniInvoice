using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class CreateInvoiceDetailResponse : BaseResponse
    {
        public CreateInvoiceDetailResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateInvoiceDetailResponse()
        {
        }
        public InvoiceDetailDto InvoiceDetail { get; set; } = new();
    }
}
