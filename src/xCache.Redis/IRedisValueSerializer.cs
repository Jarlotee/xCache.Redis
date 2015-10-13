namespace xCache.Redis
{
    public interface IRedisValueSerializer
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string item);
    }
}
