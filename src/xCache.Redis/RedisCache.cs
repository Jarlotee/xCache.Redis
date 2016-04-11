using StackExchange.Redis;
using System;
using System.Diagnostics;

namespace xCache.Redis
{
    public class RedisCache : ICache
    {
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly IRedisValueSerializer _serializer;

        public RedisCache(IConnectionMultiplexer multiplexer, IRedisValueSerializer serializer)
        {
            _multiplexer = multiplexer;
            _serializer = serializer;
        }

        public void Add<T>(string key, CacheItem<T> item)
        {
            try
            {
                var db = _multiplexer.GetDatabase();

                db.StringSet(key, _serializer.Serialize(item));
                db.KeyExpire(key, item.Expires);
            }
            catch (Exception ex)
            {
                Trace.TraceError("An error occured when trying to add an item to RedisCache [{0}] /n Stack Trace:/n{1}", ex.Message, ex.StackTrace);
            }
        }

        CacheItem<T> ICache.Get<T>(string key)
        {
            try
            {
                var db = _multiplexer.GetDatabase();

                var result = db.StringGet(key);

                if (result.HasValue)
                {
                    return _serializer.Deserialize<CacheItem<T>>(result);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("An error occured when trying to get an item from RedisCache [{0}] /nStack Trace: /n{1}", ex.Message, ex.StackTrace);
            }

            return null;
        }

        public bool Remove(string key)
        {
            try
            {
                var db = _multiplexer.GetDatabase();
                return db.KeyDelete(key);
            }
            catch (Exception ex)
            {
                Trace.TraceError("An error occured when trying to remove an item to RedisCache [{0}] /n Stack Trace:/n{1}", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}
