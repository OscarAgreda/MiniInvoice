using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CustomerAddressProfile : Profile
    {
        public CustomerAddressProfile()
        {
            CreateMap<CustomerAddress, CustomerAddressDto>();
            CreateMap<CustomerAddressDto, CustomerAddress>();
            CreateMap<CreateCustomerAddressRequest, CustomerAddress>();
            CreateMap<UpdateCustomerAddressRequest, CustomerAddress>();
            CreateMap<DeleteCustomerAddressRequest, CustomerAddress>();
        }
    }
}
