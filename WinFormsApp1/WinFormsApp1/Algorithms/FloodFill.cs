using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class FloodFill
    {
        // ─── Recursive Flood Fill ───────────────────────────────────────
        public static void DrawRecursive(Bitmap canvas, int x, int y,
                                         Color fillColor, Color borderColor)
        {
            if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height) return;

            Color current = canvas.GetPixel(x, y);

            if (ColorMatch(current, borderColor)) return;
            if (ColorMatch(current, fillColor))   return;

            canvas.SetPixel(x, y, fillColor);

            DrawRecursive(canvas, x + 1, y,     fillColor, borderColor);
            DrawRecursive(canvas, x - 1, y,     fillColor, borderColor);
            DrawRecursive(canvas, x,     y + 1, fillColor, borderColor);
            DrawRecursive(canvas, x,     y - 1, fillColor, borderColor);
        }

        // ─── Non-Recursive Flood Fill (stack-based) ─────────────────────
        public static void DrawNonRecursive(Bitmap canvas, int x, int y,
                                             Color fillColor, Color borderColor)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                if (p.X < 0 || p.X >= canvas.Width  ||
                    p.Y < 0 || p.Y >= canvas.Height) continue;

                Color current = canvas.GetPixel(p.X, p.Y);

                if (ColorMatch(current, borderColor)) continue;
                if (ColorMatch(current, fillColor))   continue;

                canvas.SetPixel(p.X, p.Y, fillColor);

                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X - 1, p.Y));
                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X, p.Y - 1));
            }
        }

        // ─── Helper: loose color comparison (tolerates anti-alias noise) ─
        private static bool ColorMatch(Color a, Color b, int tolerance = 10)
        {
            return Math.Abs(a.R - b.R) <= tolerance &&
                   Math.Abs(a.G - b.G) <= tolerance &&
                   Math.Abs(a.B - b.B) <= tolerance;
        }
    }
}
