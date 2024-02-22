using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Invoice;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.InvoiceEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateInvoiceRequest>
    .WithActionResult<UpdateInvoiceResponse>
  {
    private readonly IRepository<Invoice> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
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
