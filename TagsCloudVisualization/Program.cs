using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using TagsCloudVisualization.Cloud;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Graphic;

namespace TagsCloudVisualization
{
    public class Program
    {
        private static int minFontSize;
        private static int maxFontSize;
        private static int minTagWeight;
        private static int maxTagWeight;

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите название файла с изображением");
            var imageName = Console.ReadLine();

            var settings = GetSettings();

            var coordinates = settings["centerPoint"].Split(',').Select(int.Parse).ToArray();
            var centerPoint = new Point(coordinates[0], coordinates[1]);

            var size = settings["imageSize"].Split(',').Select(int.Parse).ToArray();
            var imageSize = new Size(size[0], size[1]);

            var textFile = settings["textFile"];
            var maxTagsCount = int.Parse(settings["maxTagsCount"]);

            var fontSizeRange = settings["fontSizeRange"].Split(',').Select(int.Parse).ToArray();
            minFontSize = fontSizeRange[0];
            maxFontSize = fontSizeRange[1];

            DrawStatisticsTagsCloud(centerPoint, imageSize, textFile, imageName, maxTagsCount);
        }

        public static Dictionary<string, string> GetSettings()
        {
            return File.ReadAllLines("settings.txt")
                .Select(line => line.Split(' '))
                .ToDictionary(pair => pair[0], pair => pair[1]);
        }

        public static Dictionary<string, int> GetStatistics(string textFile)
        {
            var statistics = new Dictionary<string, int>();

            var text = File.ReadAllLines(textFile);
            var words = text
                .SelectMany(line => Regex.Split(line, @"\W+"))
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower())
                .ToArray();
            var uniqueWords = words.Distinct();

            foreach (var uniqueWord in uniqueWords)
            {
                var count = words.Count(word => word == uniqueWord);
                statistics.Add(uniqueWord, count);
            }

            return statistics;
        }

        public static void DrawStatisticsTagsCloud(Point center, Size imageSize, string textFile, string imageName, int tagsCount)
        {
            var tags = new List<Tag>();
            var statistics = GetStatistics(textFile);
            var visualizator = new Visualizator(imageSize);
            var cloudLayouter = new CircularCloudLayouter(center);
            var mostPopularWords = statistics.OrderByDescending(entry => entry.Value).Take(100).ToArray();

            maxTagWeight = mostPopularWords[0].Value;
            minTagWeight = mostPopularWords[mostPopularWords.Length - 1].Value;
            
            foreach (var pair in mostPopularWords)
            {
                var tag = new Tag(pair.Key, GetFont(pair.Value));
                tag.Area = cloudLayouter.PutNextRectangle(tag.TagSize);
                tags.Add(tag);
            }

            visualizator.DrawTags(tags);
            visualizator.SaveImage(Directory.GetCurrentDirectory() + "\\" + imageName);
        }

        public static Font GetFont(int tagWeight)
        {
            double fontSize = minFontSize + (tagWeight - minTagWeight) * (maxFontSize - minFontSize) / (maxTagWeight - minTagWeight);
            return new Font("Arial", (int)Math.Ceiling(fontSize));
        }
    }
}
