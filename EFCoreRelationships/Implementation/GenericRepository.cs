using EFCoreRelationships.Interface;

namespace EFCoreRelationships.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _dbContext;
        internal DbSet<T> DbSet { get; set; }
        public GenericRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.DbSet = this._dbContext.Set<T>();
        }
        public virtual Task<bool> AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return this.DbSet.ToListAsync();
           // throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateEntity(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
