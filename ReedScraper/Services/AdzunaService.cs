using Newtonsoft.Json;
using ReedScraper.Models;
using ReedScraper.Utils;

namespace ReedScraper.Services;

public class AdzunaService
{
    private readonly HttpClient _client = new();
    private const string APP_ID = "314d5b92";
    private const string APP_KEY = "2ddc82e59bd8b891209c73e95a545643";

    public async Task<List<JobResult>> GetTodaysJobsAsync(string keyword)
    {
        await RateLimiter.WaitAsync();
        string url = $"https://api.adzuna.com/v1/api/jobs/gb/search/1?app_id={APP_ID}&app_key={APP_KEY}&what={Uri.EscapeDataString(keyword)}&sort_by=date&max_days_old=1";

        try
        {
            var json = await _client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<AdzunaResponse>(json);
            var list = new List<JobResult>();

            foreach (var job in data?.results ?? new())
            {
                list.Add(new JobResult
                {
                    title = job.title,
                    CompanyName = job.company.display_name,
                    JobDescription = job.description,
                    Skills = DataScanner.ExtractSkills(job.description),
                    ContactPerson = "Recruiter",
                    Email = DataScanner.ExtractEmail(job.description),
                    Phone = DataScanner.ExtractPhone(job.description)
                });
            }
            return list;
        }
        catch { return new List<JobResult>(); }
    }
}