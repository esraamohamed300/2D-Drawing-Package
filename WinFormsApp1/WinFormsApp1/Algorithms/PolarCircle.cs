using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class PolarCircle
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int radius, Color color)
        {
            if (radius < 0) radius = -radius;
            if (radius == 0) { SafeSet(bmp, xc, yc, color); return; }

            int x = 0, y = radius;
            double theta = 90.0 * (Math.PI / 180.0);
            double dtheta = 1.0 / radius;

            Plot8(bmp, xc, yc, x, y, color);

            while (x < y)
            {
                theta -= dtheta;
                x = (int)Math.Round(radius * Math.Cos(theta));
                y = (int)Math.Round(radius * Math.Sin(theta));
                Plot8(bmp, xc, yc, x, y, color);
            }
        }

        static void Plot8(Bitmap bmp, int xc, int yc, int a, int b, Color color)
        {
            SafeSet(bmp, xc + a, yc + b, color);
            SafeSet(bmp, xc - a, yc + b, color);
            SafeSet(bmp, xc + a, yc - b, color);
            SafeSet(bmp, xc - a, yc - b, color);
            SafeSet(bmp, xc + b, yc + a, color);
            SafeSet(bmp, xc - b, yc + a, color);
            SafeSet(bmp, xc + b, yc - a, color);
            SafeSet(bmp, xc - b, yc - a, color);
        }

        static void SafeSet(Bitmap bmp, int x, int y, Color color)
        {
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                bmp.SetPixel(x, y, color);
        }
    }
}
