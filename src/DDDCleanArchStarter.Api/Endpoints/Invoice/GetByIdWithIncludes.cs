using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Invoice;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdInvoiceRequest>.WithActionResult<
        GetByIdInvoiceResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Invoice> _repository;

        public GetByIdWithIncludes(
            IRepository<Invoice> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/invoices/i/{InvoiceId}")]
        [SwaggerOperation(
            Summary = "Get a Invoice by Id With Includes",
            Description = "Gets a Invoice by Id With Includes",
            OperationId = "invoices.GetByIdWithIncludes",
            Tags = new[] { "InvoiceEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdInvoiceResponse>> HandleAsync(
            [FromRoute] GetByIdInvoiceRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdInvoiceResponse(request.CorrelationId());
            try
            {
                var spec = new InvoiceByIdWithIncludesSpec(request.InvoiceId);
                var invoice = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching invoice with ID {request.InvoiceId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}