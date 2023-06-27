using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace Projekt
{
    public static class GraphManager
    {
        private static Image DownloadImage(string imageUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(imageUrl);
                using (MemoryStream stream = new MemoryStream(data))
                {
                    return Image.FromStream(stream);
                }
            }
        }

        private static Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        public static void GenerateGraph(string firstValueName, string secondValueName, double[] firstValues, double[] secondValues, int[] years, PictureBox graphBox)
        {
            if (firstValues == null || secondValues == null || years == null)
                return;
            
            if (firstValues.Length != secondValues.Length || firstValues.Length != years.Length || firstValues.Length == 0)
                return;

            Console.WriteLine(string.Join(", ", firstValues));
            Console.WriteLine(string.Join(", ", secondValues));
            Console.WriteLine(string.Join(", ", years));
            // Start url string
            string baseUrl = "https://quickchart.io/chart?c=";

            var jsonData = new
            {
                type = "line",
                data = new
                {
                    labels = years.Select(n => n.ToString()).ToArray(),
            datasets = new[]
        {
            new
            {
                label = firstValueName,
                borderColor = "rgb(0, 0, 255)",
                backgroundColor = "rgb(255, 99, 132)",
                fill = false,
                data = firstValues,
                yAxisID = "y"
            },
            new
            {
                label = secondValueName,
                borderColor = "rgb(255, 0, 0)",
                backgroundColor = "rgb(255, 99, 132)",
                fill = false,
                data = secondValues,
                yAxisID = "y1"
            }
        }
                },
                options = new
                {
                    stacked = false,
                    title = new
                    {
                        display = true,
                        text = "Wykres "
                    },
                    scales = new
                    {
                        yAxes = new[]
            {
                new
                {
                    id = "y",
                    type = "linear",
                    display = true,
                    position = "left"
                },
                new
                {
                    id = "y1",
                    type = "linear",
                    display = true,
                    position = "right",
                }
            }
                    }
                }
            };

            string jsonString = JsonConvert.SerializeObject(jsonData);

            Image image = DownloadImage(baseUrl + jsonString);
            Image resizedImage = ResizeImage(image, graphBox.Width, graphBox.Height);

            graphBox.Image = resizedImage;
        }
    }
}
