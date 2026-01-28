using System.Text;
using ReedScraper.Models;

namespace ReedScraper.Utils;

public static class CsvWriterHelper
{
    public static void Write(string path, List<JobResult> jobs)
    {
        var sb = new StringBuilder();
        sb.AppendLine("CompanyName,JobDescription,Skills,ContactPerson,Email,Phone,Title");

        foreach (var job in jobs)
        {
            // Use semi-colon or double quotes to ensure the categories don't break CSV columns
            string skills = job.Skills.Replace(",", ";");
            string desc = job.JobDescription.Replace("\"", "'").Replace("\n", " ").Replace("\r", "");

            sb.AppendLine($"\"{job.CompanyName}\",\"{desc}\",\"{skills}\",\"{job.ContactPerson}\",\"{job.Email}\",\"{job.Phone}\",\"{job.title}\"");
        }
        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }
}