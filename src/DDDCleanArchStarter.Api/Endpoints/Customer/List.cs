using System;
using System.Collections.Generic;
using System.Linq;
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
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListCustomerRequest>
      .WithActionResult<ListCustomerResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _repository;

        public List(IRepository<Customer> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/customers")]
        [SwaggerOperation(
            Summary = "List Customers",
            Description = "List Customers",
            OperationId = "customers.List",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<ListCustomerResponse>> HandleAsync([FromQuery] ListCustomerRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListCustomerResponse(request.CorrelationId());
            try
            {
                var spec = new CustomerGetListSpec();
                var customers = await _repository.ListAsync(spec, cancellationToken);
                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No customers found.");
                    return NotFound();
                }
                response.Customers = _mapper.Map<List<CustomerDto>>(customers);
                response.Count = response.Customers.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching customer list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}