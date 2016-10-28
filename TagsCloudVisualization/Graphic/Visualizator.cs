using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization.Graphic
{
    public class Visualizator
    {
        public Pen Pen { get; set; }
        public Bitmap Image { get; private set; }
        public Color BackgroundColor { get; set; }
        public Graphics Painter { get; private set; }

        public Visualizator(Size imageSize)
        {
            Pen = new Pen(Color.DarkOrange, 3);
            BackgroundColor = Color.DarkSlateBlue;
            CreateNewImage(imageSize);
        }

        public void CreateNewImage(Size imageSize)
        {
            Image = new Bitmap(imageSize.Width, imageSize.Height);
            Painter = Graphics.FromImage(Image);
            Painter.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(new Point(0, 0), imageSize));
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            Painter.DrawRectangles(Pen, rectangles.ToArray());
        }

        public void DrawTags(IEnumerable<Tag> tags)
        {
            var brush = new SolidBrush(Pen.Color);
            foreach (var tag in tags)
            {
                Painter.DrawString(tag.Text, tag.TagFont, brush, tag.Area.Location);
            }
        }

        public void SaveImage(string filename)
        {
            Image.Save(filename);
        }
    }
}

