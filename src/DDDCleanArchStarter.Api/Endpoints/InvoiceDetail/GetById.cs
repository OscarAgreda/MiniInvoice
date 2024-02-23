using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceDetailEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdInvoiceDetailRequest>.WithActionResult<
        GetByIdInvoiceDetailResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<InvoiceDetail> _repository;

        public GetById(
            IRepository<InvoiceDetail> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/invoiceDetails/{InvoiceDetailId}")]
        [SwaggerOperation(
            Summary = "Get a InvoiceDetail by Id",
            Description = "Gets a InvoiceDetail by Id",
            OperationId = "invoiceDetails.GetById",
            Tags = new[] { "InvoiceDetailEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdInvoiceDetailResponse>> HandleAsync(
            [FromRoute] GetByIdInvoiceDetailRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdInvoiceDetailResponse(request.CorrelationId());
            try
            {
                var invoiceDetail = await _repository.GetByIdAsync(request.InvoiceDetailId, cancellationToken);
                if (invoiceDetail is null)
                {
                    var errorMsg = $"InvoiceDetail with ID {request.InvoiceDetailId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonInvoiceDetail = JsonConvert.SerializeObject(invoiceDetail, settings);
                InvoiceDetailDto finalInvoiceDetailDto = JsonConvert.DeserializeObject<InvoiceDetailDto>(jsonInvoiceDetail, settings);
                response.InvoiceDetail = finalInvoiceDetailDto;
                _logger.LogInformation($"Successfully fetched invoiceDetail with ID {request.InvoiceDetailId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching invoiceDetail with ID {request.InvoiceDetailId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}