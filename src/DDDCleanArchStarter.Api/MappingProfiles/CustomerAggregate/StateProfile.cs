using AutoMapper;
using BlazorMauiShared.Models.State;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, StateDto>();
            CreateMap<StateDto, State>();
            CreateMap<CreateStateRequest, State>();
            CreateMap<UpdateStateRequest, State>();
            CreateMap<DeleteStateRequest, State>();
        }
    }
}
