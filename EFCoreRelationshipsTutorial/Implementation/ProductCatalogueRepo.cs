using EFCoreRelationshipsTutorial.Interface;
using EFCoreRelationshipsTutorial.Models;

namespace EFCoreRelationshipsTutorial.Implementation
{
    public class ProductCatalogueRepo : GenericRepository<ProductCatalogues>, IProductCatalogueRepo
    {
        public ProductCatalogueRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public override Task<List<ProductCatalogues>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<ProductCatalogues> GetAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<bool> AddEntity(ProductCatalogues entity)
        {
            try
            {
                await DbSet.AddAsync(entity);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override async Task<bool> UpdateEntity(ProductCatalogues entity)
        {
            try
            {
                var existdata = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);
                if (existdata != null)
                {
                    existdata.CatelogueId = entity.CatelogueId;
                    existdata.ProductId = entity.ProductId;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<bool> DeleteEntity(int id)
        {
            var existdata = await DbSet.FirstOrDefaultAsync(item => item.Id == id);
            if (existdata != null)
            {
                DbSet.Remove(existdata);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
