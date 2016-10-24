using System;
using System.Linq;
using System.Drawing;
using System.Collections;
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

    public class CircularCloudLayouter
    {
        public readonly List<Rectangle> RectanglesCloud = new List<Rectangle>();
        public readonly Point CenterPoint;

        private readonly IEnumerator<Point> spiralPoints = new SpiralPoints().GetEnumerator();
        private static readonly Func<Point, Size, Point> RectangleLocation = 
            (center, size) => new Point(center.X - size.Width / 2, center.Y - size.Height / 2);

        public CircularCloudLayouter(Point center) { CenterPoint = center; }

        private bool InFreePlace(Rectangle rectangle) { return !RectanglesCloud.Any(rectangle.IntersectsWith); }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            while (true)
            {
                var nextPoint = spiralPoints.Current;
                spiralPoints.MoveNext();

                var location = RectangleLocation(nextPoint + (Size)CenterPoint, rectangleSize);
                rectangle = new Rectangle(location, rectangleSize);

                if (!InFreePlace(rectangle)) continue;

                rectangle = TryMoveToCenter(rectangle);
                RectanglesCloud.Add(rectangle);
                break;
            }
            return rectangle;
        }

        private Rectangle TryMoveToCenter(Rectangle rectangle)
        {
            if (rectangle.Center() == CenterPoint)
                return rectangle;

            var movedRect = rectangle;
            var pointToCenter = CenterPoint - (Size)rectangle.Center();
            pointToCenter = new Point(Math.Sign(pointToCenter.X), Math.Sign(pointToCenter.Y));
            Rectangle cacheRect;

            while (true)
            {
                cacheRect = movedRect;
                movedRect.Location += new Size(pointToCenter.X, pointToCenter.Y);
                if (!InFreePlace(movedRect)) break;
            }
            return cacheRect;
        }

    }

    public class SpiralPoints : IEnumerable<Point>
    {
        private static readonly Func<double, Point> Spiral = angleInRadians =>
        {
            var x = (int) (angleInRadians * Math.Cos(angleInRadians) * Math.PI/8);
            var y = (int) (angleInRadians * Math.Sin(angleInRadians) * Math.PI/8);
            return new Point(x, y);
        };

        public IEnumerator<Point> GetEnumerator()
        {
            var angle = 0.0;
            while (true)
            {
                yield return Spiral(angle);
                angle += 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
