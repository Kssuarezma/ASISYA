using APICOMMON.UtilObjects;
using FACADE;

namespace ASISYA.Models.Autenticacion
{
    public class External
    {
        #region Propiedades

        private readonly IConfiguration _configuration;
        public External(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion



    }
}
