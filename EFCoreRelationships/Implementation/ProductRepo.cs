using EFCoreRelationships.Interface;
using EFCoreRelationships.Models;

namespace EFCoreRelationships.Implementation
{
    public class ProductRepo : GenericRepository<Products>, IProductRepo
    {
        public ProductRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public override Task<List<Products>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<Products> GetAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<bool> AddEntity(Products entity)
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
        public override async Task<bool> UpdateEntity(Products entity)
        {
            try
            {
                var existdata = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);
                if (existdata != null)
                {
                    existdata.Name = entity.Name;
                    existdata.Description = entity.Description;
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