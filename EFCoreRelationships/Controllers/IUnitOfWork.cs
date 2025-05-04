using EFCoreRelationships.Interface;

namespace EFCoreRelationships.Controllers
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
