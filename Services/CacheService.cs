namespace CommentKillerPOC.Services
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
    }

    public class CacheService : ICacheService
    {
        public async Task<T> GetAsync<T>(string key)
        {
            return default(T);
        }

        public Task SetAsync<T>(string key, T value)
        {
            return Task.CompletedTask;
        }
    }
}
