using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Address;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AddressEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdAddressRequest>.WithActionResult<
        GetByIdAddressResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Address> _repository;

        public GetById(
            IRepository<Address> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/addresses/{AddressId}")]
        [SwaggerOperation(
            Summary = "Get a Address by Id",
            Description = "Gets a Address by Id",
            OperationId = "addresses.GetById",
            Tags = new[] { "AddressEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAddressResponse>> HandleAsync(
            [FromRoute] GetByIdAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAddressResponse(request.CorrelationId());
            try
            {
                var address = await _repository.GetByIdAsync(request.AddressId, cancellationToken);
                if (address is null)
                {
                    var errorMsg = $"Address with ID {request.AddressId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAddress = JsonConvert.SerializeObject(address, settings);
                AddressDto finalAddressDto = JsonConvert.DeserializeObject<AddressDto>(jsonAddress, settings);
                response.Address = finalAddressDto;
                _logger.LogInformation($"Successfully fetched address with ID {request.AddressId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching address with ID {request.AddressId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}