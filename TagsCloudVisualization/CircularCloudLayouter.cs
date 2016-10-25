using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static Point Center(this Rectangle rectangle)
        {
            return rectangle.Location + new Size(rectangle.Width / 2, rectangle.Height / 2);
        }
    }

    public class CircularCloudLayouter: IRectangleLayouter
    {
        public List<Rectangle> Rectangles { get; }
        public readonly Point CenterPoint;

        private readonly IEnumerable<Point> spiralPoints = new SpiralPoints();

        private static Point RectangleLocation(Point center, Size size)
        {
            return new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
        }

        public CircularCloudLayouter(Point center)
        {
            CenterPoint = center;
            Rectangles = new List<Rectangle>();
        }

        private bool InFreePlace(Rectangle rectangle)
        {
            return !Rectangles.Any(rectangle.IntersectsWith);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(0, 0, 0, 0);

            foreach (var nextPoint in spiralPoints)
            {
                var location = RectangleLocation(nextPoint + (Size)CenterPoint, rectangleSize);
                rectangle = new Rectangle(location, rectangleSize);

                if (InFreePlace(rectangle)) break;
            }

            rectangle = TryMoveToCenter(rectangle);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle TryMoveToCenter(Rectangle rectangle)
        {
            if (rectangle.Center() == CenterPoint)
                return rectangle;

            var movedRect = rectangle;
            var pointToCenter = CenterPoint - (Size)rectangle.Center();
            pointToCenter = new Point(Math.Sign(pointToCenter.X), Math.Sign(pointToCenter.Y));
            var cachedRect = rectangle;

            while (InFreePlace(movedRect))
            {
                cachedRect = movedRect;
                movedRect.Location += new Size(pointToCenter.X, pointToCenter.Y);
            }

            return cachedRect;
        }
    }
}
