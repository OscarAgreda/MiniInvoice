using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class UpdateCustomerPhoneNumberRequest : BaseRequest
    {
      public int RowId { get; set; }
      public Guid CustomerId { get; set; }
      public Guid PhoneNumberId { get; set; }
      public Guid PhoneNumberTypeId { get; set; }
      public bool PhoneHasBeenVerified { get; set; }
        public static UpdateCustomerPhoneNumberRequest FromDto(CustomerPhoneNumberDto customerPhoneNumberDto)
        {
            return new UpdateCustomerPhoneNumberRequest
            {
                RowId = customerPhoneNumberDto.RowId,
                CustomerId = customerPhoneNumberDto.CustomerId,
                PhoneNumberId = customerPhoneNumberDto.PhoneNumberId,
                PhoneNumberTypeId = customerPhoneNumberDto.PhoneNumberTypeId,
                PhoneHasBeenVerified = customerPhoneNumberDto.PhoneHasBeenVerified
            };
        }
    }
}
