using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerAccountEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListCustomerAccountRequest>
      .WithActionResult<ListCustomerAccountResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAccount> _repository;

        public List(IRepository<CustomerAccount> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/customerAccounts")]
        [SwaggerOperation(
            Summary = "List CustomerAccounts",
            Description = "List CustomerAccounts",
            OperationId = "customerAccounts.List",
            Tags = new[] { "CustomerAccountEndpoints" })
        ]
        public override async Task<ActionResult<ListCustomerAccountResponse>> HandleAsync([FromQuery] ListCustomerAccountRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListCustomerAccountResponse(request.CorrelationId());
            try
            {
                var spec = new CustomerAccountGetListSpec();
                var customerAccounts = await _repository.ListAsync(spec, cancellationToken);
                if (customerAccounts == null || !customerAccounts.Any())
                {
                    _logger.LogWarning("No customerAccounts found.");
                    return NotFound();
                }
                response.CustomerAccounts = _mapper.Map<List<CustomerAccountDto>>(customerAccounts);
                response.Count = response.CustomerAccounts.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching customerAccount list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}