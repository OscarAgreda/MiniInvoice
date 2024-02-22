using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.PhoneNumberTypeEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListPhoneNumberTypeRequest>
    .WithActionResult<ListPhoneNumberTypeResponse>
  {
    private readonly IRepository<PhoneNumberType> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<PhoneNumberType> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/phoneNumberTypes")]
    [SwaggerOperation(
        Summary = "List PhoneNumberTypes",
        Description = "List PhoneNumberTypes",
        OperationId = "phoneNumberTypes.List",
        Tags = new[] { "PhoneNumberTypeEndpoints" })
    ]
    public override async Task<ActionResult<ListPhoneNumberTypeResponse>> HandleAsync([FromQuery] ListPhoneNumberTypeRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListPhoneNumberTypeResponse(request.CorrelationId());
            try
            {
                var spec = new PhoneNumberTypeGetListSpec();
                var phoneNumberTypes = await _repository.ListAsync(spec, cancellationToken);
                if (phoneNumberTypes == null || !phoneNumberTypes.Any()) 
                {
                    _logger.LogWarning("No phoneNumberTypes found.");
                    return NotFound();
                }
                response.PhoneNumberTypes = _mapper.Map<List<PhoneNumberTypeDto>>(phoneNumberTypes);
                response.Count = response.PhoneNumberTypes.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching phoneNumberType list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
