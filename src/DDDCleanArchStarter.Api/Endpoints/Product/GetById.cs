using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Product;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.ProductEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdProductRequest>.WithActionResult<
        GetByIdProductResponse>
    {
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;

        public GetById(
            IRepository<Product> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/products/{ProductId}")]
        [SwaggerOperation(
            Summary = "Get a Product by Id",
            Description = "Gets a Product by Id",
            OperationId = "products.GetById",
            Tags = new[] { "ProductEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdProductResponse>> HandleAsync(
            [FromRoute] GetByIdProductRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdProductResponse(request.CorrelationId());
            try
            {
                var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
                if (product is null)
                {
                    var errorMsg = $"Product with ID {request.ProductId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonProduct = JsonConvert.SerializeObject(product, settings);
                ProductDto finalProductDto = JsonConvert.DeserializeObject<ProductDto>(jsonProduct, settings);
                response.Product = finalProductDto;
                _logger.LogInformation($"Successfully fetched product with ID {request.ProductId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching product with ID {request.ProductId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}