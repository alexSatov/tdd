using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization.Graphic
{
    public class Tag
    {
        public string Text { get; }
        public Size TagSize { get; }
        public Font TagFont { get; }
        public Rectangle Area { get; set; }

        public Tag(string text, Font font)
        {
            Text = text;
            TagFont = font;
            TagSize = GetSize();
        }

        private Size GetSize()
        {
            var size = TextRenderer.MeasureText(Text, TagFont);
            return size;
        }
    }
}
