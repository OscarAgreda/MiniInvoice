using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Account
{
    public class UpdateAccountRequest : BaseRequest
    {
      public Guid AccountId { get; set; }
      public string AccountNumber { get; set; }
      public string AccountName { get; set; }
      public string AccountDescription { get; set; }
      public bool? IsDeleted { get; set; }
      public Guid TenantId { get; set; }
      public Guid AccountTypeId { get; set; }
        public static UpdateAccountRequest FromDto(AccountDto accountDto)
        {
            return new UpdateAccountRequest
            {
                AccountId = accountDto.AccountId,
                AccountNumber = accountDto.AccountNumber,
                AccountName = accountDto.AccountName,
                AccountDescription = accountDto.AccountDescription,
                IsDeleted = accountDto.IsDeleted,
                TenantId = accountDto.TenantId,
                AccountTypeId = accountDto.AccountTypeId
            };
        }
    }
}
