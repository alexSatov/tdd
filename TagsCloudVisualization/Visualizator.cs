using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Visualizator
    {
        private readonly IRectangleLayouter rectLayouter;
        private readonly Bitmap image;
        private readonly Graphics painter;
        private static readonly Pen pen = new Pen(Color.DarkOrange, 3);


        public Visualizator(IRectangleLayouter rectLayouter, Size imageSize)
        {
            this.rectLayouter = rectLayouter;
            image = new Bitmap(imageSize.Width, imageSize.Height);
            painter = Graphics.FromImage(image);
            painter.FillRectangle(new SolidBrush(Color.DarkSlateBlue), new Rectangle(new Point(0, 0), imageSize));
        }

        public void DrawRandomTagsCloud(int rectanglesCount, Size maxSize)
        {
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var size = new Size(random.Next(maxSize.Width / 2, maxSize.Width),
                    random.Next(maxSize.Height / 2, maxSize.Height));
                var rectangle = rectLayouter.PutNextRectangle(size);
                painter.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawTagsCloud()
        {
            painter.DrawRectangles(pen, rectLayouter.Rectangles.ToArray());
        }

        public void SaveTagsCloud(string filename)
        {
            image.Save(filename);
        }
    }
}

