using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAACCESS.Exten
{
    interface IRepository
    {
        void BulkInsertEntities<TModelEntity>(IList<TModelEntity> modelEntities) where TModelEntity : class;
    }
}
