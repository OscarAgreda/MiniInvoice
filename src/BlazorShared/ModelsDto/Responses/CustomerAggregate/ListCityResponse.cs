using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.City
{
    public class ListCityResponse : BaseResponse
    {
        public ListCityResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public ListCityResponse()
        {
        }
        public List<CityDto> Cities { get; set; } = new();
        public int Count { get; set; }
    }
}
