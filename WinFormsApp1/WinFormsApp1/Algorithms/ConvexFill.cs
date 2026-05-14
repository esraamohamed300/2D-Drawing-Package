using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class ConvexFill
    {
        private const int MAXENTRIES = 800;

        public static void Draw(Bitmap canvas, Point[] points, Color color)
        {
            if (points == null || points.Length < 3) return;

            int[] xmin = new int[MAXENTRIES];
            int[] xmax = new int[MAXENTRIES];

            for (int i = 0; i < MAXENTRIES; i++)
            {
                xmin[i] = int.MaxValue;
                xmax[i] = int.MinValue;
            }

            // Draw outline + fill scan table
            int n = points.Length;
            for (int i = 0; i < n; i++)
            {
                Point v1 = points[i];
                Point v2 = points[(i + 1) % n];

                // Draw edge
                DDA.Draw(canvas, v1.X, v1.Y, v2.X, v2.Y, Color.Red);

                // Scan edge into table
                ScanEdge(v1, v2, xmin, xmax);
            }

            // Fill horizontal spans
            for (int y = 0; y < MAXENTRIES; y++)
            {
                if (xmin[y] < xmax[y])
                {
                    for (int x = xmin[y]; x <= xmax[y]; x++)
                    {
                        if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
                            canvas.SetPixel(x, y, color);
                    }
                }
            }
        }

        private static void ScanEdge(Point v1, Point v2, int[] xmin, int[] xmax)
        {
            if (v1.Y == v2.Y) return;
            if (v1.Y > v2.Y) { Point tmp = v1; v1 = v2; v2 = tmp; }

            double slope = (double)(v2.X - v1.X) / (v2.Y - v1.Y);
            double x = v1.X;

            for (int y = v1.Y; y < v2.Y; y++)
            {
                if (y >= 0 && y < MAXENTRIES)
                {
                    int xi = (int)Math.Ceiling(x);
                    if (xi < xmin[y]) xmin[y] = xi;

                    int xa = (int)Math.Floor(x);
                    if (xa > xmax[y]) xmax[y] = xa;
                }
                x += slope;
            }
        }
    }
}
