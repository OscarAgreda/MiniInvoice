using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceDetailEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteInvoiceDetailRequest>.WithActionResult<
        DeleteInvoiceDetailResponse>
    {
        private readonly IRepository<InvoiceDetail> _invoiceDetailReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<InvoiceDetail> _repository;

        public Delete(IRepository<InvoiceDetail> InvoiceDetailRepository, IRepository<InvoiceDetail> InvoiceDetailReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = InvoiceDetailRepository;
            _logger = logger;
            _invoiceDetailReadRepository = InvoiceDetailReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/invoiceDetails/{InvoiceDetailId}")]
        [SwaggerOperation(
            Summary = "Deletes an InvoiceDetail",
            Description = "Deletes an InvoiceDetail",
            OperationId = "invoiceDetails.delete",
            Tags = new[] { "InvoiceDetailEndpoints" })
        ]
        public override async Task<ActionResult<DeleteInvoiceDetailResponse>> HandleAsync(
            [FromRoute] DeleteInvoiceDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteInvoiceDetailResponse(request.CorrelationId());
            var invoiceDetail = await _invoiceDetailReadRepository.GetByIdAsync(request.InvoiceDetailId, cancellationToken);
            if (invoiceDetail == null)
            {
                var errorMsg = $"InvoiceDetail with ID {request.InvoiceDetailId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(invoiceDetail, cancellationToken);
                _logger.LogInformation($"Successfully deleted invoiceDetail with ID {request.InvoiceDetailId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting invoiceDetail with ID {request.InvoiceDetailId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}