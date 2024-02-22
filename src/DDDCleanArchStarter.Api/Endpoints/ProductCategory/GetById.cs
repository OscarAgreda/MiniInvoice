using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.ProductCategoryEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdProductCategoryRequest>.WithActionResult<
        GetByIdProductCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<ProductCategory> _repository;
        public GetById(
            IRepository<ProductCategory> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/productCategories/{RowId}")]
        [SwaggerOperation(
            Summary = "Get a ProductCategory by Id",
            Description = "Gets a ProductCategory by Id",
            OperationId = "productCategories.GetById",
            Tags = new[] { "ProductCategoryEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdProductCategoryResponse>> HandleAsync(
            [FromRoute] GetByIdProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdProductCategoryResponse(request.CorrelationId());
            try
            {
                var productCategory = await _repository.GetByIdAsync(request.RowId, cancellationToken);
                if (productCategory is null)
                {
                    var errorMsg = $"ProductCategory with ID {request.RowId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonProductCategory = JsonConvert.SerializeObject(productCategory, settings);
                ProductCategoryDto finalProductCategoryDto = JsonConvert.DeserializeObject<ProductCategoryDto>(jsonProductCategory, settings);
                response.ProductCategory = finalProductCategoryDto;
                _logger.LogInformation($"Successfully fetched productCategory with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching productCategory with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
