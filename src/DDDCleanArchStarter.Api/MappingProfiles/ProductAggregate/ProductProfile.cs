using AutoMapper;
using BlazorMauiShared.Models.Product;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
namespace DDDInvoicingClean.Api.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<DeleteProductRequest, Product>();
        }
    }
}
