# Reed & Adzuna Job Scraper 🚀

A powerful C# WinForms application that aggregates job listings from **Reed.co.uk** and **Adzuna** APIs. It features **Deep Web Scraping** to extract hidden contact details (Email/Phone) and categorizes skills using advanced Regex pattern matching.

## ✨ Features

* **Dual-API Sync:** Fetches real-time job data from Reed and Adzuna simultaneously.
* **Deep Scraper:** Visits each job page automatically to extract **Emails** and **Phone Numbers** that are usually hidden in API snippets.
* **Categorized Skill Extraction:** Uses a comprehensive dictionary to group found skills into categories like *Cloud & Infrastructure*, *Programming*, *Cybersecurity*, etc.
* **Automated CSV Export:** Generates a structured `SyncedJobs.csv` file with 7 distinct columns.
* **Email Notifications:** Sends an HTML-formatted report of today's jobs directly to your inbox.
* **Rate Limiting:** Built-in delays to ensure your IP remains safe from being blocked by job boards.

## 🏗️ Project Architecture

The project is built using **Clean Architecture** principles:

* **/Models**: Defines the `JobResult` and API Response structures.
* **/Services**: Contains the API clients (`ReedApiService`, `AdzunaService`) and the `DeepScraperService`.
* **/Utils**: Core logic for CSV writing, Email dispatch, and the `DataScanner` (Regex Engine).

## 🚀 Getting Started

### Prerequisites

* .NET 8.0 SDK or higher
* Visual Studio 2022
* A Gmail account (or other SMTP provider) for email updates.

### Configuration

1. **API Keys:** Open `ReedApiService.cs` and `AdzunaService.cs` to verify your API credentials.
2. **Email Setup:** * In `EmailService.cs`, replace `YOUR_EMAIL` and `YOUR_APP_PASSWORD`.
* *Note: For Gmail, you must generate an **App Password** in your Google Account Security settings.*


3. **Recipient:** In `MainForm.cs`, update the `SendJobUpdate` method with your target email address.

## 📊 Data Mapping

The application exports the following columns to `SyncedJobs.csv`:

| Column | Source |
| --- | --- |
| **CompanyName** | API Metadata |
| **JobDescription** | API Snippet |
| **Skills** | Regex Scanner (Categorized) |
| **ContactPerson** | Static Placeholder/Scraper |
| **Email** | Deep Web Scraper (Regex) |
| **Phone** | Deep Web Scraper (Regex) |
| **Title** | API Metadata |

## 🛠️ How it Works (The "Deep Scan")

Standard APIs protect recruiter privacy. This app overcomes that by:

1. Fetching the **Job URL**.
2. Downloading the **HTML Source** of the job page.
3. Running **Regex Patterns** to identify email strings (`[a-z]@[domain]`) and UK phone formats.
4. Parsing the text against **100+ technical keywords** to build a categorized skill profile.

