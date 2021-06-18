using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace E1201710050004.Clases
{
    class UbicacionInfo
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(250)]
        public string latitud { get; set; }
        [MaxLength(250)]
        public string longitud { get; set; }
        public string ubicacion { get; set; }
        [MaxLength(250)]
        public string descripcion_corta { get; set; }
  
    }
}
