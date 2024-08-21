namespace SentryMobileScope;

public partial class MainPage : ContentPage
{
    private readonly IsolatedTransactionManager _transactionManager;
    private readonly DataService _dataService;
    private readonly WorkQueue _queue;
    private int _counter = 0;
    public MainPage()
    {
        InitializeComponent();

        Scope? rootScope = null;
        SentrySdk.ConfigureScope(scope => rootScope = scope);

        _transactionManager = new IsolatedTransactionManager(rootScope);
        _dataService = new (new (_transactionManager));
        
        _queue = new WorkQueue();
        _queue.Start();
    }
    
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var transaction = _transactionManager.StartTransaction($"click", "test.ui.command");
        for (var i = 0; i < 10; i++)
        {
            _dataService.Add(_counter++);
            _queue.Enqueue("send");
            await Task.Delay(20);
        }
        transaction.Finish();
    }
}