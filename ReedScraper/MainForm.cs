using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ReedScraper.Apify;
using ReedScraper.Utils;

namespace ReedScraper
{
    public class MainForm : Form
    {
        private TextBox txtUrl;
        private Button btnSync;

        public MainForm()
        {
            // Form properties
            this.Text = "ReedScraper";
            this.Size = new Size(500, 160);

            // TextBox
            txtUrl = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(440, 30),
                PlaceholderText = "Enter Reed.co.uk job URL"
            };
            this.Controls.Add(txtUrl);

            // Button
            btnSync = new Button
            {
                Location = new Point(20, 65),
                Size = new Size(150, 35),
                Text = "Sync Data"
            };
            btnSync.Click += BtnSync_Click;
            this.Controls.Add(btnSync);
        }

        private async void BtnSync_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("Please enter a valid URL");
                return;
            }

            btnSync.Enabled = false;

            try
            {
                var apify = new ApifyClient();
                var rawData = await apify.RunActorAsync(txtUrl.Text);

                var jobs = rawData.Select(JobMapper.Map).ToList();

                CsvWriterHelper.WriteToCsv(jobs);

                MessageBox.Show("CSV file generated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnSync.Enabled = true;
            }
        }
    }
}
