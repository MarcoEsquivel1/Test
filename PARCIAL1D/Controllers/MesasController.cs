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
    public class MesasController : ControllerBase
    {
        private readonly ordenContext _contexto;

        public MesasController(ordenContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/mesas/")]
        public IActionResult Get()
        {
            try
            {
                var listadoMesas = (from m in _contexto.Mesas
                                    join e in _contexto.Empresas on m.EmpresaId equals e.EmpresaId
                                    select new
                                    {
                                        m.MesaId,
                                        e.EmpresaId,
                                        e.NombreEmpresa,
                                        m.DescripcionMesa,
                                        m.ZonaMesa,
                                        m.SillasMesa,
                                        m.Estado,
                                        m.FechaCreacion,
                                        m.FechaModificacion,
                                    });
                if (listadoMesas.Count() > 0)
                {
                    return Ok(listadoMesas);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/mesas/{idMesa}")]
        public IActionResult Get(int idMesa)
        {
            try
            {
                var unaMesa = (from m in _contexto.Mesas
                               join e in _contexto.Empresas on m.EmpresaId equals e.EmpresaId
                               where m.MesaId == idMesa
                               select new
                               {
                                   m.MesaId,
                                   e.EmpresaId,
                                   e.NombreEmpresa,
                                   m.DescripcionMesa,
                                   m.ZonaMesa,
                                   m.SillasMesa,
                                   m.Estado,
                                   m.FechaCreacion,
                                   m.FechaModificacion,
                               }).ToList();
                if (unaMesa.Count > 0)
                {
                    return Ok(unaMesa);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/mesas")]
        public IActionResult guardarMesa([FromBody] Mesas mesaNueva)
        {
            try
            {
                _contexto.Mesas.Add(mesaNueva);
                _contexto.SaveChanges();
                return Ok(mesaNueva);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/mesas")]
        public IActionResult updateMesa([FromBody] Mesas mesaAModificar)
        {
            try
            {
                Mesas mesaExiste = (from m in _contexto.Mesas
                                    where m.MesaId == mesaAModificar.MesaId
                                    select m).FirstOrDefault();

                if (mesaExiste is null)
                {
                    return NotFound();
                }

                mesaExiste.DescripcionMesa = mesaAModificar.DescripcionMesa;
                mesaAModificar.ZonaMesa = mesaAModificar.ZonaMesa;
                mesaAModificar.SillasMesa = mesaAModificar.SillasMesa;
                mesaAModificar.Estado = mesaAModificar.Estado;
                mesaAModificar.FechaModificacion = mesaAModificar.FechaModificacion;


                _contexto.Entry(mesaExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(mesaExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

