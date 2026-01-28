using System.Text;
using System.Text.RegularExpressions;

namespace ReedScraper.Utils
{
    public static class DataScanner
    {
        // 1. Primary Languages to show first
        private static readonly string[] PrimaryLanguages = {
            "Python", "JavaScript", "Java", "C++", "C#", "PHP", "Ruby", "Swift", "Kotlin", "TypeScript", "SQL", "Go"
        };

        // 2. Full Categorized Dictionary
        private static readonly Dictionary<string, string[]> Categories = new()
        {
            { "Programming & Development", new[] { "HTML5", "CSS3", "React.js", "Angular", "Node.js", "API Development", "Git", "Unit Testing", "Debugging", "Mobile App Development" } },
            { "Cloud & Infrastructure", new[] { "AWS", "Azure", "Google Cloud", "Cloud Migration", "Server Management", "Virtualization", "Docker", "Kubernetes", "Terraform", "CI/CD", "Linux", "Active Directory", "Networking", "VPN", "Load Balancing" } },
            { "Data Science & AI", new[] { "Machine Learning", "Deep Learning", "NLP", "Tableau", "Power BI", "Data Mining", "Hadoop", "Spark", "Statistical Analysis", "Data Warehousing", "Predictive Modeling", "Generative AI" } },
            { "Cybersecurity", new[] { "Ethical Hacking", "Penetration Testing", "Firewall", "Intrusion Detection", "Encryption", "Vulnerability Assessment", "IAM", "Risk Management", "Security Auditing", "Malware Analysis" } },
            { "UI/UX & Design", new[] { "UX Design", "UI Design", "Wireframing", "Prototyping", "Photoshop", "Illustrator", "Responsive Design" } },
            { "Project Management", new[] { "Agile", "Scrum", "Kanban", "ITIL", "SDLC", "Stakeholder Management" } },
            { "Essential Soft Skills", new[] { "Critical Thinking", "Problem-Solving", "Communication", "Teamwork", "Adaptability", "Leadership", "Negotiation" } },
            { "Specialized & Emerging", new[] { "Blockchain", "IoT", "Smart Contract", "Robotics", "Quantum", "Salesforce", "CRM", "SEO", "Shopify", "Magento" } }
        };

        public static string ExtractSkills(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "N/A";

            var output = new StringBuilder();

            // STEP 1: Find and show Primary Languages first
            var foundLanguages = PrimaryLanguages
                .Where(lang => Regex.IsMatch(text, $@"\b{Regex.Escape(lang)}\b", RegexOptions.IgnoreCase))
                .ToList();

            if (foundLanguages.Any())
            {
                output.Append(string.Join(", ", foundLanguages) + " | ");
            }

            // STEP 2: Find Categorized Skills
            foreach (var category in Categories)
            {
                var matches = category.Value
                    .Where(skill => Regex.IsMatch(text, $@"\b{Regex.Escape(skill)}\b", RegexOptions.IgnoreCase))
                    .ToList();

                if (matches.Any())
                {
                    output.Append($"{category.Key}: {string.Join(", ", matches)} | ");
                }
            }

            string finalResult = output.ToString().TrimEnd(' ', '|');
            return string.IsNullOrEmpty(finalResult) ? "General Tech" : finalResult;
        }

        public static string ExtractEmail(string text)
        {
            // Aggressive pattern for emails
            var match = Regex.Match(text, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
            return match.Success ? match.Value : "Not Found";
        }

        public static string ExtractPhone(string text)
        {
            // Aggressive pattern for UK Mobile/Landline within HTML tags
            var match = Regex.Match(text, @"(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4}))");
            return match.Success ? match.Value : "Not Found";
        }
    }
}