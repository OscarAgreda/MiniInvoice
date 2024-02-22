using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Country
{
    public class UpdateCountryRequest : BaseRequest
    {
      public Guid CountryId { get; set; }
      public string CountryName { get; set; }
      public string CountryCode { get; set; }
      public Guid TenantId { get; set; }
        public static UpdateCountryRequest FromDto(CountryDto countryDto)
        {
            return new UpdateCountryRequest
            {
                CountryId = countryDto.CountryId,
                CountryName = countryDto.CountryName,
                CountryCode = countryDto.CountryCode,
                TenantId = countryDto.TenantId
            };
        }
    }
}
