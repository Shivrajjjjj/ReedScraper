using System.Net;
using System.Net.Mail;
using System.Text;
using ReedScraper.Models;

namespace ReedScraper.Services
{
    public class EmailService
    {
        public void SendJobUpdate(string recipientEmail, List<JobResult> jobs)
        {
            try
            {
                // Configuration - Use your email provider's SMTP settings
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("YOUR_EMAIL@gmail.com", "YOUR_APP_PASSWORD"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("YOUR_EMAIL@gmail.com"),
                    Subject = $"Today's Job Update - {DateTime.Now:yyyy-MM-dd}",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(recipientEmail);

                // Build HTML Table for the email
                StringBuilder sb = new StringBuilder();
                sb.Append("<h2>Daily Scraped Jobs Report</h2>");
                sb.Append("<table border='1' cellpadding='5' style='border-collapse: collapse;'>");
                sb.Append("<tr style='background-color: #f2f2f2;'><th>Company</th><th>Title</th><th>Email</th><th>Phone</th><th>Skills</th></tr>");

                foreach (var job in jobs)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{job.CompanyName}</td>");
                    sb.Append($"<td>{job.title}</td>");
                    sb.Append($"<td>{job.Email}</td>");
                    sb.Append($"<td>{job.Phone}</td>");
                    sb.Append($"<td>{job.Skills}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                mailMessage.Body = sb.ToString();
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Email failed to send: " + ex.Message);
            }
        }
    }
}