using System;
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
    public class Delete : EndpointBaseAsync.WithRequest<DeleteInvoiceRequest>.WithActionResult<
        DeleteInvoiceResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IRepository<Invoice> _invoiceReadRepository;
        private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Invoice> _repository;
        public Delete(IRepository<Invoice> InvoiceRepository, IRepository<Invoice> InvoiceReadRepository,
            IAppLoggerService<Delete> logger,
		       IRepository<InvoiceDetail> invoiceDetailRepository,
            IMapper mapper)
        {
            _repository = InvoiceRepository;
            _logger = logger;
            _invoiceReadRepository = InvoiceReadRepository;
			    _invoiceDetailRepository = invoiceDetailRepository;
            _mapper = mapper;
        }
        [HttpDelete("api/invoices/{InvoiceId}")]
        [SwaggerOperation(
            Summary = "Deletes an Invoice",
            Description = "Deletes an Invoice",
            OperationId = "invoices.delete",
            Tags = new[] { "InvoiceEndpoints" })
        ]
        public override async Task<ActionResult<DeleteInvoiceResponse>> HandleAsync(
            [FromRoute] DeleteInvoiceRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteInvoiceResponse(request.CorrelationId());
            var invoice = await _invoiceReadRepository.GetByIdAsync(request.InvoiceId, cancellationToken);
            if (invoice == null)
            {
                    var errorMsg = $"Invoice with ID {request.InvoiceId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
            }
            var invoiceDetailSpec = new GetInvoiceDetailWithInvoiceKeySpec(invoice.InvoiceId);
            var invoiceDetails = await _invoiceDetailRepository.ListAsync(invoiceDetailSpec);
            await _invoiceDetailRepository.DeleteRangeAsync(invoiceDetails);
            try
            {
                await _repository.DeleteAsync(invoice, cancellationToken);
                _logger.LogInformation($"Successfully deleted invoice with ID {request.InvoiceId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting invoice with ID {request.InvoiceId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
