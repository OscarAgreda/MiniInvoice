using AutoMapper;
using BlazorMauiShared.Models.Customer;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<UpdateCustomerRequest, Customer>();
            CreateMap<DeleteCustomerRequest, Customer>();
        }
    }
}
