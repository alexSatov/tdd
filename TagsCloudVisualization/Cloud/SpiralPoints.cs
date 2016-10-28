using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;


namespace TagsCloudVisualization
{
    public class SpiralPoints : IEnumerable<Point>
    {
        private static Point GetNextPoint(double angleInRadians)
        {
            var x = (int)(angleInRadians * Math.Cos(angleInRadians) * Math.PI / 8);
            var y = (int)(angleInRadians * Math.Sin(angleInRadians) * Math.PI / 8);
            return new Point(x, y);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            var angle = 0.0;
            while (true)
            {
                yield return GetNextPoint(angle);
                angle += 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
