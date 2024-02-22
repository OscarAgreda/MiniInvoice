using System;
namespace BlazorShared.Models
{
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correlationId)
        {
            _correlationId = correlationId;
        }
        public BaseResponse()
        {
        }
        public bool IsSuccess { get; set; } = false;
        public string ErrorMessage { get; set; }
    }
}