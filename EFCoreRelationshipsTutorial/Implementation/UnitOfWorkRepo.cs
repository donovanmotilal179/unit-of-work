using EFCoreRelationshipsTutorial.Controllers;
using EFCoreRelationshipsTutorial.Interface;

namespace EFCoreRelationshipsTutorial.Implementation
{
    public class UnitofWorkRepo : IUnitOfWork
    {
        public IArmourRepo armourRepo { get; private set; }
        public ICatalogueRepo catalogueRepo { get; private set; }
        public IProductCatalogueRepo productCatalogueRepo { get; private set; }
        public IProductRepo productRepo { get; private set; }


        private readonly DbContext _dbContext;

        public UnitofWorkRepo(DbContext dbContext)
        {
            this._dbContext = dbContext;
            armourRepo = new ArmourRepo(_dbContext);
            catalogueRepo = new CatalogueRepo(_dbContext);
            productCatalogueRepo = new ProductCatalogueRepo(_dbContext);
            productRepo = new ProductRepo(_dbContext);
        }

        public async Task CompleteAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
