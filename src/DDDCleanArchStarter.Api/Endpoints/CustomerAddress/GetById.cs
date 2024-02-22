using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerAddressEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCustomerAddressRequest>.WithActionResult<
        GetByIdCustomerAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<CustomerAddress> _repository;
        public GetById(
            IRepository<CustomerAddress> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/customerAddresses/{RowId}")]
        [SwaggerOperation(
            Summary = "Get a CustomerAddress by Id",
            Description = "Gets a CustomerAddress by Id",
            OperationId = "customerAddresses.GetById",
            Tags = new[] { "CustomerAddressEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerAddressResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerAddressResponse(request.CorrelationId());
            try
            {
                var customerAddress = await _repository.GetByIdAsync(request.RowId, cancellationToken);
                if (customerAddress is null)
                {
                    var errorMsg = $"CustomerAddress with ID {request.RowId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerAddress = JsonConvert.SerializeObject(customerAddress, settings);
                CustomerAddressDto finalCustomerAddressDto = JsonConvert.DeserializeObject<CustomerAddressDto>(jsonCustomerAddress, settings);
                response.CustomerAddress = finalCustomerAddressDto;
                _logger.LogInformation($"Successfully fetched customerAddress with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching customerAddress with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
