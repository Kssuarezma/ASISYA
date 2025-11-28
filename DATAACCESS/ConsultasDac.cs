using APICOMMON.DTO.Productos;
using APICOMMON.Entidades;
using APICOMMON.UtilObjects;
using DATAACCESS.Exten;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAACCESS
{
    public class ConsultasDac
    {
        private readonly IConfiguration _configuration;
        public ConsultasDac(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Propiedades
        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        #endregion


        #region productos
        public (List<productosListaDTO> productos, int totalRegistros) ObtenerProductos(ProductosFiltradosDTO filtro)
        {
            using (var db = new ASISYSDB(_configuration))
            {
                var query = from p in db.Products
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            join s in db.Suppliers on p.SupplierID equals s.SupplierID
                            select new productosListaDTO
                            {
                                ProductID = p.ProductID,
                                ProductName = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                UnitsInStock = p.UnitsInStock,
                                CategoryName = c.CategoryName,
                                SupplierName = s.CompanyName
                            };

                if (!string.IsNullOrEmpty(filtro.Search))
                {
                    query = query.Where(x => x.ProductName.Contains(filtro.Search));
                }

                if (filtro.CategoryId.HasValue)
                {
                    query = query.Where(x => db.Products.Any(p => p.CategoryID == filtro.CategoryId));
                }

                if (filtro.SupplierId.HasValue)
                {
                    query = query.Where(x => db.Products.Any(p => p.SupplierID == filtro.SupplierId));
                }

                if (filtro.MinPrice.HasValue)
                {
                    query = query.Where(x => x.UnitPrice >= filtro.MinPrice);
                }

                if (filtro.MaxPrice.HasValue)
                {
                    query = query.Where(x => x.UnitPrice <= filtro.MaxPrice);
                }

                int total = query.Count();

                var data = query
                    .Skip((filtro.Page - 1) * filtro.PageSize)
                    .Take(filtro.PageSize)
                    .ToList();

                return (data, total);
            }
        }

        public ProductoDetalleArchivoDTO? ObtenerProductoPorId(int id)
        {
            using (var db = new ASISYSDB(_configuration))
            {
                var result = (from p in db.Products
                              join c in db.Categories
                                on p.CategoryID equals c.CategoryID
                              where p.ProductID == id
                              select new ProductoDetalleArchivoDTO
                              {
                                  ProductID = p.ProductID,
                                  ProductName = p.ProductName,
                                  UnitPrice = p.UnitPrice,
                                  UnitsInStock = p.UnitsInStock,
                                  CategoryName = c.CategoryName,
                                  CategoryPicture = c.Picture
                              }).FirstOrDefault();

                return result;
            }
        }
        #endregion
    }
}
