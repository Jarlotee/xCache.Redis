//using StackExchange.Redis;
//using System;
//using System.Diagnostics;
//using xCache.Durable;

//namespace xCache.Redis.Durable
//{
//    public class RedisDurableCacheQueue : IDurableCacheQueue
//    {
//        private readonly IConnectionMultiplexer _multiplexer;
//        private readonly IDurableCacheRefreshHandler _handler;
//        private readonly ISubscriber _subscriber;
//        private readonly IRedisValueSerializer _serializer;

//        public RedisDurableCacheQueue(IConnectionMultiplexer multiplexer,
//            IDurableCacheRefreshHandler handler, IRedisValueSerializer serializer)
//        {
//            _multiplexer = multiplexer;
//            _handler = handler;
//            _serializer = serializer;
//            _subscriber = multiplexer.GetSubscriber();

//            _subscriber.Subscribe("__keyevent@0__:expired", (channel, value) =>
//            {
//                Trace.TraceInformation("Heard: {0}", value);

//                if (!value.IsNull && value.ToString().StartsWith("{\"AbsoluteExpiration\":"))
//                {
//                    var refreshEvent = _serializer.Deserialize<DurableCacheRefreshEvent>(value.ToString());

//                    //TODO handle refresh

//                    ScheduleRefresh(refreshEvent);
//                }
//            });
//        }

//        public void Purge()
//        {
//            throw new NotImplementedException();
//        }

//        public void ScheduleRefresh(DurableCacheRefreshEvent refreshEvent)
//        {
//            var key = _serializer.Serialize(refreshEvent);
//            var db = _multiplexer.GetDatabase();

//            db.StringSet(key, "huh", refreshEvent.RefreshTime);
//        }
//    }
//}
