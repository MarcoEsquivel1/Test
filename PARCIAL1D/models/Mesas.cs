using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class Mesas
    {
        [Key]
        public int MesaId { get; set; }
        public int? EmpresaId { get; set; }
        public String? DescripcionMesa { get; set; }
        public String? ZonaMesa { get; set; }
        public int? SillasMesa { get; set; }
        public String? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
