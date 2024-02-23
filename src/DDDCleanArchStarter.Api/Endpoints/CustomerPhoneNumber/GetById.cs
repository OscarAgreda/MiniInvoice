using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerPhoneNumberEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCustomerPhoneNumberRequest>.WithActionResult<
        GetByIdCustomerPhoneNumberResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;

        public GetById(
            IRepository<CustomerPhoneNumber> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/customerPhoneNumbers/{RowId}")]
        [SwaggerOperation(
            Summary = "Get a CustomerPhoneNumber by Id",
            Description = "Gets a CustomerPhoneNumber by Id",
            OperationId = "customerPhoneNumbers.GetById",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerPhoneNumberResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerPhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerPhoneNumberResponse(request.CorrelationId());
            try
            {
                var customerPhoneNumber = await _repository.GetByIdAsync(request.RowId, cancellationToken);
                if (customerPhoneNumber is null)
                {
                    var errorMsg = $"CustomerPhoneNumber with ID {request.RowId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerPhoneNumber = JsonConvert.SerializeObject(customerPhoneNumber, settings);
                CustomerPhoneNumberDto finalCustomerPhoneNumberDto = JsonConvert.DeserializeObject<CustomerPhoneNumberDto>(jsonCustomerPhoneNumber, settings);
                response.CustomerPhoneNumber = finalCustomerPhoneNumberDto;
                _logger.LogInformation($"Successfully fetched customerPhoneNumber with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching customerPhoneNumber with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}