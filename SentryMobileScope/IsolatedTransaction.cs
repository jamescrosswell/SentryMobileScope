namespace SentryMobileScope;

public class IsolatedTransaction(IsolatedTransactionManager transactionManager, Scope? scope, ITransactionTracer transaction)
{
    public void Finish()
    {
        transactionManager.ResetTransaction(this);
        SentrySdk.CaptureTransaction(new SentryTransaction(transaction), scope, null);
    }

    public ISpan? StartChild(string operation, string description) => transaction.StartChild(operation, description);
    
    public ISpan AsSpan() => transaction;
}