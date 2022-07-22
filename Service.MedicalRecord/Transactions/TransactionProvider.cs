using Service.MedicalRecord.Context;

namespace Service.MedicalRecord.Transactions
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ApplicationDbContext _context;

        public TransactionProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
