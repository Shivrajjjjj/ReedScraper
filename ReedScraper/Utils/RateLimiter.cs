namespace ReedScraper.Utils;

public static class RateLimiter
{
    private static readonly SemaphoreSlim _lock = new(1, 1);
    public static async Task WaitAsync()
    {
        await _lock.WaitAsync();
        try
        {
            // Reed API allows ~1 request per second comfortably. 
            // We use 2 seconds to be absolutely safe from IP flags.
            await Task.Delay(2000);
        }
        finally { _lock.Release(); }
    }
}