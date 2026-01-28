namespace ReedScraper
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtKeyword;
        private Button btnSync;
        private ListBox listJobs;

        private void InitializeComponent()
        {
            txtKeyword = new TextBox();
            btnSync = new Button();
            listJobs = new ListBox();

            txtKeyword.PlaceholderText = "Enter job keyword (e.g. .NET)";
            txtKeyword.Location = new Point(20, 20);
            txtKeyword.Size = new Size(280, 30);

            btnSync.Text = "Sync Today’s Jobs";
            btnSync.Location = new Point(310, 20);
            btnSync.Size = new Size(170, 30);
            btnSync.Click += btnSync_Click;

            listJobs.Location = new Point(20, 70);
            listJobs.Size = new Size(460, 260);

            Controls.Add(txtKeyword);
            Controls.Add(btnSync);
            Controls.Add(listJobs);

            Text = "ReedScraper – Adzuna API";
            Size = new Size(520, 380);
        }
    }
}
