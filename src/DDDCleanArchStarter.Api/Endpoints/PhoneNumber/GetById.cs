using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.PhoneNumberEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdPhoneNumberRequest>.WithActionResult<
        GetByIdPhoneNumberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<PhoneNumber> _repository;
        public GetById(
            IRepository<PhoneNumber> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/phoneNumbers/{PhoneNumberId}")]
        [SwaggerOperation(
            Summary = "Get a PhoneNumber by Id",
            Description = "Gets a PhoneNumber by Id",
            OperationId = "phoneNumbers.GetById",
            Tags = new[] { "PhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdPhoneNumberResponse>> HandleAsync(
            [FromRoute] GetByIdPhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdPhoneNumberResponse(request.CorrelationId());
            try
            {
                var phoneNumber = await _repository.GetByIdAsync(request.PhoneNumberId, cancellationToken);
                if (phoneNumber is null)
                {
                    var errorMsg = $"PhoneNumber with ID {request.PhoneNumberId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonPhoneNumber = JsonConvert.SerializeObject(phoneNumber, settings);
                PhoneNumberDto finalPhoneNumberDto = JsonConvert.DeserializeObject<PhoneNumberDto>(jsonPhoneNumber, settings);
                response.PhoneNumber = finalPhoneNumberDto;
                _logger.LogInformation($"Successfully fetched phoneNumber with ID {request.PhoneNumberId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching phoneNumber with ID {request.PhoneNumberId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
