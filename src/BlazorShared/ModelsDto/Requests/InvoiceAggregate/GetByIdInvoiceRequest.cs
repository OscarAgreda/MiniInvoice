using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Invoice
{
    public class GetByIdInvoiceRequest : BaseRequest
    {
        public Guid InvoiceId { get; set; }
    }
}