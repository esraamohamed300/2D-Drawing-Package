using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class MidPointEllipse
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int a, int b, Color color)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            if (a == 0 && b == 0) { SafeSet(bmp, xc, yc, color); return; }

            float a2 = a * a, b2 = b * b;
            float x = 0, y = b;
            float dx = 2 * b2 * x;
            float dy = 2 * a2 * y;

            // Region 1
            float d1 = b2 - a2 * b + 0.25f * a2;
            while (dx < dy)
            {
                Plot4(bmp, xc, yc, (int)x, (int)y, color);
                if (d1 < 0)
                {
                    x++;
                    dx += 2 * b2;
                    d1 += dx + b2;
                }
                else
                {
                    x++;
                    y--;
                    dx += 2 * b2;
                    dy -= 2 * a2;
                    d1 += dx - dy + b2;
                }
            }

            // Region 2
            float d2 = b2 * ((x + 0.5f) * (x + 0.5f))
                     + a2 * ((y - 1)    * (y - 1))
                     - a2 * b2;
            while (y >= 0)
            {
                Plot4(bmp, xc, yc, (int)x, (int)y, color);
                if (d2 > 0)
                {
                    y--;
                    dy -= 2 * a2;
                    d2 += a2 - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx += 2 * b2;
                    dy -= 2 * a2;
                    d2 += dx - dy + a2;
                }
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
