namespace CommentKillerPOC.Services
{
    public record DataUsage(DateTime timestamp, int size);

    public interface IDataMonitoringService
    {
        Task<bool> CheckDataUsage(string id, TimeSpan requestTimeSpan);
    }

    public class DataMonitoringService : IDataMonitoringService
    {
        const int MAX_SIZE = 36_000;

        private readonly ICacheService _cacheService;

        public DataMonitoringService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<bool> CheckDataUsage(string id, TimeSpan requestTimespan)
        {
            var size = Convert.ToInt32(requestTimespan.TotalSeconds);
            var current = new DataUsage(DateTime.Now, size);
            var data = await _cacheService.GetAsync<List<DataUsage>>($"cdd-data-usage-{id}");
            
            if (data is null)
            {
                data = new List<DataUsage>();
            }
            
            data.Add(current);
            data = data.Where(d => d.timestamp > DateTime.Now.AddMinutes(-1)).ToList();

            if (data.Sum(d => d.size) <= MAX_SIZE)
            {
                await _cacheService.SetAsync($"cdd-data-usage-{id}", data);
                return true;
            }

            return false;
        }
    }
}
