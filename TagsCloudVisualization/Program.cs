using System;
using System.IO;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cloudLayouter = new CircularCloudLayouter(new Point(500, 500));
            var visualizator = new Visualizator(new Size(1000, 1000));

            var random = new Random();
            for (var i = 0; i < 50; i++)
            {
                var size = new Size(random.Next(60, 120), random.Next(15, 30));
                cloudLayouter.PutNextRectangle(size);
            }

            visualizator.DrawRectangles(cloudLayouter.Rectangles);
            visualizator.SaveImage(Directory.GetCurrentDirectory() + "\\cloud.png");
        }
    }
}
