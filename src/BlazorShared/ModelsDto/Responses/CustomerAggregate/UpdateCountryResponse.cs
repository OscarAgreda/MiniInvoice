using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Country
{
    public class UpdateCountryResponse : BaseResponse
    {
        public UpdateCountryResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateCountryResponse()
        {
        }

        public CountryDto Country { get; set; } = new();
    }
}