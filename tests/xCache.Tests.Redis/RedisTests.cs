using StackExchange.Redis;
using System;
using System.Threading;
using xCache.Redis;
using Xunit;
using xCache.Extensions;

namespace xCache.Tests.Redis
{
    public class RedisTests
    {
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly IRedisValueSerializer _serializer;
        private readonly ICache _cache;
        
        public RedisTests()
        {
            _multiplexer = ConnectionMultiplexer.Connect("localhost");
            _serializer = new JsonRedisValueSerializer();
            _cache = new RedisCache(_multiplexer, _serializer);
        }

        [Fact]
        public void TestFiveSecondTimeout()
        {
            var key = "time";

            var now = DateTime.Now.ToString();

            _cache.Add(key, CacheExtensions.Wrap(now, DateTime.UtcNow.Add(new TimeSpan(0,0,5))));

            Thread.Sleep(new TimeSpan(0, 0, 2));

            var cached = CacheExtensions.Unwrap(_cache.Get<string>(key));

            Assert.Equal(now, cached);

            Thread.Sleep(new TimeSpan(0, 0, 5));

            Assert.Null(_cache.Get<string>(key));
        }

        [Fact]
        public void TestFiveSecondTimeoutStruct()
        {
            var key = "time2";

            var now = DateTime.Now;

            _cache.Add(key, CacheExtensions.Wrap(now, DateTime.UtcNow.Add(new TimeSpan(0, 0, 5))));

            Thread.Sleep(new TimeSpan(0, 0, 2));

            var cached = CacheExtensions.Unwrap(_cache.Get<DateTime>(key));

            Assert.Equal(now, cached);

            Thread.Sleep(new TimeSpan(0, 0, 5));

            Assert.Null(_cache.Get<string>(key));
        }
    }
}
