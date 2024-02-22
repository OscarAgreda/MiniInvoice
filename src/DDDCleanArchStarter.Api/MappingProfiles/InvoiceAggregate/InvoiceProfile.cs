using AutoMapper;
using BlazorMauiShared.Models.Invoice;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();
            CreateMap<CreateInvoiceRequest, Invoice>();
            CreateMap<UpdateInvoiceRequest, Invoice>();
            CreateMap<DeleteInvoiceRequest, Invoice>();
        }
    }
}
