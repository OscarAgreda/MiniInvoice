using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.AddressType;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AddressTypeEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdAddressTypeRequest>.WithActionResult<
        GetByIdAddressTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<AddressType> _repository;
        public GetById(
            IRepository<AddressType> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/addressTypes/{AddressTypeId}")]
        [SwaggerOperation(
            Summary = "Get a AddressType by Id",
            Description = "Gets a AddressType by Id",
            OperationId = "addressTypes.GetById",
            Tags = new[] { "AddressTypeEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAddressTypeResponse>> HandleAsync(
            [FromRoute] GetByIdAddressTypeRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAddressTypeResponse(request.CorrelationId());
            try
            {
                var addressType = await _repository.GetByIdAsync(request.AddressTypeId, cancellationToken);
                if (addressType is null)
                {
                    var errorMsg = $"AddressType with ID {request.AddressTypeId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAddressType = JsonConvert.SerializeObject(addressType, settings);
                AddressTypeDto finalAddressTypeDto = JsonConvert.DeserializeObject<AddressTypeDto>(jsonAddressType, settings);
                response.AddressType = finalAddressTypeDto;
                _logger.LogInformation($"Successfully fetched addressType with ID {request.AddressTypeId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching addressType with ID {request.AddressTypeId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
