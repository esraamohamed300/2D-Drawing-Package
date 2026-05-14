using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class CardinalSpline
    {
        // Exact translation of DrawCardinalSpline from C++.
        // Points: array of control points (minimum 3)
        // c     : tension parameter (0.5 is a good default)
        // numpix: number of interpolation steps per segment (e.g. 100)

        public static void Draw(Bitmap bmp, PointF[] points, double c, int numpix, Color color)
        {
            int n = points.Length;
            if (n < 3) return;

            double c1 = 1.0 - c;

            // Tangent at first interior point uses P[0] and P[2]
            PointF T0 = new PointF(
                (float)(c1 * (points[2].X - points[0].X)),
                (float)(c1 * (points[2].Y - points[0].Y))
            );

            for (int i = 1; i < n - 1; i++)
            {
                PointF T1 = new PointF(
                    (float)(c1 * (points[i + 1].X - points[i - 1].X)),
                    (float)(c1 * (points[i + 1].Y - points[i - 1].Y))
                );

                HermiteCurve.Draw(bmp, points[i - 1], T0, points[i], T1, numpix, color);

                T0 = T1;
            }
        }
    }
}
