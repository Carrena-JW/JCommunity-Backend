using System.Collections.Concurrent;

namespace JCommunity.Infrastructure.MessagePublisher;

public static class TemporaryMemoryStorage
{
    private static ConcurrentDictionary<string, QueueRecord> _storage =
        new ConcurrentDictionary<string, QueueRecord>();

    public static ConcurrentDictionary<string, QueueRecord> GetAllItems() => _storage;

    public static int StoredRecordCount()
    {
        return _storage.Count;
    }

    public static void AddRecord(string id, QueueRecord record)
    {
        _storage[id] = record;
    }

    public static bool TryGetRecord(string id, out QueueRecord? record)
    {
        return _storage.TryGetValue(id, out record);
    }

    public static void RemoveRecord(string id)
    {
        _storage.TryRemove(id, out _);
    }

    public static void RemoveAllRecord()
    {
        _storage.Clear();
    }
}
