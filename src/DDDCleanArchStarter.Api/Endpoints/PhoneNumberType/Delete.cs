using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.PhoneNumberTypeEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeletePhoneNumberTypeRequest>.WithActionResult<
        DeletePhoneNumberTypeResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumberType> _phoneNumberTypeReadRepository;
        private readonly IRepository<PhoneNumberType> _repository;

        public Delete(IRepository<PhoneNumberType> PhoneNumberTypeRepository, IRepository<PhoneNumberType> PhoneNumberTypeReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = PhoneNumberTypeRepository;
            _logger = logger;
            _phoneNumberTypeReadRepository = PhoneNumberTypeReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/phoneNumberTypes/{PhoneNumberTypeId}")]
        [SwaggerOperation(
            Summary = "Deletes an PhoneNumberType",
            Description = "Deletes an PhoneNumberType",
            OperationId = "phoneNumberTypes.delete",
            Tags = new[] { "PhoneNumberTypeEndpoints" })
        ]
        public override async Task<ActionResult<DeletePhoneNumberTypeResponse>> HandleAsync(
            [FromRoute] DeletePhoneNumberTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new DeletePhoneNumberTypeResponse(request.CorrelationId());
            var phoneNumberType = await _phoneNumberTypeReadRepository.GetByIdAsync(request.PhoneNumberTypeId, cancellationToken);
            if (phoneNumberType == null)
            {
                var errorMsg = $"PhoneNumberType with ID {request.PhoneNumberTypeId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(phoneNumberType, cancellationToken);
                _logger.LogInformation($"Successfully deleted phoneNumberType with ID {request.PhoneNumberTypeId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting phoneNumberType with ID {request.PhoneNumberTypeId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}