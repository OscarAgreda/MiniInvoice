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

namespace DDDInvoicingClean.Api.InvoiceEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateInvoiceRequest>.WithActionResult<
        CreateInvoiceResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Invoice> _repository;

        public Create(
            IRepository<Invoice> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/invoices")]
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
            var response = new CreateInvoiceResponse(request.CorrelationId());
            var newInvoice = new Invoice(
            invoiceId:Guid.NewGuid(),
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
                var dto = _mapper.Map<InvoiceDto>(newInvoice);
                response.Invoice = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating invoice with Id {newInvoice.InvoiceId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}