using ReedScraper.Models;

namespace ReedScraper.Services
{
    public class JobAggregatorService
    {
        private readonly AdzunaService _adzuna = new();
        private readonly ReedApiService _reed = new(); // Updated to use the API service

        public async Task<List<JobResult>> GetAllJobsAsync(string keyword)
        {
            var allJobs = new List<JobResult>();

            try
            {
                // Run both requests. 
                // The RateLimiter.WaitAsync() inside each service will ensure 
                // they don't fire at the exact same millisecond.
                var adzunaTask = _adzuna.GetTodaysJobsAsync(keyword);
                var reedTask = _reed.GetJobsAsync(keyword);

                // Wait for both to complete
                var results = await Task.WhenAll(adzunaTask, reedTask);

                foreach (var list in results)
                {
                    allJobs.AddRange(list);
                }
            }
            catch (Exception)
            {
                // Handle aggregation errors
            }

            return allJobs;
        }
    }
}