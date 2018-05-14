using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Albums.Models
{
    public class FilePath
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public FileType TipoArchivo { get; set; }
        //public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}