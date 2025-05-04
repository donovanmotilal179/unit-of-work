using AutoMapper;
using EFCoreRelationships.Models;
using Microsoft.Extensions.Logging;

namespace EFCoreRelationships.Mapper
{
    public class AddMapperProfile : Profile
    {
        public AddMapperProfile()
        {
            CreateMap<ArmourDto, Armour>();
            CreateMap<ProductDto, Products>();
            CreateMap<CatalogueDto, Catalogues>();
            CreateMap<ProductCatalogueDto, ProductCatalogues>();
        }        
    }
}
