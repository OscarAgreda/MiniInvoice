using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AddressType;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AddressTypeEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteAddressTypeRequest>.WithActionResult<
        DeleteAddressTypeResponse>
    {
        private readonly IRepository<AddressType> _addressTypeReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<AddressType> _repository;

        public Delete(IRepository<AddressType> AddressTypeRepository, IRepository<AddressType> AddressTypeReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = AddressTypeRepository;
            _logger = logger;
            _addressTypeReadRepository = AddressTypeReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/addressTypes/{AddressTypeId}")]
        [SwaggerOperation(
            Summary = "Deletes an AddressType",
            Description = "Deletes an AddressType",
            OperationId = "addressTypes.delete",
            Tags = new[] { "AddressTypeEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAddressTypeResponse>> HandleAsync(
            [FromRoute] DeleteAddressTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAddressTypeResponse(request.CorrelationId());
            var addressType = await _addressTypeReadRepository.GetByIdAsync(request.AddressTypeId, cancellationToken);
            if (addressType == null)
            {
                var errorMsg = $"AddressType with ID {request.AddressTypeId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(addressType, cancellationToken);
                _logger.LogInformation($"Successfully deleted addressType with ID {request.AddressTypeId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting addressType with ID {request.AddressTypeId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}