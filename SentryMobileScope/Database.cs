namespace SentryMobileScope;

public class Database
{
    readonly System.Collections.Concurrent.ConcurrentDictionary<int, object> _data = new();

    public void Get(int id)
    {
        // var span = SentrySdk.GetSpan()?.StartChild("database", $"get {id}");
        using var task = Tracing.Source.StartActivity();
        task.SetDisplayName($"get {id}");
        _data.TryGetValue(id, out _);
    }
    public void Save(int id, object data)
    {
        // var span = SentrySdk.GetSpan()?.StartChild("database", $"save {id}");
        using var task = Tracing.Source.StartActivity();
        task.SetDisplayName($"save {id}");
        _data.TryAdd(id, data);
    }
    public void Delete(int id)
    {
        // var span = SentrySdk.GetSpan()?.StartChild("database", $"remove {id}");
        using var task = Tracing.Source.StartActivity();
        task.SetDisplayName($"remove {id}");
        _data.Remove(id, out var _);
    }
    public ICollection<int> GetIds()
    {
        return _data.Keys;
    }
}