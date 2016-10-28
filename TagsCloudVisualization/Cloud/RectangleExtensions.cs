using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            return rectangle.Location + new Size(rectangle.Width / 2, rectangle.Height / 2);
        }
    }
}
