using CsvHelper;
using ReedScraper.Models;
using System.Globalization;

namespace ReedScraper.Utils
{
    public static class CsvWriterHelper
    {
        public static void WriteToCsv(List<JobResult> jobs)
        {
            var fileName = $"ReedJobs_{DateTime.Now:yyyyMMdd}.csv";

            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(jobs);
        }
    }
}
