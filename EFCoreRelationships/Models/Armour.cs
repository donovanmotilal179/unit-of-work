namespace EFCoreRelationships.Models
{
    public class Armour
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Damage { get; set; }
        public int CatalogueId { get; set; }

  //      public Catalogues Catalogues { get; set; }
    }
}
