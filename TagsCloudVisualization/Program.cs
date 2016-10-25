using System.IO;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cloudLayouter = new CircularCloudLayouter(new Point(500, 500));
            var visualizator = new Visualizator(cloudLayouter, new Size(1000, 1000));
            visualizator.DrawRandomTagsCloud(50, new Size(180, 50));
            visualizator.SaveTagsCloud(Directory.GetCurrentDirectory() + "\\cloud.png");
        }
    }
}
