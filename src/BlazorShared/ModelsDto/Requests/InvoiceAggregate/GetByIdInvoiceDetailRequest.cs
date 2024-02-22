using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class GetByIdInvoiceDetailRequest : BaseRequest
    {
        public Guid InvoiceDetailId { get; set; }
    }
}
