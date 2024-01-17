using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class DetalleOrdenes
    {
        [Key]
        public int? DetalleOrdenId { get; set; }
        public int? EncabezadoOrdenId { get; set; }
        public int? EmpresaId { get; set; }
        public int? PlatoId { get; set; }
        public int? Cantidad { get; set; }
        public String? Comentarios { get; set; }
        public decimal? DescuentoEspecial { get; set; }
        public decimal? RecargoOrden { get; set; }
        public String? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
