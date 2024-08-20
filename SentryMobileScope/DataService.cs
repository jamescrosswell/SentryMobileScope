namespace SentryMobileScope;

public class DataService
{
    private readonly Database _database;
    
    public DataService(Database database)
    {
        _database = database;
    }
    
    public void Add(int id)
    {
        _database.Save(id, new object());
    }

    public async Task SendAllData()
    {
        foreach (var id in _database.GetIds())
        {
            _database.Get(id);
            //make service call
            await Task.Delay(100);
            _database.Delete(id);
        }
    }
}