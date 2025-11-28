using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.Enum
{
    public class BussinesEnums
    {
        public enum EnumTipoEjecucion
        {
            SP,
            Query
        }
        public enum EnumTipoRespuesta
        {
            Error,
            Correcto
        }

        public enum EnumTipoDeLog
        {
            User,
            Trace,
            Error,
            DataBaseError,
            DataBaseTime,
            CriticsChangeClient
        }
    }
}
