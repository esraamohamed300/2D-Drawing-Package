using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class Bresenham
    {
        public static void Draw(Bitmap bmp, int xs, int ys, int xe, int ye, Color color)
        {
            int dx = Math.Abs(xe - xs);
            int dy = Math.Abs(ye - ys);

            int xinc = xs < xe ? 1 : -1;
            int yinc = ys < ye ? 1 : -1;

            int x = xs;
            int y = ys;

            bmp.SetPixel(x, y, color);

            if (dx >= dy)
            {
                int d = dx - 2 * dy;
                int c1 = 2 * (dx - dy);
                int c2 = -2 * dy;

                while (x != xe)
                {
                    if (d < 0)
                    {
                        d += c1;
                        y += yinc;
                    }
                    else
                    {
                        d += c2;
                    }

                    x += xinc;
                    bmp.SetPixel(x, y, color);
                }
            }
            else
            {
                int d = 2 * dx - dy;
                int c1 = 2 * (dx - dy);
                int c2 = 2 * dx;

                while (y != ye)
                {
                    if (d > 0)
                    {
                        d += c1;
                        x += xinc;
                    }
                    else
                    {
                        d += c2;
                    }

                    y += yinc;
                    bmp.SetPixel(x, y, color);
                }
            }
        }
    }
}