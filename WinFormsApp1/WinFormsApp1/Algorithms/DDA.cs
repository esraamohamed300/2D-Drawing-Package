using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class DDA
    {
        public static void Draw(Bitmap bmp, int xs, int ys, int xe, int ye, Color color)
        {
            int dx = xe - xs;
            int dy = ye - ys;

            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            if (steps == 0)
            {
                bmp.SetPixel(xs, ys, color);
                return;
            }

            float xInc = dx / (float)steps;
            float yInc = dy / (float)steps;

            float x = xs;
            float y = ys;

            for (int i = 0; i <= steps; i++)
            {
                bmp.SetPixel((int)Math.Round(x), (int)Math.Round(y), color);
                x += xInc;
                y += yInc;
            }
        }
    }
}