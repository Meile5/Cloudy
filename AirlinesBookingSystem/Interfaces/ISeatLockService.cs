namespace AirlinesBookingSystem.Interfaces;

public interface ISeatLockService
{
    Task<bool> TryLockSeatAsync(string flightId, string seatId, string sagaId);
    Task ReleaseSeatAsync(string flightId, string seatId);
}
