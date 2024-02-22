using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.InvoiceDetailEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListInvoiceDetailRequest>
    .WithActionResult<ListInvoiceDetailResponse>
  {
    private readonly IRepository<InvoiceDetail> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<InvoiceDetail> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/invoiceDetails")]
    [SwaggerOperation(
        Summary = "List InvoiceDetails",
        Description = "List InvoiceDetails",
        OperationId = "invoiceDetails.List",
        Tags = new[] { "InvoiceDetailEndpoints" })
    ]
    public override async Task<ActionResult<ListInvoiceDetailResponse>> HandleAsync([FromQuery] ListInvoiceDetailRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListInvoiceDetailResponse(request.CorrelationId());
            try
            {
                var spec = new InvoiceDetailGetListSpec();
                var invoiceDetails = await _repository.ListAsync(spec, cancellationToken);
                if (invoiceDetails == null || !invoiceDetails.Any()) 
                {
                    _logger.LogWarning("No invoiceDetails found.");
                    return NotFound();
                }
                response.InvoiceDetails = _mapper.Map<List<InvoiceDetailDto>>(invoiceDetails);
                response.Count = response.InvoiceDetails.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching invoiceDetail list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
