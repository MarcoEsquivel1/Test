using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class Pagos
    {
        [Key]
        public int PagoId { get; set; }
        public int? EmpresaId { get; set; }
        public int? OrdenId { get; set; }
        public int? MovimientoCajaId { get; set; }
        public String? TipoPago { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Propina { get; set; }
        public decimal? Total { get; set; }
        public decimal? MontoPagado { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? CreditoId { get; set; }
        public String? TarjetaNumero { get; set; }
        public String? NombreTarjeta { get; set; }
        public String? autorizacion { get; set; }
        public String? Estado { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
