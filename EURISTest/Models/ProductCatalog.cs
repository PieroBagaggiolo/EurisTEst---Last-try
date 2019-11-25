using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    [Table("ProductCatalog")]
    public class ProductCatalog
    {
        [Key]
        public int Id { get; set; }
        public int FKCatalogId { get; set; }
        [ForeignKey("FKCatalogId")]
        public CatalogModel Catalog { get; set; }
        public int FKProductId { get; set; }
        [ForeignKey("FKProductId")]
        public ProductModel Product { get; set; }
    }
}