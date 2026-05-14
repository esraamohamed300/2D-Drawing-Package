using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class ModifiedBresenhamCircle
    {
        public static void Draw(Bitmap bmp, int xc, int yc, int radius, Color color)
        {
            if (radius < 0) radius = -radius;
            if (radius == 0) { SafeSet(bmp, xc, yc, color); return; }

            int x = 0, y = radius;
            int d  = 1 - radius;
            int c1 = 3;
            int c2  = 5 - 2 * radius;

            Plot8(bmp, xc, yc, x, y, color);

            while (x < y)
            {
                if (d < 0)
                {
                    d  += c1;
                    c2 += 2;
                }
                else
                {
                    d  += c2;
                    c2 += 4;
                    y--;
                }
                c1 += 2;
                x++;
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
