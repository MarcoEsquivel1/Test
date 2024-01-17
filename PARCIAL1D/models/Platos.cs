using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class Platos
    {
        [Key]
        public int PlatoId { get; set; }
        public int? EmpresaId { get; set; }

        public string? NombrePlato { get; set; }

        public string? DescripcionPlato { get; set; }

        public decimal? Precio { get; set; }

        public string? TiempoPreparacion { get; set; }

        public char? AplicaPropina { get; set; }


        public char? Estado { get; set; }
        public char? Lunes { get; set; }
        public char? Martes { get; set; }
        public char? Miercoles { get; set; }
        public char? Jueves { get; set; }
        public char? Viernes { get; set; }
        public char? Sabado { get; set; }
        public char? Domingo { get; set; }


        public DateTime? FechaCreacion { get; set; }


        public DateTime? FechaModificacion { get; set; }

    }
}
