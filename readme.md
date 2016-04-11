# xCache.Redis

This libarary extends [xCache] to use Redis as the underlying store. 

## Installation

The [library] is available on nuget 

`install-package xCache.Redis`

## Usage

> Register your preffered caching service to the ICache interface through your DI container.

```csharp

	//Register	
	Ioc.RegisterInstance<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
	Ioc.RegisterInstance<IRedisValueSerializer>(new JsonRedisValueSerializer());
	Ioc.RegisterType<ICache, xCache.Redis.RedisCache>();

```

> Resolve ICache and handle your caching needs

```csharp
	
	var cache = Ioc.Resolve<ICache>();
	var key = "testKey";
	
	var result = cache.Get<string>(key);
	
	if(result == null)
	{
		result = GetResult();
		
		//Cache for 5 minutes
		cache.Add<string>(key, result, new Timespan(0,5,0));
	}
```

## Aop

xCache.Redis is compliant with Aspect oriented programming (Aop)

See the [xCache] readme for more information.

### Version
0.2.1

Please note that xCache.Redis@0.1.0 is only compatible with the xCache@0.1.0

### License
MIT

[xCache]:https://github.com/Jarlotee/xCache/
[library]:https://www.nuget.org/packages/xCache.Redis/
