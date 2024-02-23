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

namespace BusinessManagement.Api.InvoiceDetailEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateInvoiceDetailRequest>
      .WithActionResult<UpdateInvoiceDetailResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<InvoiceDetail> _repository;

        public Update(
            IRepository<InvoiceDetail> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/invoiceDetails")]
        [SwaggerOperation(
            Summary = "Updates a InvoiceDetail",
            Description = "Updates a InvoiceDetail",
            OperationId = "invoiceDetails.update",
            Tags = new[] { "InvoiceDetailEndpoints" })
        ]
        public override async Task<ActionResult<UpdateInvoiceDetailResponse>> HandleAsync(UpdateInvoiceDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateInvoiceDetailResponse(request.CorrelationId());
            try
            {
                var idndvdToUpdate = _mapper.Map<InvoiceDetail>(request);
                await _repository.UpdateAsync(idndvdToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonInvoiceDetail = JsonConvert.SerializeObject(idndvdToUpdate, settings);
                InvoiceDetailDto finalInvoiceDetailDto = JsonConvert.DeserializeObject<InvoiceDetailDto>(jsonInvoiceDetail, settings);
                response.InvoiceDetail = finalInvoiceDetailDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating invoiceDetail with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}