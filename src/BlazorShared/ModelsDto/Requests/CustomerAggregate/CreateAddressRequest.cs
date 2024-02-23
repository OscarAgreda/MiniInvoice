using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Address
{
    public class CreateAddressRequest : BaseRequest
    {
        public string AddressStr { get; set; }
        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid TenantId { get; set; }
        public string ZipCode { get; set; }
    }
}