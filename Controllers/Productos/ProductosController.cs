using APICOMMON.DTO.Productos;
using APICOMMON.Entidades;
using APICOMMON.UtilObjects;
using FACADE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data;
using Xunit;

namespace ASISYA.Controllers.Productos
{
    //[Authorize]
    [Route("api/RadicacionElectronica")]
    [ApiController]
    public class ProductosController : Controller
    {
        #region propiedades 

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ProductosController(IConfiguration configuration, HttpClient httpClient)
        {
            this._configuration = configuration;
            _httpClient = httpClient;
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        private FACADE.Facade _BusClass;
        public FACADE.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new FACADE.Facade(_configuration);
                }

            }
            set { _BusClass = value; }
        }

        #endregion

        #region metodos


        [HttpPost]
        [Route("CrearCategorias")]
        public IActionResult CrearCategorias([FromBody] CategoriesDTO request)
        {
            try
            {
                BusClass.InsertarCategoria(request);

                return Ok(new
                {
                    success = true,
                    message = "Categoría creada correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("CrearProductosMasivos")]
        public IActionResult CrearProductosMasivos([FromBody] ProductsDTO request)
        {
            try
            {
                if (request == null || request.Cantidad <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Cantidad inválida"
                    });
                }

                BusClass.InsertarProductosMasivos(request);

                return Ok(new
                {
                    success = true,
                    message = $"Se generaron {request.Cantidad} productos correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("ObtenerProductosFiltrados")]
        public IActionResult ObtenerProductosFiltrados([FromQuery] ProductosFiltradosDTO filtro)
        {
            try
            {
                var (productos, total) = BusClass.ObtenerProductos(filtro);

                return Ok(new
                {
                    success = true,
                    totalRecords = total,
                    page = filtro.Page,
                    pageSize = filtro.PageSize,
                    data = productos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al consultar productos",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("ObtenerProductoID/{id}")]
        public IActionResult ObtenerProductoID(int id)
        {
            try
            {
                var producto = BusClass.ObtenerProductoPorId(id);

                if (producto == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Producto no encontrado"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = producto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al obtener el producto",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("ActualizarProducto")]
        public IActionResult ActualizarProducto([FromBody] ProductosActEliDTO request)
        {
            try
            {
                BusClass.ActualizarProducto(request);

                return Ok(new
                {
                    success = true,
                    message = "Producto actualizado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("EliminarProductoID/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            try
            {
                BusClass.EliminarProducto(id);

                return Ok(new
                {
                    success = true,
                    message = "Producto eliminado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        #endregion metodos

    }
}
