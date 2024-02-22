using AutoMapper;
using BlazorMauiShared.Models.Address;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<CreateAddressRequest, Address>();
            CreateMap<UpdateAddressRequest, Address>();
            CreateMap<DeleteAddressRequest, Address>();
        }
    }
}
