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

namespace BusinessManagement.Api.InvoiceEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateInvoiceRequest>
      .WithActionResult<UpdateInvoiceResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Invoice> _repository;

        public Update(
            IRepository<Invoice> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/invoices")]
        [SwaggerOperation(
            Summary = "Updates a Invoice",
            Description = "Updates a Invoice",
            OperationId = "invoices.update",
            Tags = new[] { "InvoiceEndpoints" })
        ]
        public override async Task<ActionResult<UpdateInvoiceResponse>> HandleAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateInvoiceResponse(request.CorrelationId());
            try
            {
                var invToUpdate = _mapper.Map<Invoice>(request);
                await _repository.UpdateAsync(invToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonInvoice = JsonConvert.SerializeObject(invToUpdate, settings);
                InvoiceDto finalInvoiceDto = JsonConvert.DeserializeObject<InvoiceDto>(jsonInvoice, settings);
                response.Invoice = finalInvoiceDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating invoice with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}