using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IRectangleLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        List<Rectangle> Rectangles { get; }
    }
}
