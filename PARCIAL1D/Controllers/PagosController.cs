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
    public class PagosController : ControllerBase
    {
        private readonly ordenContext _contexto;

        public PagosController(ordenContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/pagos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoPagos = (from p in _contexto.Pagos
                                    join e in _contexto.Empresas on p.EmpresaId equals e.EmpresaId
                                    join o in _contexto.EncabezadoOrden on p.OrdenId equals o.EncabezadoOrdenId
                                    join u in _contexto.Usuarios on p.UsuarioId equals u.UsuarioId
                                    select new
                                    {
                                        p.PagoId,
                                        e.EmpresaId,
                                        e.NombreEmpresa,
                                        p.OrdenId,
                                        p.MovimientoCajaId,
                                        p.TipoPago,
                                        p.SubTotal,
                                        p.Propina,
                                        p.Total,
                                        p.MontoPagado,
                                        p.UsuarioId,
                                        Usuario = u.Nombres + " " + u.Apellidos,
                                        p.FechaCreacion,
                                        p.CreditoId,
                                        p.TarjetaNumero,
                                        p.NombreTarjeta,
                                        p.autorizacion,
                                        p.Estado,
                                        p.FechaModificacion
                                    }).OrderBy(p => p.PagoId);
                if (listadoPagos.Count() > 0)
                {
                    return Ok(listadoPagos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/pagos/{idPago}")]
        public IActionResult Get(int idPago)
        {
            try
            {
                var unPago = (from p in _contexto.Pagos
                              join e in _contexto.Empresas on p.EmpresaId equals e.EmpresaId
                              join o in _contexto.EncabezadoOrden on p.OrdenId equals o.EncabezadoOrdenId
                              join u in _contexto.Usuarios on p.UsuarioId equals u.UsuarioId
                              where p.PagoId == idPago
                              select new
                              {
                                  p.PagoId,
                                  e.EmpresaId,
                                  e.NombreEmpresa,
                                  p.OrdenId,
                                  p.MovimientoCajaId,
                                  p.TipoPago,
                                  p.SubTotal,
                                  p.Propina,
                                  p.Total,
                                  p.MontoPagado,
                                  p.UsuarioId,
                                  Usuario = u.Nombres + " " + u.Apellidos,
                                  p.FechaCreacion,
                                  p.CreditoId,
                                  p.TarjetaNumero,
                                  p.NombreTarjeta,
                                  p.autorizacion,
                                  p.Estado,
                                  p.FechaModificacion
                              }).FirstOrDefault();
                if (unPago != null)
                {
                    return Ok(unPago);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/pagos_mesa/{idMesa}")]
        public IActionResult GetxMesa(int idMesa)
        {
            try
            {
                var listadoPagos = (from p in _contexto.Pagos
                                    join e in _contexto.Empresas on p.EmpresaId equals e.EmpresaId
                                    join o in _contexto.EncabezadoOrden on p.OrdenId equals o.EncabezadoOrdenId
                                    join u in _contexto.Usuarios on p.UsuarioId equals u.UsuarioId
                                    join m in _contexto.Mesas on o.MesaId equals m.MesaId
                                    where  o.MesaId == idMesa
                                    select new
                                    {
                                        p.PagoId,
                                        e.EmpresaId,
                                        e.NombreEmpresa,
                                        p.OrdenId,
                                        p.MovimientoCajaId,
                                        p.TipoPago,
                                        p.SubTotal,
                                        p.Propina,
                                        p.Total,
                                        p.MontoPagado,
                                        p.UsuarioId,
                                        Usuario = u.Nombres + " " + u.Apellidos,
                                        p.FechaCreacion,
                                        p.CreditoId,
                                        p.TarjetaNumero,
                                        p.NombreTarjeta,
                                        p.autorizacion,
                                        p.Estado,
                                        p.FechaModificacion
                                    }).OrderBy(p => p.PagoId);
                if (listadoPagos.Count() > 0)
                {
                    return Ok(listadoPagos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/pagos")]
        public IActionResult guardarPago([FromBody] Pagos pagoNuevo)
        {
            try
            {
                _contexto.Pagos.Add(pagoNuevo);
                _contexto.SaveChanges();
                return Ok(pagoNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/pagos")]
        public IActionResult updatePago([FromBody] Pagos pagoAModificar)
        {
            try
            {
                Pagos pagoExiste = (from p in _contexto.Pagos
                                    where p.PagoId == pagoAModificar.PagoId
                                    select p).FirstOrDefault();

                if (pagoExiste is null)
                {
                    return NotFound();
                }

                pagoExiste.TipoPago = pagoAModificar.TipoPago;
                pagoExiste.TipoPago = pagoAModificar.TipoPago;
                pagoExiste.Estado = pagoAModificar.Estado;
                pagoExiste.FechaModificacion = pagoAModificar.FechaModificacion;


                _contexto.Entry(pagoExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(pagoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

