using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AddressType
{
    public class UpdateAddressTypeRequest : BaseRequest
    {
      public Guid AddressTypeId { get; set; }
      public string AddressTypeName { get; set; }
      public string? Description { get; set; }
      public Guid TenantId { get; set; }
        public static UpdateAddressTypeRequest FromDto(AddressTypeDto addressTypeDto)
        {
            return new UpdateAddressTypeRequest
            {
                AddressTypeId = addressTypeDto.AddressTypeId,
                AddressTypeName = addressTypeDto.AddressTypeName,
                Description = addressTypeDto.Description,
                TenantId = addressTypeDto.TenantId
            };
        }
    }
}
