using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.InvoiceDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.InvoiceDetailEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateInvoiceDetailRequest>.WithActionResult<
        CreateInvoiceDetailResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<InvoiceDetail> _repository;
        public Create(
            IRepository<InvoiceDetail> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/invoiceDetails")]
        [SwaggerOperation(
            Summary = "Creates a new InvoiceDetail",
            Description = "Creates a new InvoiceDetail",
            OperationId = "invoiceDetails.create",
            Tags = new[] { "InvoiceDetailEndpoints" })
        ]
        public override async Task<ActionResult<CreateInvoiceDetailResponse>> HandleAsync(
            CreateInvoiceDetailRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateInvoiceDetailResponse(request.CorrelationId());
            var newInvoiceDetail = new InvoiceDetail(
            invoiceDetailId:Guid.NewGuid(),
            request.InvoiceId,
            request.ProductId,
            request.TenantId,
            request.Quantity,
            request.ProductName,
            request.UnitPrice,
            request.LineSale,
            request.LineTax,
            request.LineDiscount
            );
            try
            {
                await _repository.AddAsync(newInvoiceDetail, cancellationToken);
                var dto = _mapper.Map<InvoiceDetailDto>(newInvoiceDetail);
                response.InvoiceDetail = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating invoiceDetail with Id {newInvoiceDetail.InvoiceDetailId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
