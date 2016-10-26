using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DrawRectanglesCloud();
            DrawTagsCloud();
        }

        public static void DrawTagsCloud()
        {
            var random = new Random();
            var tags = new List<Tag>();
            var cloudLayouter = new CircularCloudLayouter(new Point(500, 500));
            var visualizator = new Visualizator(new Size(1000, 1000));
            var words = ("#lightsout #amazing #jeep #blue #igaddict #lovehim #rainydays #ornaments #wine #decorations " +
                         "#2012 #buyankin #creative #carols #iphonephotography #downtown #instagood #lakers #editoftheday #follow " +
                         "#together #realshit #beauty #shoutoutback #christmas #highschool #happiness #likes4likes #styles " +
                         "#early #yeah #skies #art #adorable #nephew #beautiful #newyears2013 #friends #bird #blue #instagood " +
                         "#santa #yummy #girlfriend #picoftheday #nature #buyankin #sis #goodtimes #horseshow #dog #fall #commentbackteam " +
                         "#macrogardener #want #cute #illustration #favoritesong").Split(' ');


            foreach (var word in words)
            {
                var tag = new Tag(word, new Font("Arial", random.Next(10, 30)));
                tag.Area = cloudLayouter.PutNextRectangle(tag.TagSize);
                tags.Add(tag);
            }

            visualizator.DrawTags(tags);
            visualizator.SaveImage(Directory.GetCurrentDirectory() + "\\cloud.png");
        }

        public static void DrawRectanglesCloud()
        {
            var random = new Random();
            var cloudLayouter = new CircularCloudLayouter(new Point(500, 500));
            var visualizator = new Visualizator(new Size(1000, 1000));
            
            for (var i = 0; i < 50; i++)
            {
                var size = new Size(random.Next(60, 120), random.Next(15, 30));
                cloudLayouter.PutNextRectangle(size);
            }

            visualizator.DrawRectangles(cloudLayouter.Rectangles);
            visualizator.SaveImage(Directory.GetCurrentDirectory() + "\\cloud.png");
        }
    }
}
