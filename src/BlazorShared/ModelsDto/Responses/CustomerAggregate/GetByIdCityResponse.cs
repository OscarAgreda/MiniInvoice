using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.City
{
    public class GetByIdCityResponse : BaseResponse
    {
        public GetByIdCityResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdCityResponse()
        {
        }

        public CityDto City { get; set; } = new();
    }
}