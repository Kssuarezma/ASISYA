using APICOMMON.Entidades;
using APICOMMON.DTO;
using DATAACCESS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICOMMON.DTO.Productos;



namespace FACADE
{
    public class Facade
    {
        private readonly IConfiguration _configuration;

        public Facade(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region  Propiedades

        private ConsultasDac _DACConsulta;
        public ConsultasDac DACConsulta
        {
            get
            {
                if (_DACConsulta != null)
                {
                    return _DACConsulta;
                }
                else
                {
                    return _DACConsulta = new ConsultasDac(_configuration);
                }

            }
            set { _DACConsulta = value; }
        }

        private ActualizarDac _DACActualiza;
        public ActualizarDac DACActualiza
        {
            get
            {
                if (_DACActualiza != null)
                {
                    return _DACActualiza;
                }
                else
                {
                    return _DACActualiza = new ActualizarDac(_configuration);
                }

            }
            set { _DACActualiza = value; }
        }

        private InsertarDac _DACInserta;
        public InsertarDac DACInserta
        {
            get
            {
                if (_DACInserta != null)
                {
                    return _DACInserta;
                }
                else
                {
                    return _DACInserta = new InsertarDac(_configuration);
                }

            }
            set { _DACInserta = value; }
        }

        private EliminarDac _DACElimina;
        public EliminarDac DACElimina
        {
            get
            {
                if (_DACElimina != null)
                {
                    return _DACElimina;
                }
                else
                {
                    return _DACElimina = new EliminarDac(_configuration);
                }

            }
            set { _DACElimina = value; }
        }

        #endregion

        #region Categorias

        public void InsertarCategoria(CategoriesDTO dto)
        {
            var entity = new Categories
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                Picture = dto.Picture
            };

            DACInserta.InsertarCategoria(entity);
        }

        #endregion Categorias

        #region Productos

        public void InsertarProductosMasivos(ProductsDTO request)
        {
            if (request == null) throw new Exception("Request vacío");

            var random = new Random();
            var productos = new List<Products>();

            for (int i = 0; i < request.Cantidad; i++)
            {
                productos.Add(new Products
                {
                    ProductName = $"Producto_{Guid.NewGuid().ToString("N").Substring(0, 8)}",
                    SupplierID = random.Next(request.MinSupplierId, request.MaxSupplierId + 1),
                    CategoryID = random.Next(request.MinCategoryId, request.MaxCategoryId + 1),
                    QuantityPerUnit = "1 unidad",
                    UnitPrice = (decimal)(random.NextDouble() * 500 + 1),
                    UnitsInStock = random.Next(0, 500),
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                });
            }

            DACInserta.InsertarProductosMasivos(productos);
        }

        public (List<productosListaDTO>, int) ObtenerProductos(ProductosFiltradosDTO filtro)
        {
            return DACConsulta.ObtenerProductos(filtro);
        }

        public ProductoDetalleArchivoDTO? ObtenerProductoPorId(int id)
        {
            return DACConsulta.ObtenerProductoPorId(id);
        }

        public void ActualizarProducto(ProductosActEliDTO dto)
        {
            DACActualiza.ActualizarProducto(dto);
        }

        public void EliminarProducto(int id)
        {
            DACElimina.EliminarProducto(id);
        }


        #endregion


    }
}
