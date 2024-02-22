using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Country
{
    public class CreateCountryResponse : BaseResponse
    {
        public CreateCountryResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateCountryResponse()
        {
        }
        public CountryDto Country { get; set; } = new();
    }
}
