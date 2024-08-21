namespace SentryMobileScope;

public class DataService(Database database)
{
    public void Add(int id)
    {
        database.Save(id, new object());
    }

    public async Task SendAllData()
    {
        foreach (var id in database.GetIds())
        {
            database.Get(id);
            //make service call
            await Task.Delay(100);
            database.Delete(id);
        }
    }
}