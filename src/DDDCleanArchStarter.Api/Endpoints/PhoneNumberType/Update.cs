using System;
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
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.PhoneNumberTypeEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdatePhoneNumberTypeRequest>
      .WithActionResult<UpdatePhoneNumberTypeResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumberType> _repository;

        public Update(
            IRepository<PhoneNumberType> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/phoneNumberTypes")]
        [SwaggerOperation(
            Summary = "Updates a PhoneNumberType",
            Description = "Updates a PhoneNumberType",
            OperationId = "phoneNumberTypes.update",
            Tags = new[] { "PhoneNumberTypeEndpoints" })
        ]
        public override async Task<ActionResult<UpdatePhoneNumberTypeResponse>> HandleAsync(UpdatePhoneNumberTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdatePhoneNumberTypeResponse(request.CorrelationId());
            try
            {
                var pnthntontToUpdate = _mapper.Map<PhoneNumberType>(request);
                await _repository.UpdateAsync(pnthntontToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonPhoneNumberType = JsonConvert.SerializeObject(pnthntontToUpdate, settings);
                PhoneNumberTypeDto finalPhoneNumberTypeDto = JsonConvert.DeserializeObject<PhoneNumberTypeDto>(jsonPhoneNumberType, settings);
                response.PhoneNumberType = finalPhoneNumberTypeDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating phoneNumberType with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}