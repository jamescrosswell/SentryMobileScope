namespace SentryMobileScope;

public class Database
{
    readonly System.Collections.Concurrent.ConcurrentDictionary<int, object> _data = new();

    public void Get(int id)
    {
        var span = SentrySdk.GetSpan()?.StartChild("database", $"get {id}");
        _data.TryGetValue(id, out _);
        span?.Finish();
    }
    public void Save(int id, object data)
    {
        var span = SentrySdk.GetSpan()?.StartChild("database", $"save {id}");
        _data.TryAdd(id, data);
        span?.Finish();
    }
    public void Delete(int id)
    {
        var span = SentrySdk.GetSpan()?.StartChild("database", $"remove {id}");
        _data.Remove(id, out var _);
        span?.Finish();
    }
    public ICollection<int> GetIds()
    {
        return _data.Keys;
    }
}