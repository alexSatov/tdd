using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Visualizator
    {
        private readonly CircularCloudLayouter cloudLayouter;
        private readonly Bitmap image;
        private readonly Graphics painter;
        private static readonly Pen pen = new Pen(Color.DarkOrange, 3);


        public Visualizator(CircularCloudLayouter cloudLayouter, Size imageSize)
        {
            this.cloudLayouter = cloudLayouter;
            image = new Bitmap(imageSize.Width, imageSize.Height);
            painter = Graphics.FromImage(image);
            painter.FillRectangle(new SolidBrush(Color.DarkSlateBlue), new Rectangle(new Point(0, 0), imageSize));
        }

        public void DrawRandomTagsCloud(int rectanglesCount, Size maxSize)
        {
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var size = new Size(random.Next(maxSize.Width / 2) + maxSize.Width / 2, 
                    random.Next(maxSize.Height / 2) + maxSize.Height / 2);
                var rectangle = cloudLayouter.PutNextRectangle(size);
                painter.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawTagsCloud()
        {
            painter.DrawRectangles(pen, cloudLayouter.RectanglesCloud.ToArray());
        }

        public void SaveTagsCloud(string filename)
        {
            image.Save(filename);
        }
    }
}
