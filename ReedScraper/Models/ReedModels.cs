namespace ReedScraper.Models;

        public class ReedResponse
        { 
            public List<ReedJob> results { get; set; } = new(); 
        }
        public class ReedJob
        {
            public string jobTitle { get; set; } = "";
            public string employerName { get; set; } = "";
            public string jobDescription { get; set; } = "";
        }
