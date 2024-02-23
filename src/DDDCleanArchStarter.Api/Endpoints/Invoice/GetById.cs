using System;
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
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdInvoiceRequest>.WithActionResult<
        GetByIdInvoiceResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Invoice> _repository;

        public GetById(
            IRepository<Invoice> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/invoices/{InvoiceId}")]
        [SwaggerOperation(
            Summary = "Get a Invoice by Id",
            Description = "Gets a Invoice by Id",
            OperationId = "invoices.GetById",
            Tags = new[] { "InvoiceEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdInvoiceResponse>> HandleAsync(
            [FromRoute] GetByIdInvoiceRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdInvoiceResponse(request.CorrelationId());
            try
            {
                var invoice = await _repository.GetByIdAsync(request.InvoiceId, cancellationToken);
                if (invoice is null)
                {
                    var errorMsg = $"Invoice with ID {request.InvoiceId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonInvoice = JsonConvert.SerializeObject(invoice, settings);
                InvoiceDto finalInvoiceDto = JsonConvert.DeserializeObject<InvoiceDto>(jsonInvoice, settings);
                response.Invoice = finalInvoiceDto;
                _logger.LogInformation($"Successfully fetched invoice with ID {request.InvoiceId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching invoice with ID {request.InvoiceId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}