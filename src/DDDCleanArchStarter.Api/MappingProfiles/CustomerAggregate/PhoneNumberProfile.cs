using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumberDto, PhoneNumber>();
            CreateMap<CreatePhoneNumberRequest, PhoneNumber>();
            CreateMap<UpdatePhoneNumberRequest, PhoneNumber>();
            CreateMap<DeletePhoneNumberRequest, PhoneNumber>();
        }
    }
}
