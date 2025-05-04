using EFCoreRelationships.Interface;
using EFCoreRelationships.Models;

namespace EFCoreRelationships.Implementation
{
    public class ArmourRepo : GenericRepository<Armour>, IArmourRepo
    {
        public ArmourRepo(DbContext dbContext) : base(dbContext)
        { 
        
        }

        public override Task<List<Armour>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<Armour> GetAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<bool> AddEntity(Armour entity)
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
        public override async Task<bool> UpdateEntity(Armour entity)
        {
            try
            {
                var existdata = await DbSet.FirstOrDefaultAsync(item => item.Id == entity.Id);
                if (existdata != null)
                {
                    existdata.Name = entity.Name;
                    existdata.Damage = entity.Damage;
                    existdata.CatalogueId = entity.CatalogueId;
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
