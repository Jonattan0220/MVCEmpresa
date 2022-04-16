using System;
using System.Collections.Generic;

namespace MVCEmpresa.Models
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            CatalogoInventarios = new HashSet<CatalogoInventario>();
        }

        public int IdUbicacion { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<CatalogoInventario> CatalogoInventarios { get; set; }
    }
}
