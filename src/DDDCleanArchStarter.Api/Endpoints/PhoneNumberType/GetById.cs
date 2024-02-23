using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.PhoneNumberTypeEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdPhoneNumberTypeRequest>.WithActionResult<
        GetByIdPhoneNumberTypeResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumberType> _repository;

        public GetById(
            IRepository<PhoneNumberType> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/phoneNumberTypes/{PhoneNumberTypeId}")]
        [SwaggerOperation(
            Summary = "Get a PhoneNumberType by Id",
            Description = "Gets a PhoneNumberType by Id",
            OperationId = "phoneNumberTypes.GetById",
            Tags = new[] { "PhoneNumberTypeEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdPhoneNumberTypeResponse>> HandleAsync(
            [FromRoute] GetByIdPhoneNumberTypeRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdPhoneNumberTypeResponse(request.CorrelationId());
            try
            {
                var phoneNumberType = await _repository.GetByIdAsync(request.PhoneNumberTypeId, cancellationToken);
                if (phoneNumberType is null)
                {
                    var errorMsg = $"PhoneNumberType with ID {request.PhoneNumberTypeId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonPhoneNumberType = JsonConvert.SerializeObject(phoneNumberType, settings);
                PhoneNumberTypeDto finalPhoneNumberTypeDto = JsonConvert.DeserializeObject<PhoneNumberTypeDto>(jsonPhoneNumberType, settings);
                response.PhoneNumberType = finalPhoneNumberTypeDto;
                _logger.LogInformation($"Successfully fetched phoneNumberType with ID {request.PhoneNumberTypeId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching phoneNumberType with ID {request.PhoneNumberTypeId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}