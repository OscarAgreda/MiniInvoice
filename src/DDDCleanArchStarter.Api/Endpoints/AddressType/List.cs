using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AddressType;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AddressTypeEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListAddressTypeRequest>
    .WithActionResult<ListAddressTypeResponse>
  {
    private readonly IRepository<AddressType> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<AddressType> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/addressTypes")]
    [SwaggerOperation(
        Summary = "List AddressTypes",
        Description = "List AddressTypes",
        OperationId = "addressTypes.List",
        Tags = new[] { "AddressTypeEndpoints" })
    ]
    public override async Task<ActionResult<ListAddressTypeResponse>> HandleAsync([FromQuery] ListAddressTypeRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListAddressTypeResponse(request.CorrelationId());
            try
            {
                var spec = new AddressTypeGetListSpec();
                var addressTypes = await _repository.ListAsync(spec, cancellationToken);
                if (addressTypes == null || !addressTypes.Any()) 
                {
                    _logger.LogWarning("No addressTypes found.");
                    return NotFound();
                }
                response.AddressTypes = _mapper.Map<List<AddressTypeDto>>(addressTypes);
                response.Count = response.AddressTypes.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching addressType list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
