using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AccountEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListAccountRequest>
      .WithActionResult<ListAccountResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Account> _repository;

        public List(IRepository<Account> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/accounts")]
        [SwaggerOperation(
            Summary = "List Accounts",
            Description = "List Accounts",
            OperationId = "accounts.List",
            Tags = new[] { "AccountEndpoints" })
        ]
        public override async Task<ActionResult<ListAccountResponse>> HandleAsync([FromQuery] ListAccountRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListAccountResponse(request.CorrelationId());
            try
            {
                var spec = new AccountGetListSpec();
                var accounts = await _repository.ListAsync(spec, cancellationToken);
                if (accounts == null || !accounts.Any())
                {
                    _logger.LogWarning("No accounts found.");
                    return NotFound();
                }
                response.Accounts = _mapper.Map<List<AccountDto>>(accounts);
                response.Count = response.Accounts.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching account list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}