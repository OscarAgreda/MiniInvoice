using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Country
{
    public class GetByIdCountryResponse : BaseResponse
    {
        public GetByIdCountryResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdCountryResponse()
        {
        }

        public CountryDto Country { get; set; } = new();
    }
}