using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Invoice;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.InvoiceEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListInvoiceRequest>
    .WithActionResult<ListInvoiceResponse>
  {
    private readonly IRepository<Invoice> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<Invoice> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/invoices")]
    [SwaggerOperation(
        Summary = "List Invoices",
        Description = "List Invoices",
        OperationId = "invoices.List",
        Tags = new[] { "InvoiceEndpoints" })
    ]
    public override async Task<ActionResult<ListInvoiceResponse>> HandleAsync([FromQuery] ListInvoiceRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListInvoiceResponse(request.CorrelationId());
            try
            {
                var spec = new InvoiceGetListSpec();
                var invoices = await _repository.ListAsync(spec, cancellationToken);
                if (invoices == null || !invoices.Any()) 
                {
                    _logger.LogWarning("No invoices found.");
                    return NotFound();
                }
                response.Invoices = _mapper.Map<List<InvoiceDto>>(invoices);
                response.Count = response.Invoices.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching invoice list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
