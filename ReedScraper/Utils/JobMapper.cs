using ReedScraper.Apify;
using ReedScraper.Models;
using System.Text.RegularExpressions;

namespace ReedScraper.Utils
{
    public static class JobMapper
    {
        public static JobResult Map(ApifyJobItem item)
        {
            return new JobResult
            {
                CompanyName = Extract(item.text, @"Company[:\s]+(.+)"),
                JobDescription = item.text,
                Skills = ExtractSkills(item.text),
                ContactPerson = Extract(item.text, @"Contact[:\s]+(.+)"),
                Email = Regex.Match(item.text, @"[\w\.-]+@[\w\.-]+\.\w+").Value,
                Phone = Regex.Match(item.text, @"\+?\d[\d\s\-]{8,}").Value
            };
        }

        private static string Extract(string text, string pattern)
        {
            var match = Regex.Match(text, pattern);
            return match.Success ? match.Groups[1].Value.Trim() : "N/A";
        }

        private static string ExtractSkills(string text)
        {
            string[] skills = { "C#", ".NET", "SQL", "Angular", "React", "Python" };
            return string.Join(", ", skills.Where(s => text.Contains(s, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
