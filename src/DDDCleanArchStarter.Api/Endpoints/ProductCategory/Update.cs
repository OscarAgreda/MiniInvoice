using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.ProductCategoryEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateProductCategoryRequest>
      .WithActionResult<UpdateProductCategoryResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductCategory> _repository;

        public Update(
            IRepository<ProductCategory> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/productCategories")]
        [SwaggerOperation(
            Summary = "Updates a ProductCategory",
            Description = "Updates a ProductCategory",
            OperationId = "productCategories.update",
            Tags = new[] { "ProductCategoryEndpoints" })
        ]
        public override async Task<ActionResult<UpdateProductCategoryResponse>> HandleAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateProductCategoryResponse(request.CorrelationId());
            try
            {
                var pcrcocToUpdate = _mapper.Map<ProductCategory>(request);
                await _repository.UpdateAsync(pcrcocToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonProductCategory = JsonConvert.SerializeObject(pcrcocToUpdate, settings);
                ProductCategoryDto finalProductCategoryDto = JsonConvert.DeserializeObject<ProductCategoryDto>(jsonProductCategory, settings);
                response.ProductCategory = finalProductCategoryDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating productCategory with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}