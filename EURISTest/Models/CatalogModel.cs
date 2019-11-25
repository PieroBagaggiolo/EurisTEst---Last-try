using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    [Table("Catalogo")]
    public class CatalogModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<ProductCatalog> ProductsCatalogs { get; set; }
    }
}