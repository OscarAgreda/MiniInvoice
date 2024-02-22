using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Country
{
    public class ListCountryResponse : BaseResponse
    {
        public ListCountryResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public ListCountryResponse()
        {
        }
        public List<CountryDto> Countries { get; set; } = new();
        public int Count { get; set; }
    }
}
