using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.ProductEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateProductRequest>.WithActionResult<
        CreateProductResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;
        public Create(
            IRepository<Product> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/products")]
        [SwaggerOperation(
            Summary = "Creates a new Product",
            Description = "Creates a new Product",
            OperationId = "products.create",
            Tags = new[] { "ProductEndpoints" })
        ]
        public override async Task<ActionResult<CreateProductResponse>> HandleAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateProductResponse(request.CorrelationId());
            var newProduct = new Product(
            productId:Guid.NewGuid(),
            request.ProductName,
            request.ProductDescription,
            request.ProductUnitPrice,
            request.ProductIsActive,
            request.ProductMinimumCharacters,
            request.ProductMinimumCallMinutes,
            request.ProductChargeRatePerCharacter,
            request.ProductChargeRateCallPerSecond,
            request.IsDeleted,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newProduct, cancellationToken);
                var dto = _mapper.Map<ProductDto>(newProduct);
                response.Product = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating product with Id {newProduct.ProductId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
