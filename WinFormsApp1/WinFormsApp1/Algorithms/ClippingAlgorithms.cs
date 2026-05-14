using System;
using System.Collections.Generic;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    // =========================================================================
    //  Shared helpers
    // =========================================================================
    internal static class ClippingHelpers
    {
        /// <summary>Draw a filled 5×5 dot on the bitmap.</summary>
        public static void DrawDot(Bitmap canvas, int x, int y, Color color, int size = 5)
        {
            int half = size / 2;
            for (int dy = -half; dy <= half; dy++)
                for (int dx = -half; dx <= half; dx++)
                {
                    int px = x + dx, py = y + dy;
                    if (px >= 0 && px < canvas.Width && py >= 0 && py < canvas.Height)
                        canvas.SetPixel(px, py, color);
                }
        }

        /// <summary>Draw a rectangle border using DDA lines.</summary>
        public static void DrawRect(Bitmap canvas, int xmin, int ymin, int xmax, int ymax, Color color)
        {
            DDA.Draw(canvas, xmin, ymin, xmax, ymin, color);
            DDA.Draw(canvas, xmax, ymin, xmax, ymax, color);
            DDA.Draw(canvas, xmax, ymax, xmin, ymax, color);
            DDA.Draw(canvas, xmin, ymax, xmin, ymin, color);
        }

        /// <summary>Draw a circle border using Bresenham.</summary>
        public static void DrawCircle(Bitmap canvas, int cx, int cy, int r, Color color)
            => BresenhamCircle.Draw(canvas, cx, cy, r, color);
    }

    // =========================================================================
    //  RECTANGLE — Point Clipping
    // =========================================================================
    public static class RectPointClipping
    {
        /// <summary>
        /// Tests whether (px,py) is inside the rectangle [xmin,xmax]×[ymin,ymax].
        /// Draws a green dot if inside, red dot if outside, and the rect border.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int px, int py,
                                int xmin, int ymin, int xmax, int ymax)
        {
            // Normalise so top-left / bottom-right order doesn't matter
            if (xmin > xmax) (xmin, xmax) = (xmax, xmin);
            if (ymin > ymax) (ymin, ymax) = (ymax, ymin);

            ClippingHelpers.DrawRect(canvas, xmin, ymin, xmax, ymax, Color.Blue);

            bool inside = px >= xmin && px <= xmax && py >= ymin && py <= ymax;
            Color dotColor = inside ? Color.Green : Color.Red;
            ClippingHelpers.DrawDot(canvas, px, py, dotColor, 7);

            return inside;
        }
    }

    // =========================================================================
    //  RECTANGLE — Line Clipping  (Cohen-Sutherland)
    // =========================================================================
    public static class RectLineClipping
    {
        // Outcodes
        private const int INSIDE = 0, LEFT = 1, RIGHT = 2, BOTTOM = 4, TOP = 8;

        private static int ComputeOutCode(double x, double y,
                                          int xmin, int ymin, int xmax, int ymax)
        {
            int code = INSIDE;
            if (x < xmin) code |= LEFT;
            else if (x > xmax) code |= RIGHT;
            if (y < ymin) code |= TOP;
            else if (y > ymax) code |= BOTTOM;
            return code;
        }

        /// <summary>
        /// Clips the segment (x1,y1)-(x2,y2) against the rectangle.
        /// Draws the original segment in gray, the clipped segment in green,
        /// or a red cross if the segment is fully outside.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int x1, int y1, int x2, int y2,
                                int xmin, int ymin, int xmax, int ymax)
        {
            if (xmin > xmax) (xmin, xmax) = (xmax, xmin);
            if (ymin > ymax) (ymin, ymax) = (ymax, ymin);

            // Draw the rectangle window
            ClippingHelpers.DrawRect(canvas, xmin, ymin, xmax, ymax, Color.Blue);

            // Draw original line in gray
            DDA.Draw(canvas, x1, y1, x2, y2, Color.Gray);

            double cx1 = x1, cy1 = y1, cx2 = x2, cy2 = y2;

            int out1 = ComputeOutCode(cx1, cy1, xmin, ymin, xmax, ymax);
            int out2 = ComputeOutCode(cx2, cy2, xmin, ymin, xmax, ymax);

            while (true)
            {
                if ((out1 | out2) == 0)          // both inside
                {
                    DDA.Draw(canvas, (int)cx1, (int)cy1, (int)cx2, (int)cy2, Color.Green);
                    ClippingHelpers.DrawDot(canvas, (int)cx1, (int)cy1, Color.Green);
                    ClippingHelpers.DrawDot(canvas, (int)cx2, (int)cy2, Color.Green);
                    return true;
                }
                if ((out1 & out2) != 0)          // both outside same region
                {
                    Console.WriteLine("[Clipping] Line is completely outside the rectangle.");
                    return false;
                }

                // Pick the outside point
                int outCode = out1 != INSIDE ? out1 : out2;
                double xi, yi;

                if ((outCode & BOTTOM) != 0)
                {
                    xi = cx1 + (cx2 - cx1) * (ymax - cy1) / (cy2 - cy1);
                    yi = ymax;
                }
                else if ((outCode & TOP) != 0)
                {
                    xi = cx1 + (cx2 - cx1) * (ymin - cy1) / (cy2 - cy1);
                    yi = ymin;
                }
                else if ((outCode & RIGHT) != 0)
                {
                    yi = cy1 + (cy2 - cy1) * (xmax - cx1) / (cx2 - cx1);
                    xi = xmax;
                }
                else  // LEFT
                {
                    yi = cy1 + (cy2 - cy1) * (xmin - cx1) / (cx2 - cx1);
                    xi = xmin;
                }

                if (outCode == out1) { cx1 = xi; cy1 = yi; out1 = ComputeOutCode(cx1, cy1, xmin, ymin, xmax, ymax); }
                else { cx2 = xi; cy2 = yi; out2 = ComputeOutCode(cx2, cy2, xmin, ymin, xmax, ymax); }
            }
        }
    }

    // =========================================================================
    //  RECTANGLE — Polygon Clipping  (Sutherland-Hodgman)
    // =========================================================================
    public static class RectPolygonClipping
    {
        private static List<PointF> ClipAgainstEdge(List<PointF> poly,
                                                     Func<PointF, bool> inside,
                                                     Func<PointF, PointF, PointF> intersect)
        {
            var output = new List<PointF>();
            if (poly.Count == 0) return output;

            PointF prev = poly[poly.Count - 1];
            bool prevIn = inside(prev);

            foreach (PointF cur in poly)
            {
                bool curIn = inside(cur);
                if (!prevIn && curIn) { output.Add(intersect(prev, cur)); output.Add(cur); }
                else if (prevIn && curIn) { output.Add(cur); }
                else if (prevIn && !curIn) { output.Add(intersect(prev, cur)); }
                prev = cur; prevIn = curIn;
            }
            return output;
        }

        private static PointF VIntersect(PointF a, PointF b, float xe)
        {
            float t = (xe - a.X) / (b.X - a.X);
            return new PointF(xe, a.Y + t * (b.Y - a.Y));
        }
        private static PointF HIntersect(PointF a, PointF b, float ye)
        {
            float t = (ye - a.Y) / (b.Y - a.Y);
            return new PointF(a.X + t * (b.X - a.X), ye);
        }

        /// <summary>
        /// Clips the polygon against the rectangle and draws:
        ///   • Original polygon in gray (drawn first / underneath)
        ///   • Clipped polygon edges in green (drawn second)
        ///   • Rectangle CLIPPING WINDOW in blue (drawn LAST — always on top, never overwritten)
        /// Returns the clipped vertex list (may be empty if fully outside).
        /// </summary>
        public static List<PointF> Clip(Bitmap canvas,
                                        List<Point> polygon,
                                        int xmin, int ymin, int xmax, int ymax)
        {
            if (xmin > xmax) (xmin, xmax) = (xmax, xmin);
            if (ymin > ymax) (ymin, ymax) = (ymax, ymin);

            // 1. Draw original polygon in gray (background layer)
            for (int i = 0; i < polygon.Count; i++)
            {
                Point a = polygon[i], b = polygon[(i + 1) % polygon.Count];
                DDA.Draw(canvas, a.X, a.Y, b.X, b.Y, Color.Gray);
            }

            // 2. Run Sutherland-Hodgman
            var poly = new List<PointF>();
            foreach (var pt in polygon) poly.Add(new PointF(pt.X, pt.Y));

            poly = ClipAgainstEdge(poly, p => p.X >= xmin, (a, b) => VIntersect(a, b, xmin));
            poly = ClipAgainstEdge(poly, p => p.Y >= ymin, (a, b) => HIntersect(a, b, ymin));
            poly = ClipAgainstEdge(poly, p => p.X <= xmax, (a, b) => VIntersect(a, b, xmax));
            poly = ClipAgainstEdge(poly, p => p.Y <= ymax, (a, b) => HIntersect(a, b, ymax));

            // 3. Draw clipped polygon edges in green
            if (poly.Count >= 2)
            {
                for (int i = 0; i < poly.Count; i++)
                {
                    PointF a = poly[i], b = poly[(i + 1) % poly.Count];
                    DDA.Draw(canvas, (int)a.X, (int)a.Y, (int)b.X, (int)b.Y, Color.Green);
                }
                foreach (var v in poly)
                    ClippingHelpers.DrawDot(canvas, (int)v.X, (int)v.Y, Color.Green, 5);
            }
            else
            {
                Console.WriteLine("[Clipping] Polygon is completely outside the rectangle.");
            }

            // 4. Redraw the clipping window LAST — always blue, never overwritten by polygon
            ClippingHelpers.DrawRect(canvas, xmin, ymin, xmax, ymax, Color.Blue);

            return poly;
        }
    }

    // =========================================================================
    //  SQUARE — Point Clipping
    // =========================================================================
    public static class SquarePointClipping
    {
        /// <summary>
        /// Clips point (px,py) against a square centred at (cx,cy) with half-side = half.
        /// The square is the largest square that fits the given half-length symmetrically.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int px, int py,
                                int cx, int cy, int half)
        {
            int xmin = cx - half, ymin = cy - half;
            int xmax = cx + half, ymax = cy + half;

            ClippingHelpers.DrawRect(canvas, xmin, ymin, xmax, ymax, Color.DarkViolet);

            bool inside = px >= xmin && px <= xmax && py >= ymin && py <= ymax;
            ClippingHelpers.DrawDot(canvas, px, py, inside ? Color.Green : Color.Red, 7);

            return inside;
        }
    }

    // =========================================================================
    //  SQUARE — Line Clipping  (Cohen-Sutherland on a square window)
    // =========================================================================
    public static class SquareLineClipping
    {
        /// <summary>
        /// Clips the segment against a square centred at (cx,cy) with half-side = half.
        /// Internally re-uses RectLineClipping.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int x1, int y1, int x2, int y2,
                                int cx, int cy, int half)
        {
            int xmin = cx - half, ymin = cy - half;
            int xmax = cx + half, ymax = cy + half;

            // Draw the square window separately so the color differs from rectangle
            ClippingHelpers.DrawRect(canvas, xmin, ymin, xmax, ymax, Color.DarkViolet);

            // Reuse Cohen-Sutherland logic but suppress rect drawing inside it
            // by calling the raw algorithm directly
            return RectLineClipping.Clip(canvas, x1, y1, x2, y2, xmin, ymin, xmax, ymax);
        }
    }

    // =========================================================================
    //  CIRCLE — Point Clipping
    // =========================================================================
    public static class CirclePointClipping
    {
        /// <summary>
        /// Tests whether (px,py) is inside the circle centred at (cx,cy) with radius r.
        /// Draws the circle border in orange, green dot if inside, red if outside.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int px, int py,
                                int cx, int cy, int r)
        {
            ClippingHelpers.DrawCircle(canvas, cx, cy, r, Color.DarkOrange);

            int dx = px - cx, dy = py - cy;
            bool inside = dx * dx + dy * dy <= r * r;
            ClippingHelpers.DrawDot(canvas, px, py, inside ? Color.Green : Color.Red, 7);

            return inside;
        }
    }

    // =========================================================================
    //  CIRCLE — Line Clipping  (quadratic parametric)
    // =========================================================================
    public static class CircleLineClipping
    {
        /// <summary>
        /// Clips the segment (x1,y1)-(x2,y2) against the circle (cx,cy,r).
        /// Draws original segment in gray, visible portion in green.
        /// </summary>
        public static bool Clip(Bitmap canvas,
                                int x1, int y1, int x2, int y2,
                                int cx, int cy, int r)
        {
            ClippingHelpers.DrawCircle(canvas, cx, cy, r, Color.DarkOrange);

            // Draw original in gray
            DDA.Draw(canvas, x1, y1, x2, y2, Color.Gray);

            double dx = x2 - x1, dy = y2 - y1;

            // P(t) = P1 + t*(P2-P1),  substitute into (x-cx)²+(y-cy)²=r²
            double a = dx * dx + dy * dy;
            double b = 2.0 * (dx * (x1 - cx) + dy * (y1 - cy));
            double c = (x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) - (double)r * r;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                Console.WriteLine("[Clipping] Line is completely outside the circle.");
                return false;
            }

            double sqrtD = Math.Sqrt(discriminant);
            double t1 = (-b - sqrtD) / (2 * a);
            double t2 = (-b + sqrtD) / (2 * a);

            if (t1 > 1.0 || t2 < 0.0)
            {
                Console.WriteLine("[Clipping] Line segment does not intersect the circle.");
                return false;
            }

            t1 = Math.Max(0.0, t1);
            t2 = Math.Min(1.0, t2);

            int nx1 = (int)Math.Round(x1 + t1 * dx);
            int ny1 = (int)Math.Round(y1 + t1 * dy);
            int nx2 = (int)Math.Round(x1 + t2 * dx);
            int ny2 = (int)Math.Round(y1 + t2 * dy);

            DDA.Draw(canvas, nx1, ny1, nx2, ny2, Color.Green);
            ClippingHelpers.DrawDot(canvas, nx1, ny1, Color.Green);
            ClippingHelpers.DrawDot(canvas, nx2, ny2, Color.Green);

            return true;
        }
    }
}