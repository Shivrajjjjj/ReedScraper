namespace ReedScraper.Models
{
    public class AdzunaResponse
    {
        public List<AdzunaJob> results { get; set; } = new();
    }

    public class AdzunaJob
    {
        public string title { get; set; } = "";
        public string description { get; set; } = "";
        public AdzunaCompany company { get; set; } = new();
    }

    public class AdzunaCompany
    {
        public string display_name { get; set; } = "";
    }
}