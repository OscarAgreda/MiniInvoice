using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class PhoneNumberTypeProfile : Profile
    {
        public PhoneNumberTypeProfile()
        {
            CreateMap<PhoneNumberType, PhoneNumberTypeDto>();
            CreateMap<PhoneNumberTypeDto, PhoneNumberType>();
            CreateMap<CreatePhoneNumberTypeRequest, PhoneNumberType>();
            CreateMap<UpdatePhoneNumberTypeRequest, PhoneNumberType>();
            CreateMap<DeletePhoneNumberTypeRequest, PhoneNumberType>();
        }
    }
}
