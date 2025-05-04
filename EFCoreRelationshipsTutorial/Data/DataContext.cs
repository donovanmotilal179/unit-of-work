using Microsoft.EntityFrameworkCore;
using EFCoreRelationshipsTutorial.Models;

namespace EFCoreRelationshipsTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DbSet<spGetProductCatalogues> GetProductCatalogues_Results { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductCatalogues> ProductCatalogues { get; set; }
        public virtual DbSet<Catalogues> Catalogues { get; set; }
        public virtual DbSet<Armour> Armours { get; set;}

      
    }
}
