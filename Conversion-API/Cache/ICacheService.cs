namespace ConversionAPI.Cache
{
    public interface ICacheService
    {
        bool TryGetValue<TItem>(object key, out TItem value);
        void SetSlidingExpiration<TItem>(string key, TItem value, TimeSpan? slidingExpirationInMinutes = null);
        void Remove(object key);
    }
}
