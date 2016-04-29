using System;
using System.Drawing;

namespace LuckyColorBot
{
    public class ImageCreator
    {
        public ImageCreator()
        {
            ColorNameFont = new Font("Meiryo", 20);

            BackgroundBrush = new SolidBrush(ColorTranslator.FromHtml("#333333"));
            ColorNameBrush = new SolidBrush(ColorTranslator.FromHtml("#EEEEEE"));
        }

        public Font ColorNameFont { get; set; }
        public Brush BackgroundBrush { get; set; }
        public Brush ColorNameBrush { get; set; }

        public Bitmap GetImage(Color color)
        {
            if (color == null) throw new ArgumentNullException();

            var image = new Bitmap(300, 260);
            var graphics = Graphics.FromImage(image);
            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(BackgroundBrush, new Rectangle(0, 0, 300, 40));
                graphics.FillRectangle(brush, new Rectangle(0, 40, 300, 220));
                graphics.DrawString(color.ToHtmlHex(), ColorNameFont, ColorNameBrush, new PointF(160, 2));
            }
            return image;
        }
    }
}