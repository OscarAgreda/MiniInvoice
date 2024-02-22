using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class AccountAdjustmentProfile : Profile
    {
        public AccountAdjustmentProfile()
        {
            CreateMap<AccountAdjustment, AccountAdjustmentDto>();
            CreateMap<AccountAdjustmentDto, AccountAdjustment>();
            CreateMap<CreateAccountAdjustmentRequest, AccountAdjustment>();
            CreateMap<UpdateAccountAdjustmentRequest, AccountAdjustment>();
            CreateMap<DeleteAccountAdjustmentRequest, AccountAdjustment>();
        }
    }
}
