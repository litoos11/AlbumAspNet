using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Albums.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public string imagen { get; set; }
        public List<FilePath> FilePaths { get; set; }
    }
}