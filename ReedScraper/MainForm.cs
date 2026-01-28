using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReedScraper.Services;
using ReedScraper.Utils;
using ReedScraper.Models;

namespace ReedScraper
{
    public partial class MainForm : Form
    {
        private readonly DeepScraperService _deepScraper;
        private readonly EmailService _emailService;

        public MainForm()
        {
            InitializeComponent();
            _deepScraper = new DeepScraperService();
            _emailService = new EmailService();
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            btnSync.Enabled = false;
            btnSync.Text = "Searching APIs...";
            listJobs.Items.Clear();

            try
            {
                var reedService = new ReedApiService();
                var adzunaService = new AdzunaService();
                var keyword = txtKeyword.Text;

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Please enter a keyword first.");
                    return;
                }

                // 1. Fetch initial job lists from APIs
                var reedJobs = await reedService.GetJobsAsync(keyword);
                var adzunaJobs = await adzunaService.GetTodaysJobsAsync(keyword);
                var allJobs = reedJobs.Concat(adzunaJobs).ToList();

                if (allJobs.Count == 0)
                {
                    MessageBox.Show("No jobs found for today.");
                    return;
                }

                // 2. Perform Deep Scraping to get Real-Time Emails/Phones
                int count = 0;
                foreach (var job in allJobs)
                {
                    count++;
                    btnSync.Text = $"Scraping {count}/{allJobs.Count}...";

                    // Scrape the job page/description for real contact details
                    var contact = await _deepScraper.ScrapeContactInfo(job.JobDescription);
                    job.Email = contact.Email;
                    job.Phone = contact.Phone;

                    // Update UI listbox so you see results immediately
                    listJobs.Items.Add($"{job.CompanyName} | Email: {job.Email}");

                    // Small delay to prevent IP blocking
                    await Task.Delay(300);
                }

                // 3. Save CSV
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SyncedJobs.csv");
                CsvWriterHelper.Write(path, allJobs);

                // 4. Send Email Update
                btnSync.Text = "Sending Email...";
                // Change "RECIPIENT_EMAIL@example.com" to your actual email
                _emailService.SendJobUpdate("RECIPIENT_EMAIL@example.com", allJobs);

                MessageBox.Show($"Sync complete!\n- {allJobs.Count} jobs saved to CSV.\n- Email update sent.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnSync.Enabled = true;
                btnSync.Text = "Sync Today's Jobs";
            }
        }
    }
}