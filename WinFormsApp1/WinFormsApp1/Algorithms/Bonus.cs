using System.Drawing;

namespace WinFormsApp1.Algorithms
{
    public static class Faces
    {
        public static void HappyFace(
            Bitmap canvas,
            int xc,
            int yc,
            int radius,
            Color faceColor,
            Color fillColor)
        {
            // FACE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc,
                yc,
                radius,
                faceColor);

            FloodFill.DrawNonRecursive(
                canvas,
                xc,
                yc,
                fillColor,
                faceColor);

            // LEFT EYE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc - radius / 3,
                yc - radius / 3,
                radius / 6,
                Color.Black);

            FloodFill.DrawNonRecursive(
                canvas,
                xc - radius / 3,
                yc - radius / 3,
                Color.Black,
                Color.Black);

            // RIGHT EYE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc + radius / 3,
                yc - radius / 3,
                radius / 6,
                Color.Black);

            FloodFill.DrawNonRecursive(
                canvas,
                xc + radius / 3,
                yc - radius / 3,
                Color.Black,
                Color.Black);

            // NOSE
            DDA.Draw(
                canvas,
                xc,
                yc - radius / 6,
                xc,
                yc + radius / 6,
                Color.Black);

            // MOUTH (SMILE)
            PointF P0 = new PointF(
                xc - radius / 3,
                yc + radius / 3);

            PointF T0 = new PointF(
                radius / 1.5f,
                radius / 1.5f);

            PointF P1 = new PointF(
                xc + radius / 3,
                yc + radius / 3);

            PointF T1 = new PointF(
                radius / 1.5f,
                -radius / 1.5f);

            HermiteCurve.Draw(
                canvas,
                P0,
                T0,
                P1,
                T1,
                1000,
                Color.Black);
        }

        public static void SadFace(
            Bitmap canvas,
            int xc,
            int yc,
            int radius,
            Color faceColor,
            Color fillColor)
        {
            // FACE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc,
                yc,
                radius,
                faceColor);

            FloodFill.DrawNonRecursive(
                canvas,
                xc,
                yc,
                fillColor,
                faceColor);

            // LEFT EYE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc - radius / 3,
                yc - radius / 3,
                radius / 6,
                Color.Black);

            FloodFill.DrawNonRecursive(
                canvas,
                xc - radius / 3,
                yc - radius / 3,
                Color.Black,
                Color.Black);

            // RIGHT EYE
            ModifiedBresenhamCircle.Draw(
                canvas,
                xc + radius / 3,
                yc - radius / 3,
                radius / 6,
                Color.Black);

            FloodFill.DrawNonRecursive(
                canvas,
                xc + radius / 3,
                yc - radius / 3,
                Color.Black,
                Color.Black);

            // NOSE
            DDA.Draw(
                canvas,
                xc,
                yc - radius / 6,
                xc,
                yc + radius / 6,
                Color.Black);

            // MOUTH (SAD)
            PointF P0 = new PointF(
                xc - radius / 3,
                yc + radius / 2);

            PointF T0 = new PointF(
                radius / 1.5f,
                -radius / 1.5f);

            PointF P1 = new PointF(
                xc + radius / 3,
                yc + radius / 2);

            PointF T1 = new PointF(
                radius / 1.5f,
                radius / 1.5f);

            HermiteCurve.Draw(
                canvas,
                P0,
                T0,
                P1,
                T1,
                1000,
                Color.Black);
        }
    }
}