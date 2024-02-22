using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AddressEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListAddressRequest>
    .WithActionResult<ListAddressResponse>
  {
    private readonly IRepository<Address> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<Address> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/addresses")]
    [SwaggerOperation(
        Summary = "List Addresses",
        Description = "List Addresses",
        OperationId = "addresses.List",
        Tags = new[] { "AddressEndpoints" })
    ]
    public override async Task<ActionResult<ListAddressResponse>> HandleAsync([FromQuery] ListAddressRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListAddressResponse(request.CorrelationId());
            try
            {
                var spec = new AddressGetListSpec();
                var addresses = await _repository.ListAsync(spec, cancellationToken);
                if (addresses == null || !addresses.Any()) 
                {
                    _logger.LogWarning("No addresses found.");
                    return NotFound();
                }
                response.Addresses = _mapper.Map<List<AddressDto>>(addresses);
                response.Count = response.Addresses.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching address list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
