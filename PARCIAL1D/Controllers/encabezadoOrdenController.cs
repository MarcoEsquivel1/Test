using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1D.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PARCIAL1D.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class encabezadoOrdenController : ControllerBase
    {

        private readonly ordenContext contexto;
        public encabezadoOrdenController(ordenContext mi)
        {
            contexto = mi;
        }

        [HttpGet]
        [Route("api/encabezadoOrden")]
        public IActionResult Get()
        {
            var lista = (from m in contexto.EncabezadoOrden
                         join c in contexto.Empresas on m.EmpresaId equals c.EmpresaId
                         join u in contexto.Usuarios on m.UsuarioId equals u.UsuarioId
                         join mesa in contexto.Mesas on m.MesaId equals mesa.MesaId
                         select new { m.EncabezadoOrdenId,m.EmpresaId,c.NombreEmpresa, 
                         m.UsuarioId,u.Nombres,m.TipoOrden,m.FechaCreacion,m.MesaId,mesa.DescripcionMesa,
                         m.Cliente,m.EstadoOrden,m.TipoPago,m.Estado,m.FechaModificacion
                         });
            if (lista.Count() > 0)
            {
                return Ok(lista);

            }
            return NotFound();
        }


        [HttpGet]
        [Route("api/encabezadoOrden/{id}")]
        public IActionResult Get(int id)
        {
            var nuevo = (from m in contexto.EncabezadoOrden
                     join c in contexto.Empresas on m.EmpresaId equals c.EmpresaId
                     join u in contexto.Usuarios on m.UsuarioId equals u.UsuarioId
                     join mesa in contexto.Mesas on m.MesaId equals mesa.MesaId
                     where m.EncabezadoOrdenId == id
                     select new
                     {
                         m.EncabezadoOrdenId,
                         m.EmpresaId,
                         c.NombreEmpresa,
                         m.UsuarioId,
                         u.Nombres,
                         m.TipoOrden,
                         m.FechaCreacion,
                         m.MesaId,
                         mesa.DescripcionMesa,
                         m.Cliente,
                         m.EstadoOrden,
                         m.TipoPago,
                         m.Estado,
                         m.FechaModificacion
                     }).FirstOrDefault();

            if (nuevo != null)
            {
                return Ok(nuevo);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/encabezadoOrden")]
        public IActionResult guardar([FromBody] EncabezadoOrden nuevo)
        {

            try
            {
                contexto.EncabezadoOrden.Add(nuevo);
                contexto.SaveChanges();
                return Ok(nuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("api/encabezadoOrden")]
        public IActionResult actualizar([FromBody] EncabezadoOrden nuevo)
        {
            EncabezadoOrden existe = (from e in contexto.EncabezadoOrden where e.EncabezadoOrdenId == nuevo.EncabezadoOrdenId select e).FirstOrDefault();
            if (existe is null)
            {
                return NotFound();

            }

            existe.FechaModificacion = nuevo.FechaModificacion;
            existe.MesaId = nuevo.MesaId;
            existe.EstadoOrden = nuevo.EstadoOrden;
            existe.Estado = nuevo.Estado;
            contexto.Entry(existe).State = EntityState.Modified;
            contexto.SaveChanges();
            return Ok(existe);
        }
    }
}
