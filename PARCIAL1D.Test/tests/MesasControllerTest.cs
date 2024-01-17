using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PARCIAL1D.Controllers;
using PARCIAL1D.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCIAL1D.Test.tests
{
    [TestClass]
    public class MesasControllerTest
    {
        private readonly DbContextOptions<ordenContext> _options;

        public MesasControllerTest()
        {
            // Configura las opciones de la base de datos en memoria (usando LocalDB)
            _options = new DbContextOptionsBuilder<ordenContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [TestInitialize]
        public void Initialize()
        {
            using (var contexto = new ordenContext(_options))
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.EnsureCreated();
            }
        }

        [TestMethod]
        public void ObtenerTodasLasMesas_DeberiaRetornarOk()
        {
            // Arrange
            using (var contexto = new ordenContext(_options))
            {
                // Agregamos datos para la tabla Mesas
                contexto.Mesas.Add(new Mesas
                {
                    MesaId = 1,
                    EmpresaId = 1,
                    DescripcionMesa = "Mesa de prueba",
                    ZonaMesa = "Zona de prueba",
                    SillasMesa = 4,
                    Estado = "Activa",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });

                // Agregamos datos para la tabla Empresas
                contexto.Empresas.Add(new Empresas
                {
                    EmpresaId = 1,
                    NombreEmpresa = "Empresa de prueba",
                    Representante = "Representante de prueba",
                    NIT = "123456789",
                    NRC = "NRC123",
                    Direcccion = "Dirección de prueba",
                    Correo = "correo@prueba.com",
                    Telefonos = "123456789",
                    Estado = "Activa",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });

                contexto.SaveChanges();
            }

            using (var contexto = new ordenContext(_options))
            {
                // Act
                var controlador = new MesasController(contexto);
                var resultado = controlador.Get();

                // Assert
                if (resultado is OkObjectResult okResult)
                {
                    // Verificamos si hay algún dato en el resultado
                    var mesasEnResultado = (IEnumerable<object>)okResult.Value;
                    if (mesasEnResultado.Any())
                    {
                        Console.WriteLine("Mesas encontradas en el resultado:");
                        foreach (var mesa in mesasEnResultado)
                        {
                            Console.WriteLine($"- {mesa}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron mesas en el resultado.");
                    }

                    Assert.IsTrue(mesasEnResultado.Any(), "No se encontraron mesas en el resultado.");
                }
                else
                {
                    Console.WriteLine($"Resultado: {resultado?.GetType().Name ?? "null"}");
                    Assert.Fail($"Esperaba un OkObjectResult, pero se obtuvo un {resultado?.GetType().Name ?? "null"}");
                }
            }
        }

        [TestMethod]
        public void ObtenerUnaMesaExistente_DeberiaRetornarOk()
        {
            // Arrange
            using (var contexto = new ordenContext(_options))
            {
                // Agregamos datos para la tabla Mesas
                contexto.Mesas.Add(new Mesas
                {
                    MesaId = 1,
                    EmpresaId = 1,
                    DescripcionMesa = "Mesa de prueba",
                    ZonaMesa = "Zona de prueba",
                    SillasMesa = 4,
                    Estado = "Activa",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });

                // Agregamos datos para la tabla Empresas
                contexto.Empresas.Add(new Empresas
                {
                    EmpresaId = 1,
                    NombreEmpresa = "Empresa de prueba",
                    Representante = "Representante de prueba",
                    NIT = "123456789",
                    NRC = "NRC123",
                    Direcccion = "Dirección de prueba",
                    Correo = "correo@prueba.com",
                    Telefonos = "123456789",
                    Estado = "Activa",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });

                contexto.SaveChanges();
            }

            using (var contexto = new ordenContext(_options))
            {
                // Act
                var controlador = new MesasController(contexto);
                var resultado = controlador.Get(1);

                // Assert
                if (resultado is OkObjectResult okResult)
                {
                    // Verificamos si hay algún dato en el resultado
                    var mesasEnResultado = okResult.Value as IEnumerable<object>;

                    if (mesasEnResultado != null && mesasEnResultado.Any())
                    {
                        Console.WriteLine("Mesas encontradas en el resultado:");
                        foreach (var mesa in mesasEnResultado)
                        {
                            Console.WriteLine($"- {mesa}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron mesas en el resultado.");
                    }

                    Assert.IsTrue(mesasEnResultado != null && mesasEnResultado.Any(), "No se encontraron mesas en el resultado.");
                }
                else
                {
                    Console.WriteLine($"Resultado: {resultado?.GetType().Name ?? "null"}");
                    Assert.Fail($"Esperaba un OkObjectResult, pero se obtuvo un {resultado?.GetType().Name ?? "null"}");
                }
            }
        }

        [TestMethod]
        public void ObtenerMesaInexistente_DeberiaRetornarNotFound()
        {
            // Arrange
            using (var contexto = new ordenContext(_options))
            {
                // No se agregan mesas ni empresas en este caso
            }

            using (var contexto = new ordenContext(_options))
            {
                // Act
                var controlador = new MesasController(contexto);
                var resultado = controlador.Get(1);

                // Assert
                Assert.IsInstanceOfType(resultado, typeof(NotFoundResult), "Esperaba un NotFoundResult.");
            }
        }

        [TestMethod]
        public void GuardarMesa_NuevaMesa_DeberiaRetornarOk()
        {
            // Arrange
            var nuevaMesa = new Mesas
            {
                MesaId = 3,
                EmpresaId = 1,
                DescripcionMesa = "Mesa de prueba",
                ZonaMesa = "Zona de prueba",
                SillasMesa = 4,
                Estado = "Activa",
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            using (var contexto = new ordenContext(_options))
            {
                // Act
                var controlador = new MesasController(contexto);
                var resultado = controlador.guardarMesa(nuevaMesa);

                // Assert
                Assert.IsInstanceOfType(resultado, typeof(OkObjectResult), "Esperaba un OkObjectResult.");
                var okResult = resultado as OkObjectResult;
                Assert.IsNotNull(okResult.Value, "El valor en el resultado no debe ser nulo.");
                var mesaGuardada = okResult.Value as Mesas;
                Assert.AreEqual(nuevaMesa.MesaId, mesaGuardada.MesaId, "Los IDs de las mesas no coinciden.");
            }
        }

        [TestMethod]
        public void ActualizarMesa_Existente_DeberiaRetornarOk()
        {
            // Arrange
            using (var contexto = new ordenContext(_options))
            {
                // Agregamos una mesa existente para actualizar
                var mesaExistente = new Mesas
                {
                    MesaId = 1,
                    EmpresaId = 1,
                    DescripcionMesa = "Mesa existente",
                    ZonaMesa = "Zona existente",
                    SillasMesa = 2,
                    Estado = "Inactiva",
                    FechaCreacion = DateTime.Now.AddMonths(-1),
                    FechaModificacion = DateTime.Now.AddMonths(-1)
                };
                contexto.Mesas.Add(mesaExistente);
                contexto.SaveChanges();
            }

            var mesaActualizada = new Mesas
            {
                MesaId = 1,
                EmpresaId = 1,
                DescripcionMesa = "Mesa actualizada",
                ZonaMesa = "Zona actualizada",
                SillasMesa = 4,
                Estado = "Activa",
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            using (var contexto = new ordenContext(_options))
            {
                // Act
                var controlador = new MesasController(contexto);
                var resultado = controlador.updateMesa(mesaActualizada);

                // Assert
                Assert.IsInstanceOfType(resultado, typeof(OkObjectResult), "Esperaba un OkObjectResult.");
                var okResult = resultado as OkObjectResult;
                Assert.IsNotNull(okResult.Value, "El valor en el resultado no debe ser nulo.");
                var mesaActualizadaEnResultado = okResult.Value as Mesas;
                Assert.AreEqual(mesaActualizada.DescripcionMesa, mesaActualizadaEnResultado.DescripcionMesa, "Las descripciones de las mesas no coinciden.");
            }
        }
    }
}
