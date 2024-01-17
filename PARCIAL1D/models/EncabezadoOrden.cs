using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class EncabezadoOrden
    {
        [Key]
        public int EncabezadoOrdenId { get; set; }
        public int? EmpresaId { get; set; }
        public int? UsuarioId { get; set; }
        public String? TipoOrden { get; set; }
        public int? MesaId { get; set; }
        public String? Cliente { get; set; }
        public String? EstadoOrden { get; set; }
        public String? TipoPago { get; set; }
        public String? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
