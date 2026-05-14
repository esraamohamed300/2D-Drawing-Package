using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class PolarEllipse
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int a, int b, Color color)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            if (a == 0 && b == 0) { SafeSet(bmp, xc, yc, color); return; }

            double dtheta = 1.0 / Math.Max(a, b);

            for (double theta = 0; theta <= 2 * Math.PI; theta += dtheta)
            {
                int x = (int)Math.Round(a * Math.Cos(theta));
                int y = (int)Math.Round(b * Math.Sin(theta));
                Plot4(bmp, xc, yc, x, y, color);
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
