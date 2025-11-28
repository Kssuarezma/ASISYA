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
    public class ActualizarDac
    {

        private readonly IConfiguration _configuration;
        public ActualizarDac(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Propiedades
        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        #endregion Propiedades

        #region productos

        public void ActualizarProducto(ProductosActEliDTO dto)
        {
            using (var db = new ASISYSDB(_configuration))
            {
                var producto = db.Products
                                 .FirstOrDefault(p => p.ProductID == dto.ProductID);

                if (producto == null)
                    throw new Exception("Producto no encontrado");

                producto.ProductName = dto.ProductName;
                producto.SupplierID = dto.SupplierID;
                producto.CategoryID = dto.CategoryID;
                producto.UnitPrice = dto.UnitPrice;
                producto.UnitsInStock = dto.UnitsInStock;
                producto.Discontinued = dto.Discontinued;

                db.SaveChanges();
            }
        }

        #endregion
    }
}
