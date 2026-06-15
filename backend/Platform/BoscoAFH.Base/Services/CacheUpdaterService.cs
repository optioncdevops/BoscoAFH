// <copyright file="CacheUpdaterService.cs" company="BBH">
// Copyright (c) BBH. All rights reserved.
// </copyright>

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace BoscoAFH.Base.Services
{
    public class CacheUpdaterService(IMemoryCache memoryCache): IHostedService, IDisposable
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Set the timer to trigger every hour
            using var _ =
            // Set the timer to trigger every hour
            _timer = new Timer(callback: UpdateCache,
                               null,
                               TimeSpan.Zero,
                               TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private void UpdateCache(object state)
        {
            var data = FetchData(); // Replace this with the actual method to get the data

            // Set the data in the cache with desired expiration options
            _memoryCache.Set("myDataKey", data, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
        }

        private List<string> FetchData()
        {
            // Fetch or generate the data
            return new List<string> { "Value1", "Value2", "Value3" };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
