using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async_await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            RunDownloadSync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressReport> progress = new Progress<ProgressReport>();
            progress.ProgressChanged += Progress_ProgressChanged;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                await RunDownloadAsync(progress, cts.Token);
            }
            catch (OperationCanceledException)
            {
                resultsWindow.Text += $"Async download cancelled { Environment.NewLine }";
            }
            

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private void Progress_ProgressChanged(object sender, ProgressReport e)
        {
            progress_bar.Value = e.PercentageComplete;

            foreach (var item in e.SitesDownloaded)
            {
                ReportWebsiteInfo(item);
            }
        }

        private async void executeAsyncParallel_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await RunDownloadParallelAsyncV2();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: { elapsedMs }";
        }

        private List<string> PrepData()
        {
            List<string> output = new List<string>();

            resultsWindow.Text = "";

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");

            return output;
        }

        private void RunDownloadSync()
        {
            List<string> websites = PrepData();

            foreach (string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                ReportWebsiteInfo(results);
            }
        }

        private async Task RunDownloadAsync(IProgress<ProgressReport> progress, CancellationToken token)
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> sites = new List<WebsiteDataModel>();

            ProgressReport report = new ProgressReport();

            foreach (string site in websites)
            {
                WebsiteDataModel result = await Task.Run(() => DownloadWebsite(site));
                sites.Add(result);
                ReportWebsiteInfo(result);

                token.ThrowIfCancellationRequested();

                report.SitesDownloaded = sites;
                report.PercentageComplete = (sites.Count * 100) / websites.Count; 
                progress.Report(report);
            }
        }

        private async Task RunDownloadParallelAsyncV1()
        {
            List<string> websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                tasks.Add(DownloadWebsiteAsync(site));
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                ReportWebsiteInfo(item);
            }
        }

        private async Task RunDownloadParallelAsyncV2()
        {
            List<string> websites = PrepData();
            List<WebsiteDataModel> websiteList = new List<WebsiteDataModel>();

            await Task.Run(() =>
            {
                Parallel.ForEach<string>(websites, (site) =>
                {
                    WebsiteDataModel result = DownloadWebsite(site);
                    websiteList.Add(result);
                });
            });

            foreach (var item in websiteList)
            {
                ReportWebsiteInfo(item);
            }
        }

        private WebsiteDataModel DownloadWebsite(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        private async Task<WebsiteDataModel> DownloadWebsiteAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            resultsWindow.Text += $"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long.{ Environment.NewLine }";
        }

        private void executeCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
