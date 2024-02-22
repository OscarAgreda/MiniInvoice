using AutoMapper;
using BlazorMauiShared.Models.City;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<CreateCityRequest, City>();
            CreateMap<UpdateCityRequest, City>();
            CreateMap<DeleteCityRequest, City>();
        }
    }
}
