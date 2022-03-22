using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Transactions
{
    public interface ITransactionProvider
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
