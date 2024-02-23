using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerPhoneNumberEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListCustomerPhoneNumberRequest>
      .WithActionResult<ListCustomerPhoneNumberResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;

        public List(IRepository<CustomerPhoneNumber> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/customerPhoneNumbers")]
        [SwaggerOperation(
            Summary = "List CustomerPhoneNumbers",
            Description = "List CustomerPhoneNumbers",
            OperationId = "customerPhoneNumbers.List",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<ListCustomerPhoneNumberResponse>> HandleAsync([FromQuery] ListCustomerPhoneNumberRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListCustomerPhoneNumberResponse(request.CorrelationId());
            try
            {
                var spec = new CustomerPhoneNumberGetListSpec();
                var customerPhoneNumbers = await _repository.ListAsync(spec, cancellationToken);
                if (customerPhoneNumbers == null || !customerPhoneNumbers.Any())
                {
                    _logger.LogWarning("No customerPhoneNumbers found.");
                    return NotFound();
                }
                response.CustomerPhoneNumbers = _mapper.Map<List<CustomerPhoneNumberDto>>(customerPhoneNumbers);
                response.Count = response.CustomerPhoneNumbers.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching customerPhoneNumber list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}