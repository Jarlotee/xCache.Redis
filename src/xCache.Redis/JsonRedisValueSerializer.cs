using Newtonsoft.Json;

namespace xCache.Redis
{
    public class JsonRedisValueSerializer : IRedisValueSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonRedisValueSerializer() : this(null) { }

        public JsonRedisValueSerializer(JsonSerializerSettings settings)
        {
            _settings = settings ?? new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public T Deserialize<T>(string item)
        {
            return JsonConvert.DeserializeObject<T>(item, _settings);
        }

        public string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item, _settings);
        }
    }
}
