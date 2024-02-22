using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<CreateProductCategoryRequest, ProductCategory>();
            CreateMap<UpdateProductCategoryRequest, ProductCategory>();
            CreateMap<DeleteProductCategoryRequest, ProductCategory>();
        }
    }
}
