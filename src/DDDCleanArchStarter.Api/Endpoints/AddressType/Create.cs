using System;
using System.Globalization;
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
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AddressTypeEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateAddressTypeRequest>.WithActionResult<
        CreateAddressTypeResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<AddressType> _repository;

        public Create(
            IRepository<AddressType> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/addressTypes")]
        [SwaggerOperation(
            Summary = "Creates a new AddressType",
            Description = "Creates a new AddressType",
            OperationId = "addressTypes.create",
            Tags = new[] { "AddressTypeEndpoints" })
        ]
        public override async Task<ActionResult<CreateAddressTypeResponse>> HandleAsync(
            CreateAddressTypeRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateAddressTypeResponse(request.CorrelationId());
            var newAddressType = new AddressType(
            addressTypeId:Guid.NewGuid(),
            request.AddressTypeName,
            request.Description,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newAddressType, cancellationToken);
                var dto = _mapper.Map<AddressTypeDto>(newAddressType);
                response.AddressType = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating addressType with Id {newAddressType.AddressTypeId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}