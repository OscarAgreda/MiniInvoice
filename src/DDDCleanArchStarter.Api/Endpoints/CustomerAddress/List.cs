using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerAddressEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListCustomerAddressRequest>
    .WithActionResult<ListCustomerAddressResponse>
  {
    private readonly IRepository<CustomerAddress> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<CustomerAddress> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/customerAddresses")]
    [SwaggerOperation(
        Summary = "List CustomerAddresses",
        Description = "List CustomerAddresses",
        OperationId = "customerAddresses.List",
        Tags = new[] { "CustomerAddressEndpoints" })
    ]
    public override async Task<ActionResult<ListCustomerAddressResponse>> HandleAsync([FromQuery] ListCustomerAddressRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListCustomerAddressResponse(request.CorrelationId());
            try
            {
                var spec = new CustomerAddressGetListSpec();
                var customerAddresses = await _repository.ListAsync(spec, cancellationToken);
                if (customerAddresses == null || !customerAddresses.Any()) 
                {
                    _logger.LogWarning("No customerAddresses found.");
                    return NotFound();
                }
                response.CustomerAddresses = _mapper.Map<List<CustomerAddressDto>>(customerAddresses);
                response.Count = response.CustomerAddresses.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching customerAddress list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
