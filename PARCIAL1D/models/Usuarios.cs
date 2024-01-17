using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public int? EmpresaId { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }


        public string? Correo { get; set; }
        public string? Telefonos { get; set; }

        public string? Direccion { get; set; }


        public int? PuestoId { get; set; }

        public string? DUI { get; set; }
        public string? NIT { get; set; }
        public DateTime? FechaContratacion { get; set; }


        public DateTime? FechaCreacion { get; set; }
        public char? Estado { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public string? Token { get; set; }
        public string? Clave { get; set; }


    }
}
