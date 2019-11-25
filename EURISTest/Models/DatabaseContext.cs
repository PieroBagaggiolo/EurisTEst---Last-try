using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<CatalogModel> Catalogs { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public DataBaseContext() : base("DefaultConnection") { }

        public DbSet<ProductCatalog> ProductCatalogs { get; set; }
    }
}