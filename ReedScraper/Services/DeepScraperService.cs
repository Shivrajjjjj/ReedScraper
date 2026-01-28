using System.Net.Http;
using ReedScraper.Utils;

namespace ReedScraper.Services
{
    public class DeepScraperService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<(string Email, string Phone)> ScrapeContactInfo(string jobUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(jobUrl)) return ("N/A", "N/A");

                // Visit the actual job page
                string htmlContent = await _httpClient.GetStringAsync(jobUrl);

                // Use the DataScanner on the FULL page HTML
                string email = DataScanner.ExtractEmail(htmlContent);
                string phone = DataScanner.ExtractPhone(htmlContent);

                return (email, phone);
            }
            catch
            {
                return ("See Website", "See Website");
            }
        }
    }
}