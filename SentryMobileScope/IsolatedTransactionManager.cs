namespace SentryMobileScope;

public class IsolatedTransactionManager(Scope? rootScope)
{
    IsolatedTransaction? _transaction;
    
    public IsolatedTransaction StartTransaction(string name, string operation)
    {
        var scope = rootScope?.Clone();
        var transaction = SentrySdk.StartTransaction(name, operation);
        if (scope != null)
        {
            scope.Transaction = transaction;
        }
        return _transaction = new IsolatedTransaction(this, scope, transaction);
    }

    public ISpan? StartSpan(string operation, string description)
    {
        return _transaction is { } transaction
            ? transaction.StartChild(operation, description)
            : StartTransaction(operation, description).AsSpan();
    }
    
    internal void ResetTransaction(IsolatedTransaction? expectedCurrentTransaction) =>
        Interlocked.CompareExchange(ref _transaction, null, expectedCurrentTransaction);
}