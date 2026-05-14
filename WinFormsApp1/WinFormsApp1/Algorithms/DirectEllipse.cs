using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class DirectEllipse
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int a, int b, Color color)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            if (a == 0 && b == 0) { SafeSet(bmp, xc, yc, color); return; }

            // First region: iterate over x
            for (double x = 0; x <= a; x++)
            {
                double y = b * Math.Sqrt(1.0 - (x * x) / (a * a));
                Plot4(bmp, xc, yc, (int)Math.Round(x), (int)Math.Round(y), color);
            }

            // Second region: iterate over y
            for (double y = 0; y <= b; y++)
            {
                double x = a * Math.Sqrt(1.0 - (y * y) / (b * b));
                Plot4(bmp, xc, yc, (int)Math.Round(x), (int)Math.Round(y), color);
            }
        }

        static void Plot4(Bitmap bmp, int xc, int yc, int x, int y, Color color)
        {
            SafeSet(bmp, xc + x, yc + y, color);
            SafeSet(bmp, xc - x, yc + y, color);
            SafeSet(bmp, xc + x, yc - y, color);
            SafeSet(bmp, xc - x, yc - y, color);
        }

        static void SafeSet(Bitmap bmp, int x, int y, Color color)
        {
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                bmp.SetPixel(x, y, color);
        }
    }
}
