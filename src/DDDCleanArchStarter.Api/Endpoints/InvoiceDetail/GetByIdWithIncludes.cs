using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.InvoiceDetailEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdInvoiceDetailRequest>.WithActionResult<
        GetByIdInvoiceDetailResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<InvoiceDetail> _repository;

        public GetByIdWithIncludes(
            IRepository<InvoiceDetail> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/invoiceDetails/i/{InvoiceDetailId}")]
        [SwaggerOperation(
            Summary = "Get a InvoiceDetail by Id With Includes",
            Description = "Gets a InvoiceDetail by Id With Includes",
            OperationId = "invoiceDetails.GetByIdWithIncludes",
            Tags = new[] { "InvoiceDetailEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdInvoiceDetailResponse>> HandleAsync(
            [FromRoute] GetByIdInvoiceDetailRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdInvoiceDetailResponse(request.CorrelationId());
            try
            {
                var spec = new InvoiceDetailByIdWithIncludesSpec(request.InvoiceDetailId);
                var invoiceDetail = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching invoiceDetail with ID {request.InvoiceDetailId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}