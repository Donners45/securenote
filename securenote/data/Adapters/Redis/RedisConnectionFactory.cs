using System;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using data.Notes.Abstractions;
using data.Adapters.Redis.Abstractions;
using domain;

namespace data.Adapters.Redis
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public RedisConnectionFactory(IOptions<RedisConfiguration> options)
        {
            _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options.Value.Host));
        }

        public ConnectionMultiplexer GetConnection() => _connection.Value;
    }
}
