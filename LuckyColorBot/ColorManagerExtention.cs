using System;
using System.Drawing;

namespace LuckyColorBot
{
    public static class ColorManagerExtention
    {
        public static string ToHtmlHex(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        public static string ToHex(this Color color)
        {
            return ColorTranslator.ToHtml(color).Substring(1);
        }

        public static Color GetComplementaryColor(this Color color)
        {
            var r = color.R;
            var g = color.G;
            var b = color.B;

            var max = Math.Max(r, Math.Max(g, b));
            var min = Math.Min(r, Math.Min(g, b));
            var sum = max + min;

            r = (byte) (sum - r);
            g = (byte) (sum - g);
            b = (byte) (sum - b);

            return Color.FromArgb(r, g, b);
        }
    }
}