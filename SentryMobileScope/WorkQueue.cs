namespace SentryMobileScope;

public class WorkQueue
{
    System.Collections.Concurrent.ConcurrentQueue<string> queue = new();
    private readonly IsolatedTransactionManager _transactionManager;
    private readonly DataService _dataService;

    public WorkQueue()
    {
        Scope? rootScope = null;
        SentrySdk.ConfigureScope(scope => rootScope = scope);

        _transactionManager = new IsolatedTransactionManager(rootScope);
        _dataService = new (new (_transactionManager));
    }
    
    public void Enqueue(string task)
    {
        queue.Enqueue(task);
    }

    public Task Start()
    {
        return Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(50);
                if (queue.TryDequeue(out var item))
                {
                    var transaction = _transactionManager.StartTransaction(item, "test.workQueue");
                    await _dataService.SendAllData();
                    transaction.Finish();
                }
            }
        });
    }
}