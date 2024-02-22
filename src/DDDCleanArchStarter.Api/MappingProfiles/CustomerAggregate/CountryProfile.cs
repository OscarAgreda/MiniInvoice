using AutoMapper;
using BlazorMauiShared.Models.Country;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<CreateCountryRequest, Country>();
            CreateMap<UpdateCountryRequest, Country>();
            CreateMap<DeleteCountryRequest, Country>();
        }
    }
}
