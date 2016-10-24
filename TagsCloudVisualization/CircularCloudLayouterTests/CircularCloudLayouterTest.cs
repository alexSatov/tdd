using System;
using System.Drawing;
using System.Linq;
using FluentAssert;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_should
    {
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void CreateCircularCloudLayouter()
        {
            cloudLayouter = new CircularCloudLayouter(new Point(500, 500));
        }

        [Test]
        public void putFirstRectangle_atCenter()
        {
            var rectangle = cloudLayouter.PutNextRectangle(new Size(50, 50));
            rectangle.ShouldBeEqualTo(new Rectangle(475, 475, 50, 50));
        }

        [Test]
        public void putFirstRectangle_atOffsetCenter()
        {
            cloudLayouter = new CircularCloudLayouter(new Point(25, 25));
            var rectangle = cloudLayouter.PutNextRectangle(new Size(50, 50));
            rectangle.ShouldBeEqualTo(new Rectangle(0, 0, 50, 50));
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(8, ExpectedResult = 8)]
        [TestCase(1000, ExpectedResult = 1000)]
        public int savePuttedRectangles(int rectanglesCount)
        {
            for(var i = 0; i < rectanglesCount; i++)
                cloudLayouter.PutNextRectangle(new Size(50, 20));
            return cloudLayouter.RectanglesCloud.Count;
        }

        [Test]
        public void buildCircularCloud()
        {
            for (var i = 0; i < 50; i++)
                cloudLayouter.PutNextRectangle(new Size(50, 20));

            var leftmostRect = Math.Abs(cloudLayouter.RectanglesCloud.Min(rect => rect.Location.X) - cloudLayouter.CenterPoint.X);
            var rightmostRect = Math.Abs(cloudLayouter.RectanglesCloud.Max(rect => rect.Location.X) - cloudLayouter.CenterPoint.X);
            var upmostRect = Math.Abs(cloudLayouter.RectanglesCloud.Max(rect => rect.Location.Y) - cloudLayouter.CenterPoint.Y);
            var downmostRect = Math.Abs(cloudLayouter.RectanglesCloud.Min(rect => rect.Location.Y) - cloudLayouter.CenterPoint.Y);
            
            Assert.AreEqual(leftmostRect, rightmostRect, 60);
            Assert.AreEqual(upmostRect, downmostRect, 30);
        }

        [Test]
        public void buildCloud_withNotIntersectingRectangles()
        {
            for (var i = 0; i < 30; i++)
                cloudLayouter.PutNextRectangle(new Size(15, 15));
            var intersectingRectangles = cloudLayouter.RectanglesCloud.SelectMany(
                rect => cloudLayouter.RectanglesCloud.Where(
                    otherRect => rect.IntersectsWith(otherRect) && rect != otherRect));

            intersectingRectangles.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void paintRandomCloud() // not test, method for debug painting
        {
            var v = new Visualizator(cloudLayouter, new Size(1000, 1000));
            v.DrawRandomTagsCloud(50, new Size(200, 60));
            v.SaveTagsCloud(TestContext.CurrentContext.TestDirectory + "\\cloud.png");
        }

        [TearDown]
        public void CheckOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                
                var visualizator = new Visualizator(cloudLayouter, new Size(1000, 1000));
                var dir = TestContext.CurrentContext.TestDirectory + "\\FailedTests\\";
                var testName = TestContext.CurrentContext.Test.Name;
                var path = dir + testName + "_cloud.png";
                visualizator.DrawTagsCloud();
                visualizator.SaveTagsCloud(path);
                Console.WriteLine("Tag cloud visualization saved to file " + path);
            }
        }
    }
}
