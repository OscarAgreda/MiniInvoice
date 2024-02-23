using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.City
{
    public class UpdateCityRequest : BaseRequest
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public Guid StateId { get; set; }
        public Guid TenantId { get; set; }

        public static UpdateCityRequest FromDto(CityDto cityDto)
        {
            return new UpdateCityRequest
            {
                CityId = cityDto.CityId,
                StateId = cityDto.StateId,
                CityName = cityDto.CityName,
                TenantId = cityDto.TenantId
            };
        }
    }
}