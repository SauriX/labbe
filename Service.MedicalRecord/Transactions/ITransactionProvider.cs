namespace Service.MedicalRecord.Transactions
{
    public interface ITransactionProvider
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
