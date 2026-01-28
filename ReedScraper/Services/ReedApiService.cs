using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ReedScraper.Models;
using ReedScraper.Utils;

namespace ReedScraper.Services;

public class ReedApiService
{
    private readonly HttpClient _client = new();
    private const string API_KEY = "bb9f5ed4-4a80-40bc-a2ce-81ab4acb622a";

    public async Task<List<JobResult>> GetJobsAsync(string keyword)
    {
        await RateLimiter.WaitAsync();
        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{API_KEY}:"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

        string url = $"https://www.reed.co.uk/api/1.0/search?keywords={keyword}&postedWithin=1";

        try
        {
            var response = await _client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<ReedResponse>(response);

            return data?.results.Select(j => new JobResult
            {
                title = j.jobTitle,
                CompanyName = j.employerName,
                JobDescription = j.jobDescription,
                Skills = DataScanner.ExtractSkills(j.jobDescription),
                ContactPerson = "Hiring Manager",
                Email = DataScanner.ExtractEmail(j.jobDescription), // Now uses Deep Scan
                Phone = DataScanner.ExtractPhone(j.jobDescription)
            }).ToList() ?? new();
        }
        catch { return new List<JobResult>(); }
    }
}