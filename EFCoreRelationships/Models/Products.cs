﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFCoreRelationships.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

       // public List<Catalogues> Catalogues { get; set; }

    }
}
