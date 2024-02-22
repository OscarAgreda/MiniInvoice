using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AddressEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateAddressRequest>.WithActionResult<
        CreateAddressResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Address> _repository;
        public Create(
            IRepository<Address> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/addresses")]
        [SwaggerOperation(
            Summary = "Creates a new Address",
            Description = "Creates a new Address",
            OperationId = "addresses.create",
            Tags = new[] { "AddressEndpoints" })
        ]
        public override async Task<ActionResult<CreateAddressResponse>> HandleAsync(
            CreateAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateAddressResponse(request.CorrelationId());
            var newAddress = new Address(
            addressId:Guid.NewGuid(),
            request.CityId,
            request.CountryId,
            request.StateId,
            request.AddressStr,
            request.ZipCode,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newAddress, cancellationToken);
                var dto = _mapper.Map<AddressDto>(newAddress);
                response.Address = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating address with Id {newAddress.AddressId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
