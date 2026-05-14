using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class CircleQuarterFill
    {
        // ─── Determine which quarter a point falls in relative to center ─
        // Quarter 1: +x, -y (top-right)
        // Quarter 2: -x, -y (top-left)
        // Quarter 3: -x, +y (bottom-left)
        // Quarter 4: +x, +y (bottom-right)
        public static int GetQuarter(Point center, Point p)
        {
            int dx = p.X - center.X;
            int dy = p.Y - center.Y;

            if (dx >= 0 && dy <= 0) return 1;
            if (dx <= 0 && dy <= 0) return 2;
            if (dx <= 0 && dy >= 0) return 3;
            return 4;
        }

        // ─── Fill quarter using concentric circles (pixel-by-pixel) ─────
        public static void FillQuarter(Bitmap canvas, int xc, int yc,
                                        int radius, int quarter, Color color)
        {
            for (int r = 1; r <= radius; r++)
            {
                int x = 0, y = r;
                int d  = 1 - r;
                int c1 = 3, c2 = 5 - 2 * r;

                DrawQuarterPoint(canvas, xc, yc, x, y, quarter, color);

                while (x < y)
                {
                    if (d < 0) { d += c1; c2 += 2; }
                    else       { d += c2; c2 += 4; y--; }
                    c1 += 2; x++;
                    DrawQuarterPoint(canvas, xc, yc, x, y, quarter, color);
                }
            }
        }

        // ─── Fill quarter using horizontal scan lines (faster) ───────────
        public static void FillQuarterLines(Bitmap canvas, int xc, int yc,
                                             int radius, int quarter, Color color)
        {
            int x = 0, y = radius;
            int d = 1 - radius;

            while (x <= y)
            {
                DrawQuarterLines(canvas, xc, yc, x, y, quarter, color);

                if (d < 0) d += 2 * x + 3;
                else       { d += 2 * (x - y) + 5; y--; }
                x++;
            }
        }

        // ─── Helpers ─────────────────────────────────────────────────────
        private static void DrawQuarterPoint(Bitmap canvas, int xc, int yc,
                                              int a, int b, int quarter, Color color)
        {
            switch (quarter)
            {
                case 1: SetSafe(canvas, xc + a, yc - b, color);
                        SetSafe(canvas, xc + b, yc - a, color); break;
                case 2: SetSafe(canvas, xc - a, yc - b, color);
                        SetSafe(canvas, xc - b, yc - a, color); break;
                case 3: SetSafe(canvas, xc - a, yc + b, color);
                        SetSafe(canvas, xc - b, yc + a, color); break;
                case 4: SetSafe(canvas, xc + a, yc + b, color);
                        SetSafe(canvas, xc + b, yc + a, color); break;
            }
        }

        private static void DrawQuarterLines(Bitmap canvas, int xc, int yc,
                                              int x, int y, int quarter, Color color)
        {
            switch (quarter)
            {
                case 1: // top-right
                    HLine(canvas, xc,     yc - y, xc + x, color);
                    HLine(canvas, xc,     yc - x, xc + y, color);
                    break;
                case 2: // top-left
                    HLine(canvas, xc - x, yc - y, xc,     color);
                    HLine(canvas, xc - y, yc - x, xc,     color);
                    break;
                case 3: // bottom-left
                    HLine(canvas, xc - x, yc + y, xc,     color);
                    HLine(canvas, xc - y, yc + x, xc,     color);
                    break;
                case 4: // bottom-right
                    HLine(canvas, xc,     yc + y, xc + x, color);
                    HLine(canvas, xc,     yc + x, xc + y, color);
                    break;
            }
        }

        private static void HLine(Bitmap canvas, int x1, int y, int x2, Color color)
        {
            if (x1 > x2) { int t = x1; x1 = x2; x2 = t; }
            for (int x = x1; x <= x2; x++)
                SetSafe(canvas, x, y, color);
        }

        private static void SetSafe(Bitmap canvas, int x, int y, Color color)
        {
            if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
                canvas.SetPixel(x, y, color);
        }
    }
}
