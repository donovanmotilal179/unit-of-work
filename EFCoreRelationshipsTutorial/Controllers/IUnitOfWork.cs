using EFCoreRelationshipsTutorial.Interface;

namespace EFCoreRelationshipsTutorial.Controllers
{
    public interface IUnitOfWork
    {
        IArmourRepo armourRepo { get; }

        ICatalogueRepo catalogueRepo { get; }

        IProductRepo productRepo { get; }

        IProductCatalogueRepo productCatalogueRepo { get; }

        Task CompleteAsync();
    }
}
