namespace SentryMobileScope;

public partial class MainPage : ContentPage
{
    private readonly DataService _dataService;
    private readonly WorkQueue _queue;
    private int _counter = 0;
    public MainPage()
    {
        InitializeComponent();
        
        var database = new Database();
        _dataService = new DataService(database);
        _queue = new WorkQueue(_dataService);
        _queue.Start();
    }
    
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // var transaction = SentrySdk.StartTransaction($"click", "test.ui.command");
        using var task = Tracing.Source.StartActivity();
        task.SetDisplayName("test.ui.command");
        for (int i = 0; i < 10; i++)
        {
            _dataService.Add(_counter++);
            _queue.Enqueue("send");
            await Task.Delay(20);
        }
    }
}