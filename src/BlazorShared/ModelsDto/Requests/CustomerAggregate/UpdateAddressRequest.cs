using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Address
{
    public class UpdateAddressRequest : BaseRequest
    {
      public Guid AddressId { get; set; }
      public Guid CityId { get; set; }
      public Guid CountryId { get; set; }
      public Guid StateId { get; set; }
      public string AddressStr { get; set; }
      public string ZipCode { get; set; }
      public Guid TenantId { get; set; }
        public static UpdateAddressRequest FromDto(AddressDto addressDto)
        {
            return new UpdateAddressRequest
            {
                AddressId = addressDto.AddressId,
                CityId = addressDto.CityId,
                CountryId = addressDto.CountryId,
                StateId = addressDto.StateId,
                AddressStr = addressDto.AddressStr,
                ZipCode = addressDto.ZipCode,
                TenantId = addressDto.TenantId
            };
        }
    }
}
