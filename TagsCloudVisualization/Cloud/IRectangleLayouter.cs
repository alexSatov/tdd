using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization.Cloud
{
    public interface IRectangleLayouter
    {
        List<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
