using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.PhoneNumberEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdatePhoneNumberRequest>
    .WithActionResult<UpdatePhoneNumberResponse>
  {
    private readonly IRepository<PhoneNumber> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<PhoneNumber> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/phoneNumbers")]
    [SwaggerOperation(
        Summary = "Updates a PhoneNumber",
        Description = "Updates a PhoneNumber",
        OperationId = "phoneNumbers.update",
        Tags = new[] { "PhoneNumberEndpoints" })
    ]
    public override async Task<ActionResult<UpdatePhoneNumberResponse>> HandleAsync(UpdatePhoneNumberRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdatePhoneNumberResponse(request.CorrelationId());
            try
            {
                var pnhnonToUpdate = _mapper.Map<PhoneNumber>(request);
      await _repository.UpdateAsync(pnhnonToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonPhoneNumber = JsonConvert.SerializeObject(pnhnonToUpdate, settings);
                PhoneNumberDto finalPhoneNumberDto = JsonConvert.DeserializeObject<PhoneNumberDto>(jsonPhoneNumber, settings);
                response.PhoneNumber = finalPhoneNumberDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating phoneNumber with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
