using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.CustomerAddressEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateCustomerAddressRequest>
      .WithActionResult<UpdateCustomerAddressResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAddress> _repository;

        public Update(
            IRepository<CustomerAddress> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/customerAddresses")]
        [SwaggerOperation(
            Summary = "Updates a CustomerAddress",
            Description = "Updates a CustomerAddress",
            OperationId = "customerAddresses.update",
            Tags = new[] { "CustomerAddressEndpoints" })
        ]
        public override async Task<ActionResult<UpdateCustomerAddressResponse>> HandleAsync(UpdateCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateCustomerAddressResponse(request.CorrelationId());
            try
            {
                var cauasaToUpdate = _mapper.Map<CustomerAddress>(request);
                await _repository.UpdateAsync(cauasaToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerAddress = JsonConvert.SerializeObject(cauasaToUpdate, settings);
                CustomerAddressDto finalCustomerAddressDto = JsonConvert.DeserializeObject<CustomerAddressDto>(jsonCustomerAddress, settings);
                response.CustomerAddress = finalCustomerAddressDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating customerAddress with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}