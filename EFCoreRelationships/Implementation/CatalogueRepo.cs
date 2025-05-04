using EFCoreRelationships.Interface;
using EFCoreRelationships.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Implementation
{
    public class CatalogueRepo : GenericRepository<Catalogues>, ICatalogueRepo
    {

        public CatalogueRepo(DbContext dbContext) : base(dbContext)
        {

        }

        public override Task<List<Catalogues>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<Catalogues> GetAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<bool> AddEntity(Catalogues entity)
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
        public override async Task<bool> UpdateEntity(Catalogues entity)
        {
            try
            {
                var existdata = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);
                if (existdata != null)
                {
                    existdata.Name = entity.Name;
                    existdata.UserId = entity.UserId;
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
