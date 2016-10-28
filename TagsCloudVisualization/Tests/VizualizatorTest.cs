using System;
using FluentAssert;
using System.Drawing;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    class Vizualizator_should
    {
        [Test]
        public void drawingOnlyPositivePoints()
        {
            var cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
            cloudLayouter.PutNextRectangle(new Size(50, 50));
            var visualizator = new Visualizator(new Size(50, 50));
            visualizator.DrawRectangles(cloudLayouter.Rectangles);
            for (var i = 0; i < 50; i++)
            {
                if (i < 24)
                    visualizator.Image.GetPixel(i, 24).ShouldBeEqualTo(Color.FromArgb(255, 255, 140, 0));
                else
                    visualizator.Image.GetPixel(24, Math.Abs(i - 24)).ShouldBeEqualTo(Color.FromArgb(255, 255, 140, 0));
            }
        }
    }
}
