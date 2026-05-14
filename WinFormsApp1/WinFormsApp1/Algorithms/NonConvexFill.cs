using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class NonConvexFill
    {
        private const int MAXENTRIES    = 800;
        private const int MAX_INTERSECT = 50;

        public static void Draw(Bitmap canvas, Point[] points, Color color)
        {
            if (points == null || points.Length < 3) return;

            // Scan table: each row holds x-intersection list
            int[,] xs    = new int[MAXENTRIES, MAX_INTERSECT];
            int[]  count = new int[MAXENTRIES];

            int n = points.Length;
            for (int i = 0; i < n; i++)
            {
                Point v1 = points[i];
                Point v2 = points[(i + 1) % n];

                // Draw outline in red
                DDA.Draw(canvas, v1.X, v1.Y, v2.X, v2.Y, Color.Red);

                // Collect intersections
                ScanEdge(v1, v2, xs, count);
            }

            // Fill using even-odd rule
            for (int y = 0; y < MAXENTRIES; y++)
            {
                int cnt = count[y];
                if (cnt < 2) continue;

                // Sort intersections for this row
                int[] row = new int[cnt];
                for (int k = 0; k < cnt; k++) row[k] = xs[y, k];
                Array.Sort(row);

                // Paint between pairs 0-1, 2-3, ...
                for (int i = 0; i + 1 < cnt; i += 2)
                {
                    for (int x = row[i]; x <= row[i + 1]; x++)
                    {
                        if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
                            canvas.SetPixel(x, y, color);
                    }
                }
            }
        }

        private static void ScanEdge(Point v1, Point v2, int[,] xs, int[] count)
        {
            if (v1.Y == v2.Y) return;
            if (v1.Y > v2.Y) { Point tmp = v1; v1 = v2; v2 = tmp; }

            double slope = (double)(v2.X - v1.X) / (v2.Y - v1.Y);
            double x     = v1.X;

            for (int y = v1.Y; y < v2.Y; y++)           // top-left rule: < not <=
            {
                if (y >= 0 && y < MAXENTRIES)
                {
                    int idx = count[y];
                    if (idx < MAX_INTERSECT)
                    {
                        xs[y, idx] = (int)Math.Round(x);
                        count[y]++;
                    }
                }
                x += slope;
            }
        }
    }
}
