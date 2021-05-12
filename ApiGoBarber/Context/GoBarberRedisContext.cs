using StackExchange.Redis;

namespace ApiGoBarber.Context
{
    public class GoBarberRedisContext : IGoBarberRedisContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public GoBarberRedisContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = _redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }

    }
}
