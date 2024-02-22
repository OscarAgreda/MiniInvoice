using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class InvoiceDetailProfile : Profile
    {
        public InvoiceDetailProfile()
        {
            CreateMap<InvoiceDetail, InvoiceDetailDto>();
            CreateMap<InvoiceDetailDto, InvoiceDetail>();
            CreateMap<CreateInvoiceDetailRequest, InvoiceDetail>();
            CreateMap<UpdateInvoiceDetailRequest, InvoiceDetail>();
            CreateMap<DeleteInvoiceDetailRequest, InvoiceDetail>();
        }
    }
}
