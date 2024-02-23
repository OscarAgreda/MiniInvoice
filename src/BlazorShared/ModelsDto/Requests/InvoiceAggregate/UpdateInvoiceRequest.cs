using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Invoice
{
    public class UpdateInvoiceRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string? InternalComments { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public Guid InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public string InvoicingNote { get; set; }
        public int PaymentState { get; set; }
        public Guid TenantId { get; set; }
        public decimal? TotalSale { get; set; }
        public decimal? TotalSaleTax { get; set; }

        public static UpdateInvoiceRequest FromDto(InvoiceDto invoiceDto)
        {
            return new UpdateInvoiceRequest
            {
                InvoiceId = invoiceDto.InvoiceId,
                AccountId = invoiceDto.AccountId,
                InvoiceNumber = invoiceDto.InvoiceNumber,
                AccountName = invoiceDto.AccountName,
                CustomerName = invoiceDto.CustomerName,
                PaymentState = invoiceDto.PaymentState,
                InternalComments = invoiceDto.InternalComments,
                InvoicedDate = invoiceDto.InvoicedDate,
                InvoicingNote = invoiceDto.InvoicingNote,
                TotalSale = invoiceDto.TotalSale,
                TotalSaleTax = invoiceDto.TotalSaleTax,
                TenantId = invoiceDto.TenantId
            };
        }
    }
}