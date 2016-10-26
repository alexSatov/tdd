using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter: IRectangleLayouter
    {
        public List<Rectangle> Rectangles { get; }
        public readonly Point CenterPoint;

        private readonly IEnumerable<Point> spiralPoints = new SpiralPoints();

        private static Point GetRectangleLocation(Point center, Size size)
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
                var location = GetRectangleLocation(nextPoint + (Size)CenterPoint, rectangleSize);
                rectangle = new Rectangle(location, rectangleSize);

                if (InFreePlace(rectangle)) break;
            }

            rectangle = TryMoveToCenter(rectangle);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle TryMoveToCenter(Rectangle rectangle)
        {
            if (rectangle.GetCenter() == CenterPoint)
                return rectangle;

            var movedRect = rectangle;
            var vectorToCenter = CenterPoint - (Size)rectangle.GetCenter();
            vectorToCenter = new Point(Math.Sign(vectorToCenter.X), Math.Sign(vectorToCenter.Y));
            var cachedRect = rectangle;

            while (InFreePlace(movedRect))
            {
                cachedRect = movedRect;
                movedRect.Location += new Size(vectorToCenter.X, vectorToCenter.Y);
            }

            return cachedRect;
        }
    }
}
