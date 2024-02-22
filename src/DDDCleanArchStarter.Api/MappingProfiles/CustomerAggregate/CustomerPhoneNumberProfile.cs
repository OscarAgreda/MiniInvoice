using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CustomerPhoneNumberProfile : Profile
    {
        public CustomerPhoneNumberProfile()
        {
            CreateMap<CustomerPhoneNumber, CustomerPhoneNumberDto>();
            CreateMap<CustomerPhoneNumberDto, CustomerPhoneNumber>();
            CreateMap<CreateCustomerPhoneNumberRequest, CustomerPhoneNumber>();
            CreateMap<UpdateCustomerPhoneNumberRequest, CustomerPhoneNumber>();
            CreateMap<DeleteCustomerPhoneNumberRequest, CustomerPhoneNumber>();
        }
    }
}
