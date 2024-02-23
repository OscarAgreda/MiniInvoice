using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class UpdatePhoneNumberTypeRequest : BaseRequest
    {
        public string? Description { get; set; }
        public Guid PhoneNumberTypeId { get; set; }
        public string PhoneNumberTypeName { get; set; }
        public Guid TenantId { get; set; }

        public static UpdatePhoneNumberTypeRequest FromDto(PhoneNumberTypeDto phoneNumberTypeDto)
        {
            return new UpdatePhoneNumberTypeRequest
            {
                PhoneNumberTypeId = phoneNumberTypeDto.PhoneNumberTypeId,
                PhoneNumberTypeName = phoneNumberTypeDto.PhoneNumberTypeName,
                Description = phoneNumberTypeDto.Description,
                TenantId = phoneNumberTypeDto.TenantId
            };
        }
    }
}