using System;
using System.Collections.Generic;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class ClippingAlgorithm
    {
        // ── Point Rectangle Clipping ─────────────────────────────────
        public static bool PointClipping(int x, int y,
            int xmin, int ymin, int xmax, int ymax)
        {
            return x >= xmin && x <= xmax && y >= ymin && y <= ymax;
        }

        // ── Cohen-Sutherland Line Clipping ───────────────────────────
        const int LEFT = 1;
        const int TOP = 2;
        const int RIGHT = 4;
        const int BOTTOM = 8;

        private static int GetOutCode(double x, double y,
            int xmin, int ymin, int xmax, int ymax)
        {
            int code = 0;
            if (x < xmin) code |= LEFT;
            else if (x > xmax) code |= RIGHT;
            if (y < ymin) code |= TOP;
            else if (y > ymax) code |= BOTTOM;
            return code;
        }

        public static bool CohenSutherland(
            ref double x1, ref double y1,
            ref double x2, ref double y2,
            int xmin, int ymin, int xmax, int ymax)
        {
            int out1 = GetOutCode(x1, y1, xmin, ymin, xmax, ymax);
            int out2 = GetOutCode(x2, y2, xmin, ymin, xmax, ymax);

            while (true)
            {
                if ((out1 | out2) == 0) return true;
                if ((out1 & out2) != 0) return false;

                double xi, yi;
                int outCode = out1 != 0 ? out1 : out2;

                if ((outCode & LEFT) != 0)
                {
                    xi = xmin;
                    yi = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                }
                else if ((outCode & RIGHT) != 0)
                {
                    xi = xmax;
                    yi = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                }
                else if ((outCode & TOP) != 0)
                {
                    yi = ymin;
                    xi = x1 + (x2 - x1) * (ymin - y1) / (y2 - y1);
                }
                else
                {
                    yi = ymax;
                    xi = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                }

                if (outCode == out1)
                {
                    x1 = xi; y1 = yi;
                    out1 = GetOutCode(x1, y1, xmin, ymin, xmax, ymax);
                }
                else
                {
                    x2 = xi; y2 = yi;
                    out2 = GetOutCode(x2, y2, xmin, ymin, xmax, ymax);
                }
            }
        }

        // ── Sutherland-Hodgman Polygon Clipping ──────────────────────
        public static List<PointF> PolygonClip(
            List<PointF> polygon,
            int xmin, int ymin, int xmax, int ymax)
        {
            if (polygon == null || polygon.Count == 0)
                return new List<PointF>();

            List<PointF> output = new List<PointF>(polygon);

            output = ClipEdge(output, p => p.X >= xmin,
                (a, b) => VerticalIntersect(a, b, xmin));

            output = ClipEdge(output, p => p.Y >= ymin,
                (a, b) => HorizontalIntersect(a, b, ymin));

            output = ClipEdge(output, p => p.X <= xmax,
                (a, b) => VerticalIntersect(a, b, xmax));

            output = ClipEdge(output, p => p.Y <= ymax,
                (a, b) => HorizontalIntersect(a, b, ymax));

            return output;
        }

        private static List<PointF> ClipEdge(
            List<PointF> polygon,
            Func<PointF, bool> inside,
            Func<PointF, PointF, PointF> intersect)
        {
            List<PointF> output = new List<PointF>();
            if (polygon.Count == 0) return output;

            PointF v1 = polygon[polygon.Count - 1];
            bool v1in = inside(v1);

            foreach (PointF v2 in polygon)
            {
                bool v2in = inside(v2);

                if (!v1in && v2in)
                {
                    output.Add(intersect(v1, v2));
                    output.Add(v2);
                }
                else if (v1in && v2in)
                {
                    output.Add(v2);
                }
                else if (v1in)
                {
                    output.Add(intersect(v1, v2));
                }

                v1 = v2;
                v1in = v2in;
            }
            return output;
        }

        private static PointF VerticalIntersect(PointF a, PointF b, float x)
        {
            if (b.X == a.X)
                return new PointF(x, a.Y);

            float t = (x - a.X) / (b.X - a.X);

            return new PointF(
                x,
                a.Y + t * (b.Y - a.Y)
            );
        }

        private static PointF HorizontalIntersect(PointF a, PointF b, float y)
        {
            if (b.Y == a.Y)
                return new PointF(a.X, y);

            float t = (y - a.Y) / (b.Y - a.Y);

            return new PointF(
                a.X + t * (b.X - a.X),
                y
            );
        }

        // ── Point Circle Clipping ────────────────────────────────────
        public static bool PointCircleClipping(int x, int y,
            int xc, int yc, int r)
        {
            int dx = x - xc;
            int dy = y - yc;
            return dx * dx + dy * dy <= r * r;
        }

        // ── Line Circle Clipping ─────────────────────────────────────
        public static bool LineCircleClipping(
            ref double x1, ref double y1,
            ref double x2, ref double y2,
            int xc, int yc, int r)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double a = dx * dx + dy * dy;
            double b = 2 * (dx * (x1 - xc) + dy * (y1 - yc));
            double c = (x1 - xc) * (x1 - xc) + (y1 - yc) * (y1 - yc) - (double)r * r;

            double disc = b * b - 4 * a * c;
            if (disc < 0) return false;

            disc = Math.Sqrt(disc);
            double t1 = (-b - disc) / (2 * a);
            double t2 = (-b + disc) / (2 * a);

            if (t1 > 1 || t2 < 0) return false;
            if (t1 < 0) t1 = 0;
            if (t2 > 1) t2 = 1;

            double nx1 = x1 + t1 * dx;
            double ny1 = y1 + t1 * dy;
            double nx2 = x1 + t2 * dx;
            double ny2 = y1 + t2 * dy;

            x1 = nx1; y1 = ny1;
            x2 = nx2; y2 = ny2;
            return true;
        }

        // ── Square: force equal sides from TL and BR points ──────────
        public static (int xmin, int ymin, int xmax, int ymax) MakeSquare(
            int xmin, int ymin, int xmax, int ymax)
        {
            int size = Math.Min(Math.Abs(xmax - xmin), Math.Abs(ymax - ymin));
            return (xmin, ymin, xmin + size, ymin + size);
        }
    }
}