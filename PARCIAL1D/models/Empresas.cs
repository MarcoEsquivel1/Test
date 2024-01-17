using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.models
{
    public class Empresas
    {
        [Key]
        public int EmpresaId { get; set; }
        public String NombreEmpresa { get; set; }
        public String Representante { get; set; }
        public String NIT { get; set; }
        public String NRC { get; set; }
        public String Direcccion { get; set; }
        public String Correo { get; set; }
        public String Telefonos { get; set; }
        public String Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
