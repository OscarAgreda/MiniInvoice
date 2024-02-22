using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.State
{
    public class DeleteStateRequest : BaseRequest
    {
        public Guid StateId { get; set; }
    }
}
