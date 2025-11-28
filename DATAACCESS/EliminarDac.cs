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
    public class EliminarDac
    {
        private readonly IConfiguration _configuration;
        public EliminarDac(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Propiedades
        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        #endregion

        #region productos
        
        public void EliminarProducto(int id)
        {
            using (var db = new ASISYSDB(_configuration))
            {
                var producto = db.Products
                                 .FirstOrDefault(p => p.ProductID == id);

                if (producto == null)
                    throw new Exception("Producto no encontrado");

                db.Products.Remove(producto);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
