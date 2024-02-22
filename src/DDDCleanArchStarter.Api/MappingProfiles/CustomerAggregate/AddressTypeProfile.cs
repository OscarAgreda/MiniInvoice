using AutoMapper;
using BlazorMauiShared.Models.AddressType;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class AddressTypeProfile : Profile
    {
        public AddressTypeProfile()
        {
            CreateMap<AddressType, AddressTypeDto>();
            CreateMap<AddressTypeDto, AddressType>();
            CreateMap<CreateAddressTypeRequest, AddressType>();
            CreateMap<UpdateAddressTypeRequest, AddressType>();
            CreateMap<DeleteAddressTypeRequest, AddressType>();
        }
    }
}
