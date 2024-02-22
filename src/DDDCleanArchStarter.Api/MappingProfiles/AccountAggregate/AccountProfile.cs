using AutoMapper;
using BlazorMauiShared.Models.Account;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<CreateAccountRequest, Account>();
            CreateMap<UpdateAccountRequest, Account>();
            CreateMap<DeleteAccountRequest, Account>();
        }
    }
}
