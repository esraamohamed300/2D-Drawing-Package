using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class HermiteBezierFill
    {
        // ─── Fill a square with vertical Hermite curves ──────────────────
        public static void FillSquareHermiteVertical(Bitmap canvas,
                                                      int left, int top,
                                                      int size, Color color)
        {
            int right  = left + size;
            int bottom = top  + size;

            for (int x = left; x <= right; x += 2)
            {
                DrawHermiteCurve(canvas,
                    new PointF(x, top),    new PointF(size * 0.8f, 0),
                    new PointF(x, bottom), new PointF(size * 0.8f, 0),
                    100,
                    color,
                    left, top, right, bottom);   // clip rect
            }
        }

        // ─── Fill a rectangle with horizontal Bezier curves ─────────────
        public static void FillRectangleBezierHorizontal(Bitmap canvas,
                                                          int left, int top,
                                                          int width, int height,
                                                          Color color)
        {
            int right  = left + width;
            int bottom = top  + height;

            for (int y = top; y <= bottom; y += 2)
            {
                PointF p0  = new PointF(left,  y);
                PointF p3  = new PointF(right, y);
                PointF cp1 = new PointF(left  + width  * 0.25f, y - height * 0.4f);
                PointF cp2 = new PointF(left  + width  * 0.75f, y + height * 0.4f);

                DrawBezierCurve(canvas, p0, cp1, cp2, p3, 100, color,
                                left, top, right, bottom);
            }
        }

        // ─── Core: Hermite curve on Bitmap ───────────────────────────────
        private static void DrawHermiteCurve(Bitmap canvas,
                                              PointF p0, PointF t0,
                                              PointF p1, PointF t1,
                                              int steps, Color color,
                                              int clipL, int clipT,
                                              int clipR, int clipB)
        {
            float[] cx = HermiteCoeff(p0.X, t0.X, p1.X, t1.X);
            float[] cy = HermiteCoeff(p0.Y, t0.Y, p1.Y, t1.Y);

            double dt = 1.0 / (steps - 1);
            int prevX = int.MinValue, prevY = int.MinValue;

            for (int i = 0; i < steps; i++)
            {
                double t  = i * dt;
                double t2 = t * t, t3 = t2 * t;

                int x = (int)Math.Round(cx[0]*t3 + cx[1]*t2 + cx[2]*t + cx[3]);
                int y = (int)Math.Round(cy[0]*t3 + cy[1]*t2 + cy[2]*t + cy[3]);

                // Connect with DDA segment so no gaps appear
                if (prevX != int.MinValue)
                    DrawSegmentClipped(canvas, prevX, prevY, x, y, color,
                                       clipL, clipT, clipR, clipB);

                prevX = x; prevY = y;
            }
        }

        private static void DrawBezierCurve(Bitmap canvas,
                                             PointF p0, PointF cp1,
                                             PointF cp2, PointF p3,
                                             int steps, Color color,
                                             int clipL, int clipT,
                                             int clipR, int clipB)
        {
            // Convert Bezier control points to Hermite tangents
            PointF t0 = new PointF(3 * (cp1.X - p0.X), 3 * (cp1.Y - p0.Y));
            PointF t1 = new PointF(3 * (p3.X  - cp2.X), 3 * (p3.Y  - cp2.Y));
            DrawHermiteCurve(canvas, p0, t0, p3, t1, steps, color,
                             clipL, clipT, clipR, clipB);
        }

        // ─── Hermite basis coefficients ──────────────────────────────────
        // Returns [a, b, c, d] so that x(t) = a*t³ + b*t² + c*t + d
        private static float[] HermiteCoeff(float x0, float s0, float x1, float s1)
        {
            // Hermite basis matrix applied to (x0, s0, x1, s1)
            float a =  2*x0 + s0 - 2*x1 + s1;
            float b = -3*x0 - 2*s0 + 3*x1 - s1;
            float c =  s0;
            float d =  x0;
            return new float[] { a, b, c, d };
        }

        // ─── DDA segment with clip ────────────────────────────────────────
        private static void DrawSegmentClipped(Bitmap canvas,
                                                int x1, int y1, int x2, int y2,
                                                Color color,
                                                int cl, int ct, int cr, int cb)
        {
            int dx = Math.Abs(x2 - x1), dy = Math.Abs(y2 - y1);
            int steps = Math.Max(dx, dy);
            if (steps == 0)
            {
                SetSafe(canvas, x1, y1, color, cl, ct, cr, cb);
                return;
            }

            double xInc = (double)(x2 - x1) / steps;
            double yInc = (double)(y2 - y1) / steps;
            double x = x1, y = y1;

            for (int i = 0; i <= steps; i++)
            {
                SetSafe(canvas, (int)Math.Round(x), (int)Math.Round(y),
                        color, cl, ct, cr, cb);
                x += xInc; y += yInc;
            }
        }

        private static void SetSafe(Bitmap canvas, int x, int y, Color color,
                                     int cl, int ct, int cr, int cb)
        {
            if (x >= cl && x <= cr && y >= ct && y <= cb &&
                x >= 0 && x < canvas.Width &&
                y >= 0 && y < canvas.Height)
                canvas.SetPixel(x, y, color);
        }
    }
}
