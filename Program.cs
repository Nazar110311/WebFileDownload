using System;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WebDownload webDownload = new WebDownload();
            webDownload.DownloadWebFile(@"https://download.virtualbox.org/virtualbox/6.1.38/VirtualBox-6.1.38-153438-Win.exe", @"C:\Users\nazar\VirtualBox.exe");
            for(int i = 0; i > -1; i++)
            {
                Console.WriteLine($"{webDownload.Percentages}% {webDownload.SizeFile}МБ {webDownload.FileDownloadBytes}");
                if(webDownload.Percentages == "100")
                {
                    break;
                }
                System.Threading.Thread.Sleep(900);
            }
            Console.ReadLine();
        }
    }
    public class WebDownload
    {
        public string Percentages;
        public string FileDownloadBytes;
        public string SpeedBytes;
        public string SizeFile;

        public void DownloadWebFile(string url, string savepath)
        {
            using (WebClient wc = new WebClient())
            {
                wc.OpenRead(url);
                string size = (Convert.ToDouble(wc.ResponseHeaders["Content-Length"]) / 1048576).ToString("#.#");

                wc.DownloadProgressChanged += (s, e) =>
                {
                    SizeFile = size;
                    FileDownloadBytes = ((do) e.BytesReceived / 1048576).ToString("#.#");

                    Percentages = e.ProgressPercentage.ToString();
                };
                wc.DownloadFileAsync(new Uri(url), savepath);
            }
        }
    }
} 