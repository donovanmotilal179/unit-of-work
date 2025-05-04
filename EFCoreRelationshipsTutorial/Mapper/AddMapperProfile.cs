using AutoMapper;
using EFCoreRelationshipsTutorial.Models;
using Microsoft.Extensions.Logging;

namespace EFCoreRelationshipsTutorial.Mapper
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
