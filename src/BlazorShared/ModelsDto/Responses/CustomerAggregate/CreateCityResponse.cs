using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.City
{
    public class CreateCityResponse : BaseResponse
    {
        public CreateCityResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateCityResponse()
        {
        }

        public CityDto City { get; set; } = new();
    }
}