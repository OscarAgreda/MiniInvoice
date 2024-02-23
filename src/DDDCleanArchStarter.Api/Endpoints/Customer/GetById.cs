using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Customer;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCustomerRequest>.WithActionResult<
        GetByIdCustomerResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _repository;

        public GetById(
            IRepository<Customer> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/customers/{CustomerId}")]
        [SwaggerOperation(
            Summary = "Get a Customer by Id",
            Description = "Gets a Customer by Id",
            OperationId = "customers.GetById",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerResponse(request.CorrelationId());
            try
            {
                var customer = await _repository.GetByIdAsync(request.CustomerId, cancellationToken);
                if (customer is null)
                {
                    var errorMsg = $"Customer with ID {request.CustomerId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomer = JsonConvert.SerializeObject(customer, settings);
                CustomerDto finalCustomerDto = JsonConvert.DeserializeObject<CustomerDto>(jsonCustomer, settings);
                response.Customer = finalCustomerDto;
                _logger.LogInformation($"Successfully fetched customer with ID {request.CustomerId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching customer with ID {request.CustomerId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}