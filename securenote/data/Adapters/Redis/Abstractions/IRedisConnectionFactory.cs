using StackExchange.Redis;

namespace data.Adapters.Redis.Abstractions
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer GetConnection();
    }
}