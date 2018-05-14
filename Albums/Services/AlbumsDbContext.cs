using Albums.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
//https://www.mikesdotnetting.com/article/259/asp-net-mvc-5-with-ef-6-working-with-files

namespace Albums.Services
{
    public class AlbumsDbContext: DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<FilePath>().HasRequired(x => x.Categoria);
            //modelBuilder.Entity<Categoria>().HasRequired(x => x.FilePath);

            base.OnModelCreating(modelBuilder);
        }
    }
}