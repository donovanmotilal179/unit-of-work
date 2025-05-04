namespace EFCoreRelationships.Models
{
    public class spGetProductCatalogues
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get;set; }
        public int CatalogueId { get;set; }
        public string? CatalogueName { get; set; }
        public string? CatalogueType { get; set; }
        public int UserId { get;set; }
        public string? UserName { get; set; }

    }
}
