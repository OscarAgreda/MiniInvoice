using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.City
{
    public class UpdateCityResponse : BaseResponse
    {
        public UpdateCityResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateCityResponse()
        {
        }
        public CityDto City { get; set; } = new();
    }
}
