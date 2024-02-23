using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.PhoneNumberEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdatePhoneNumberRequest>
      .WithActionResult<UpdatePhoneNumberResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumber> _repository;

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