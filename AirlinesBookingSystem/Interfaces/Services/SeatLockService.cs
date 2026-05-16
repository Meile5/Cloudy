namespace AirlinesBookingSystem.Interfaces.Services;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase; 

public class SeatLockService : ISeatLockService
{
    private readonly IDatabase _redis;
    private readonly TimeSpan _lockTtl = TimeSpan.FromMinutes(10);

    public SeatLockService(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task<bool> TryLockSeatAsync(string flightId, string seatId, string sagaId)
    {
        var key = $"seat_lock:{flightId}:{seatId}";
        return await _redis.StringSetAsync(key, sagaId, _lockTtl, When.NotExists);
    }

    public async Task ReleaseSeatAsync(string flightId, string seatId)
    {
        var key = $"seat_lock:{flightId}:{seatId}";
        await _redis.KeyDeleteAsync(key);
    }
}