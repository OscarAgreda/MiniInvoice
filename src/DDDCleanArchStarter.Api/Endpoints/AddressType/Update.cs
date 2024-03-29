using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AddressType;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.AddressTypeEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateAddressTypeRequest>
      .WithActionResult<UpdateAddressTypeResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<AddressType> _repository;

        public Update(
            IRepository<AddressType> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/addressTypes")]
        [SwaggerOperation(
            Summary = "Updates a AddressType",
            Description = "Updates a AddressType",
            OperationId = "addressTypes.update",
            Tags = new[] { "AddressTypeEndpoints" })
        ]
        public override async Task<ActionResult<UpdateAddressTypeResponse>> HandleAsync(UpdateAddressTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAddressTypeResponse(request.CorrelationId());
            try
            {
                var atdtdtToUpdate = _mapper.Map<AddressType>(request);
                await _repository.UpdateAsync(atdtdtToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAddressType = JsonConvert.SerializeObject(atdtdtToUpdate, settings);
                AddressTypeDto finalAddressTypeDto = JsonConvert.DeserializeObject<AddressTypeDto>(jsonAddressType, settings);
                response.AddressType = finalAddressTypeDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating addressType with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}