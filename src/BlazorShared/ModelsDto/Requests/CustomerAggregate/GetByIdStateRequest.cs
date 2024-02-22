using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.State
{
    public class GetByIdStateRequest : BaseRequest
    {
        public Guid StateId { get; set; }
    }
}
