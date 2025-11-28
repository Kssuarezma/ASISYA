using APICOMMON.Entidades;
using APICOMMON.UtilObjects;
using DATAACCESS.Exten;
using Microsoft.Extensions.Configuration;

namespace DATAACCESS
{
    public class InsertarDac
    {
        private readonly IConfiguration _configuration;
        public InsertarDac(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Propiedades
        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


        internal Repository DaccInsetfull
        {
            get
            {
                if (_DaccInsetfull == null)
                {
                    return _DaccInsetfull = new Repository(new ASISYSDB(_configuration));
                }
                else
                {
                    return _DaccInsetfull;
                }

            }

            set
            {
                _DaccInsetfull = value;
            }
        }
        private Repository _DaccInsetfull;
        
        #endregion


        #region categorias
        public void InsertarCategoria(Categories entity)
        {
            try
            {
                using (var db = new ASISYSDB(_configuration))
                {
                    //db.Database.OpenConnection();  
                    
                    db.Categories.Add(entity);   
                    db.SaveChanges();            
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la categoría: " + ex.Message);
            }
        }

        #endregion

        #region productos

        public void InsertarProductosMasivos(List<Products> productos)
        {
            try
            {
                using (var db = new ASISYSDB(_configuration))
                {
                    const int batchSize = 1000;

                    for (int i = 0; i < productos.Count; i += batchSize)
                    {
                        var lote = productos.Skip(i).Take(batchSize).ToList();

                        db.Products.AddRange(lote);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar productos en bloque: " + ex.Message);
            }
        }
        
        #endregion

    }
}
