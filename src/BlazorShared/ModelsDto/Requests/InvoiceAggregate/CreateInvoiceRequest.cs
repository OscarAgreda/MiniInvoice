using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Invoice
{
    public class CreateInvoiceRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string? InternalComments { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public int InvoiceNumber { get; set; }
        public string InvoicingNote { get; set; }
        public int PaymentState { get; set; }
        public Guid TenantId { get; set; }
        public decimal? TotalSale { get; set; }
        public decimal? TotalSaleTax { get; set; }
    }
}