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
    public class DetalleOrdenesController : ControllerBase
    {
        private readonly ordenContext _contexto;

        public DetalleOrdenesController(ordenContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/detalles_ordenes/")]
        public IActionResult Get()
        {
            try
            {
                var listadoDetalles = (from dO in _contexto.DetalleOrdenes 
                                       join e in _contexto.Empresas on dO.EmpresaId equals e.EmpresaId 
                                       join pl in _contexto.Platos on dO.PlatoId equals pl.PlatoId
                                       select new
                                       {
                                           dO.DetalleOrdenId,
                                           dO.EncabezadoOrdenId,
                                           dO.EmpresaId,
                                           Empresa = e.NombreEmpresa,
                                           dO.PlatoId,
                                           NombrePlato = pl.NombrePlato,
                                           PrecioPlato = pl.Precio,
                                           dO.Cantidad,
                                           dO.Comentarios,
                                           dO.DescuentoEspecial,
                                           dO.RecargoOrden,
                                           dO.Estado,
                                           dO.FechaCreacion,
                                           dO.FechaModificacion
                                       }).OrderBy(dO => dO.EncabezadoOrdenId);
                if (listadoDetalles.Count() > 0)
                {
                    return Ok(listadoDetalles);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/detalles_ordenes/{idDetalle}")]
        public IActionResult Get(int idDetalle)
        {
            try
            {
                var unDetalle = (from dO in _contexto.DetalleOrdenes
                                 join eO in _contexto.EncabezadoOrden on dO.EncabezadoOrdenId equals eO.EncabezadoOrdenId
                                 join e in _contexto.Empresas on dO.EmpresaId equals e.EmpresaId
                                 join pl in _contexto.Platos on dO.PlatoId equals pl.PlatoId
                                 where dO.DetalleOrdenId == idDetalle
                                 select new
                                 {
                                     dO.DetalleOrdenId,
                                     dO.EncabezadoOrdenId,
                                     dO.EmpresaId,
                                     Empresa = e.NombreEmpresa,
                                     dO.PlatoId,
                                     pl.NombrePlato,
                                     PrecioPlato = pl.Precio,
                                     dO.Cantidad,
                                     dO.Comentarios,
                                     dO.DescuentoEspecial,
                                     dO.RecargoOrden,
                                     dO.Estado,
                                     dO.FechaCreacion,
                                     dO.FechaModificacion
                                 }).FirstOrDefault();
                if (unDetalle != null)
                {
                    return Ok(unDetalle);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/detalles_ordenes")]
        public IActionResult guardarDetalle([FromBody] DetalleOrdenes detalleNuevo)
        {
            try
            {
                _contexto.DetalleOrdenes.Add(detalleNuevo);
                _contexto.SaveChanges();
                return Ok(detalleNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/detalles_ordenes")]
        public IActionResult updateDetalle([FromBody] DetalleOrdenes detalleAModificar)
        {
            try
            {
                DetalleOrdenes detalleExiste = (from dO in _contexto.DetalleOrdenes
                                                where dO.DetalleOrdenId == detalleAModificar.DetalleOrdenId
                                                select dO).FirstOrDefault();

                if (detalleExiste is null)
                {
                    return NotFound();
                }

                detalleExiste.Cantidad = detalleAModificar.Cantidad;
                detalleExiste.Comentarios = detalleAModificar.Comentarios;
                detalleExiste.Estado = detalleAModificar.Estado;
                detalleExiste.FechaModificacion = detalleAModificar.FechaModificacion;
                

                _contexto.Entry(detalleExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(detalleExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
