﻿namespace EFCoreRelationships
{
    public class ProductCatalogueDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = 1;
        public int CatalogueId { get; set; } = 1;
    }
}
