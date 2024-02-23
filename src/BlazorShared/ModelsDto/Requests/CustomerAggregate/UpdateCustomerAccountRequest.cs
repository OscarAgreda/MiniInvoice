using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAccount
{
    public class UpdateCustomerAccountRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public int RowId { get; set; }

        public static UpdateCustomerAccountRequest FromDto(CustomerAccountDto customerAccountDto)
        {
            return new UpdateCustomerAccountRequest
            {
                RowId = customerAccountDto.RowId,
                AccountId = customerAccountDto.AccountId,
                CustomerId = customerAccountDto.CustomerId
            };
        }
    }
}