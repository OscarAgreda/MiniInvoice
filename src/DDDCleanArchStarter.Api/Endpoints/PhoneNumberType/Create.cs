using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumberType;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace DDDInvoicingClean.Api.PhoneNumberTypeEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreatePhoneNumberTypeRequest>.WithActionResult<
        CreatePhoneNumberTypeResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumberType> _repository;
        public Create(
            IRepository<PhoneNumberType> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/phoneNumberTypes")]
        [SwaggerOperation(
            Summary = "Creates a new PhoneNumberType",
            Description = "Creates a new PhoneNumberType",
            OperationId = "phoneNumberTypes.create",
            Tags = new[] { "PhoneNumberTypeEndpoints" })
        ]
        public override async Task<ActionResult<CreatePhoneNumberTypeResponse>> HandleAsync(
            CreatePhoneNumberTypeRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreatePhoneNumberTypeResponse(request.CorrelationId());
            var newPhoneNumberType = new PhoneNumberType(
            phoneNumberTypeId:Guid.NewGuid(),
            request.PhoneNumberTypeName,
            request.Description,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newPhoneNumberType, cancellationToken);
                var dto = _mapper.Map<PhoneNumberTypeDto>(newPhoneNumberType);
                response.PhoneNumberType = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating phoneNumberType with Id {newPhoneNumberType.PhoneNumberTypeId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}