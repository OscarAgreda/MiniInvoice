using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Customer;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdCustomerRequest>.WithActionResult<
        GetByIdCustomerResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _repository;

        public GetByIdWithIncludes(
            IRepository<Customer> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/customers/i/{CustomerId}")]
        [SwaggerOperation(
            Summary = "Get a Customer by Id With Includes",
            Description = "Gets a Customer by Id With Includes",
            OperationId = "customers.GetByIdWithIncludes",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerResponse(request.CorrelationId());
            try
            {
                var spec = new CustomerByIdWithIncludesSpec(request.CustomerId);
                var customer = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching customer with ID {request.CustomerId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}