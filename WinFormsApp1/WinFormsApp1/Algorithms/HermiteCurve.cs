using System;
using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public class HermiteCurve
    {
        // ── Matrix / Vector helpers ──────────────────────────────────────

        struct Vector4
        {
            public double[] v;

            public Vector4(double a = 0, double b = 0, double c = 0, double d = 0)
            {
                v = new double[] { a, b, c, d };
            }

            // FIX: always ensure v is initialized before accessing
            public double this[int i]
            {
                get
                {
                    if (v == null) v = new double[4];
                    return v[i];
                }
                set
                {
                    if (v == null) v = new double[4];
                    v[i] = value;
                }
            }

            // Helper to ensure array exists
            public static Vector4 Empty() => new Vector4(0, 0, 0, 0);
        }

        static Vector4 MultiplyMatrixVector(double[] M, Vector4 vec)
        {
            Vector4 res = Vector4.Empty(); // FIX: use Empty() instead of new Vector4()
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    res[i] += M[i * 4 + j] * vec[j];
            return res;
        }

        static double DotProduct(Vector4 a, Vector4 b)
            => a[0] * b[0] + a[1] * b[1] + a[2] * b[2] + a[3] * b[3];

        static Vector4 GetHermiteCoeff(double x0, double s0, double x1, double s1)
        {
            double[] H = {
                 2,  1, -2,  1,
                -3, -2,  3, -1,
                 0,  1,  0,  0,
                 1,  0,  0,  0
            };
            Vector4 v = new Vector4(x0, s0, x1, s1);
            return MultiplyMatrixVector(H, v);
        }

        // ── Public draw method ───────────────────────────────────────────
        public static void Draw(Bitmap bmp,
                                PointF P0, PointF T0,
                                PointF P1, PointF T1,
                                int numPoints, Color color)
        {
            if (numPoints < 2) return;

            Vector4 xcoeff = GetHermiteCoeff(P0.X, T0.X, P1.X, T1.X);
            Vector4 ycoeff = GetHermiteCoeff(P0.Y, T0.Y, P1.Y, T1.Y);

            double dt = 1.0 / (numPoints - 1);

            for (double t = 0; t <= 1.0 + dt * 0.5; t += dt)
            {
                double tc = Math.Min(t, 1.0); // clamp to avoid floating point overshoot

                // vt = [t^3, t^2, t, 1]
                Vector4 vt = Vector4.Empty(); // FIX: use Empty() instead of new Vector4()
                vt[3] = 1;
                vt[2] = tc;
                vt[1] = tc * tc;
                vt[0] = tc * tc * tc;

                int x = (int)Math.Round(DotProduct(xcoeff, vt));
                int y = (int)Math.Round(DotProduct(ycoeff, vt));

                SafeSet(bmp, x, y, color);
            }
        }

        static void SafeSet(Bitmap bmp, int x, int y, Color color)
        {
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                bmp.SetPixel(x, y, color);
        }
    }
}