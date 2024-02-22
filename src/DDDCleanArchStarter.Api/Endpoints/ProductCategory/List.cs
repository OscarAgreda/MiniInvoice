using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.ProductCategoryEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListProductCategoryRequest>
    .WithActionResult<ListProductCategoryResponse>
  {
    private readonly IRepository<ProductCategory> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<ProductCategory> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/productCategories")]
    [SwaggerOperation(
        Summary = "List ProductCategories",
        Description = "List ProductCategories",
        OperationId = "productCategories.List",
        Tags = new[] { "ProductCategoryEndpoints" })
    ]
    public override async Task<ActionResult<ListProductCategoryResponse>> HandleAsync([FromQuery] ListProductCategoryRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListProductCategoryResponse(request.CorrelationId());
            try
            {
                var spec = new ProductCategoryGetListSpec();
                var productCategories = await _repository.ListAsync(spec, cancellationToken);
                if (productCategories == null || !productCategories.Any()) 
                {
                    _logger.LogWarning("No productCategories found.");
                    return NotFound();
                }
                response.ProductCategories = _mapper.Map<List<ProductCategoryDto>>(productCategories);
                response.Count = response.ProductCategories.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching productCategory list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
