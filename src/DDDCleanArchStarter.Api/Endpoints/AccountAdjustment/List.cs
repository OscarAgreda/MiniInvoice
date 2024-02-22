using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AccountAdjustmentEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListAccountAdjustmentRequest>
    .WithActionResult<ListAccountAdjustmentResponse>
  {
    private readonly IRepository<AccountAdjustment> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<AccountAdjustment> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/accountAdjustments")]
    [SwaggerOperation(
        Summary = "List AccountAdjustments",
        Description = "List AccountAdjustments",
        OperationId = "accountAdjustments.List",
        Tags = new[] { "AccountAdjustmentEndpoints" })
    ]
    public override async Task<ActionResult<ListAccountAdjustmentResponse>> HandleAsync([FromQuery] ListAccountAdjustmentRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListAccountAdjustmentResponse(request.CorrelationId());
            try
            {
                var spec = new AccountAdjustmentGetListSpec();
                var accountAdjustments = await _repository.ListAsync(spec, cancellationToken);
                if (accountAdjustments == null || !accountAdjustments.Any()) 
                {
                    _logger.LogWarning("No accountAdjustments found.");
                    return NotFound();
                }
                response.AccountAdjustments = _mapper.Map<List<AccountAdjustmentDto>>(accountAdjustments);
                response.Count = response.AccountAdjustments.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching accountAdjustment list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
