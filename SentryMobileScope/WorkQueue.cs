namespace SentryMobileScope;

public class WorkQueue
{
    System.Collections.Concurrent.ConcurrentQueue<string> queue = new();
    private readonly DataService _dataService;

    public WorkQueue(DataService dataService)
    {
        _dataService = dataService;
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
                    // var transaction = SentrySdk.StartTransaction(item, "test.workQueue");
                    using var task = Tracing.Source.StartActivity();
                    task.SetDisplayName("test.workQueue");
                    await _dataService.SendAllData();
                }
            }
        });
    }
}