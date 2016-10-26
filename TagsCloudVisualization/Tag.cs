using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public string Text { get; }
        public Rectangle Area { get; set; }
        public Size TagSize { get; }
        public Font TagFont { get; }

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
