using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ReedScraper.Apify
{
    public class ApifyClient
    {
        private readonly HttpClient _client;
        private const string API_TOKEN = "apify_api_Ma4NOsx6yzIeKNWVWgJ9XKx0dRJJgz229ItX";
        private const string ACTOR_ID = "apify/website-content-crawler";

        public ApifyClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", API_TOKEN);
        }

        public async Task<List<ApifyJobItem>> RunActorAsync(string url)
        {
            var input = new { startUrls = new[] { new { url } }, maxCrawlPages = 5 };

            var response = await _client.PostAsync(
                $"https://api.apify.com/v2/acts/{ACTOR_ID}/runs?wait=1",
                new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            // Deserialize response safely
            dynamic run = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            string? datasetId = run?.data?.defaultDatasetId;
            if (string.IsNullOrEmpty(datasetId))
                return new List<ApifyJobItem>(); // Return empty list if datasetId is null

            // Fetch dataset safely
            var datasetJson = await _client.GetStringAsync(
                $"https://api.apify.com/v2/datasets/{datasetId}/items");

            // Deserialize safely and return empty list if null
            var items = JsonConvert.DeserializeObject<List<ApifyJobItem>>(datasetJson);
            return items ?? new List<ApifyJobItem>();
        }
    }
}
