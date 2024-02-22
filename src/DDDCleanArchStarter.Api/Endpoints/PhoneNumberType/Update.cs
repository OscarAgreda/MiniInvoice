using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.PhoneNumberTypeEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdatePhoneNumberTypeRequest>
    .WithActionResult<UpdatePhoneNumberTypeResponse>
  {
    private readonly IRepository<PhoneNumberType> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<PhoneNumberType> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/phoneNumberTypes")]
    [SwaggerOperation(
        Summary = "Updates a PhoneNumberType",
        Description = "Updates a PhoneNumberType",
        OperationId = "phoneNumberTypes.update",
        Tags = new[] { "PhoneNumberTypeEndpoints" })
    ]
    public override async Task<ActionResult<UpdatePhoneNumberTypeResponse>> HandleAsync(UpdatePhoneNumberTypeRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdatePhoneNumberTypeResponse(request.CorrelationId());
            try
            {
                var pnthntontToUpdate = _mapper.Map<PhoneNumberType>(request);
      await _repository.UpdateAsync(pnthntontToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonPhoneNumberType = JsonConvert.SerializeObject(pnthntontToUpdate, settings);
                PhoneNumberTypeDto finalPhoneNumberTypeDto = JsonConvert.DeserializeObject<PhoneNumberTypeDto>(jsonPhoneNumberType, settings);
                response.PhoneNumberType = finalPhoneNumberTypeDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating phoneNumberType with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
