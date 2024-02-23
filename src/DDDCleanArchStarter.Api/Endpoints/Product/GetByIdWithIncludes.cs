using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Product;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.ProductEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdProductRequest>.WithActionResult<
        GetByIdProductResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;

        public GetByIdWithIncludes(
            IRepository<Product> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/products/i/{ProductId}")]
        [SwaggerOperation(
            Summary = "Get a Product by Id With Includes",
            Description = "Gets a Product by Id With Includes",
            OperationId = "products.GetByIdWithIncludes",
            Tags = new[] { "ProductEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdProductResponse>> HandleAsync(
            [FromRoute] GetByIdProductRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdProductResponse(request.CorrelationId());
            try
            {
                var spec = new ProductByIdWithIncludesSpec(request.ProductId);
                var product = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching product with ID {request.ProductId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}