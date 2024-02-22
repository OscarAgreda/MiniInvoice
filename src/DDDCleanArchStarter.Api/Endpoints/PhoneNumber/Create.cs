using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.PhoneNumberEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreatePhoneNumberRequest>.WithActionResult<
        CreatePhoneNumberResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumber> _repository;
        public Create(
            IRepository<PhoneNumber> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/phoneNumbers")]
        [SwaggerOperation(
            Summary = "Creates a new PhoneNumber",
            Description = "Creates a new PhoneNumber",
            OperationId = "phoneNumbers.create",
            Tags = new[] { "PhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<CreatePhoneNumberResponse>> HandleAsync(
            CreatePhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreatePhoneNumberResponse(request.CorrelationId());
            var newPhoneNumber = new PhoneNumber(
            phoneNumberId:Guid.NewGuid(),
            request.PhoneNumberString
            );
            try
            {
                await _repository.AddAsync(newPhoneNumber, cancellationToken);
                var dto = _mapper.Map<PhoneNumberDto>(newPhoneNumber);
                response.PhoneNumber = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating phoneNumber with Id {newPhoneNumber.PhoneNumberId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
