using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class GetByIdInvoiceDetailResponse : BaseResponse
    {
        public GetByIdInvoiceDetailResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdInvoiceDetailResponse()
        {
        }

        public InvoiceDetailDto InvoiceDetail { get; set; } = new();
    }
}