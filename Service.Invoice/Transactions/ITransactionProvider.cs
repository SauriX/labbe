namespace Service.Billing.Transactions
{
    public interface ITransactionProvider
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
