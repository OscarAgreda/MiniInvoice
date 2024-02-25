using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using BlazorMauiShared.Models.Invoice;

using DDDCleanArchStarter.Infrastructure.Services;

using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;

using DDDInvoicingCleanL.SharedKernel.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceEndpoints;

public class CreateV2 : EndpointBaseAsync.WithRequest<CreateInvoiceRequest>.WithActionResult<
    CreateInvoiceResponse>
{
    private readonly IAppLoggerService<Create> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Invoice> _repository;

    public CreateV2(
        IRepository<Invoice> repository,
        IMapper mapper,
        IAppLoggerService<Create> logger
    )
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    [HttpPost("api/invoicesV2")]
    [SwaggerOperation(
        Summary = "Creates a new Invoice",
        Description = "Creates a new Invoice",
        OperationId = "invoices.create",
        Tags = new[] { "InvoiceEndpoints" })
    ]
    public override async Task<ActionResult<CreateInvoiceResponse>> HandleAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken)
    {
        CreateInvoiceResponse response = new(request.CorrelationId());
        Invoice newInvoice = new(
            Guid.NewGuid(),
            request.AccountId,
            request.InvoiceNumber,
            request.AccountName,
            request.CustomerName,
            request.PaymentState,
            request.InternalComments,
            request.InvoicedDate,
            request.InvoicingNote,
            request.TotalSale,
            request.TotalSaleTax,
            request.TenantId
        );
        try
        {
            await _repository.AddAsync(newInvoice, cancellationToken);
            InvoiceDto dto = _mapper.Map<InvoiceDto>(newInvoice);
            response.Invoice = dto;
        }
        catch (Exception ex)
        {
            string errorMsg =
                $"Error while creating invoice with Id {newInvoice.InvoiceId.ToString("D", CultureInfo.InvariantCulture)}";
            _logger.LogError(ex, errorMsg);
            response.ErrorMessage = errorMsg;
            return BadRequest(response);
        }

        return Ok(response);
    }
}