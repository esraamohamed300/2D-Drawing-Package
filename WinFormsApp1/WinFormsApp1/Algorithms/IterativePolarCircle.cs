using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class IterativePolarCircle
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int radius, Color color)
        {
            if (radius < 0) radius = -radius;
            if (radius == 0) { SafeSet(bmp, xc, yc, color); return; }

            double x = 0, y = radius;
            double dtheta = 1.0 / radius;
            double cosTheta = Math.Cos(dtheta);
            double sinTheta = Math.Sin(dtheta);

            Plot8(bmp, xc, yc, (int)Math.Round(x), (int)Math.Round(y), color);

            while (x < y)
            {
                double x1 = x * cosTheta + y * sinTheta;
                y = y * cosTheta - x * sinTheta;
                x = x1;
                Plot8(bmp, xc, yc, (int)Math.Round(x), (int)Math.Round(y), color);
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
