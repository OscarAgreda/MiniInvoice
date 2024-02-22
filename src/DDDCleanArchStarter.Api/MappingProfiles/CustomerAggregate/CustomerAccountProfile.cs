using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CustomerAccountProfile : Profile
    {
        public CustomerAccountProfile()
        {
            CreateMap<CustomerAccount, CustomerAccountDto>();
            CreateMap<CustomerAccountDto, CustomerAccount>();
            CreateMap<CreateCustomerAccountRequest, CustomerAccount>();
            CreateMap<UpdateCustomerAccountRequest, CustomerAccount>();
            CreateMap<DeleteCustomerAccountRequest, CustomerAccount>();
        }
    }
}
