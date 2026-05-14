using System.Drawing.Drawing2D;
using WinFormsApp1.Algorithms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        PictureBox activeTool = null;

        int shapesCount = 0;
        // ?? Colors & canvas ??????????????????????????????????????????????
        Color selectedColor = Color.Red;
        Bitmap canvas;
        Bitmap gridBackground;
        enum FaceMode
        {
            None,
            Happy,
            Sad
        }

        FaceMode currentFaceMode = FaceMode.None;
        // ?? Drawing state ????????????????????????????????????????????????
        int startX, startY;
        bool isDrawing = false;

        // ?? Ellipse: 3 clicks (center, x-radius, y-radius) ??????????????
        int ellipseClickCount = 0;
        int ellipseCX, ellipseCY;
        int ellipseA;

        // ?? Hermite: 4 clicks (P0, T0, P1, T1) ??????????????????????????
        int hermiteClickCount = 0;
        PointF hermiteP0, hermiteT0, hermiteP1;

        // ?? Cardinal Spline ??????????????????????????????????????????????
        bool cardinalActive = false;
        List<PointF> cardinalPoints = new List<PointF>();
        bool cardinalDoubleClickPending = false;

        // ?? Polygon / multi-click state (for fills) ??????????????????????
        List<Point> clickPoints = new List<Point>();
        bool collectingPoints = false;

        // ?? Line algorithms ??????????????????????????????????????????????
        enum LineAlgorithm { None, DDA, Bresenham, Parametric }
        LineAlgorithm currentLineAlgo = LineAlgorithm.None;

        // ?? Circle algorithms ?????????????????????????????????????????????
        enum CircleAlgorithm { None, Direct, Polar, IterativePolar, Bresenham, ModifiedBresenham }
        CircleAlgorithm currentCircleAlgo = CircleAlgorithm.None;

        // ?? Ellipse algorithms ???????????????????????????????????????????
        enum EllipseAlgorithm { None, Direct, Polar, MidPoint }
        EllipseAlgorithm currentEllipseAlgo = EllipseAlgorithm.None;

        // ?? Curve algorithms ?????????????????????????????????????????????
        enum CurveAlgorithm { None, Hermite, Cardinal }
        CurveAlgorithm currentCurveAlgo = CurveAlgorithm.None;

        // ?? Filling algorithms ????????????????????????????????????????????
        enum FillAlgorithm
        {
            None,
            FloodFillRecursive,
            FloodFillNonRecursive,
            ConvexFill,
            NonConvexFill,
            CircleQuarterFill,
            CircleQuarterFillLines,
            HermiteSquareFill,
            BezierRectFill
        }
        FillAlgorithm currentFillAlgo = FillAlgorithm.None;

        // ?????????????????????????????????????????????????????????????????
        //  Dark menu renderer
        // ?????????????????????????????????????????????????????????????????
        class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkMenuColors()) { }
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(157, 161, 231)), e.Item.Bounds);
                else
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(58, 58, 56)))
                        e.Graphics.FillRectangle(b, e.Item.Bounds);
            }
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = Color.White;
                base.OnRenderItemText(e);
            }
        }

        class DarkMenuColors : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientBegin => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientEnd => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(25, 25, 25);
            public override Color MenuItemBorder => Color.FromArgb(157, 161, 231);
        }

        // ?????????????????????????????????????????????????????????????????
        //  Rounded corners
        // ?????????????????????????????????????????????????????????????????
        private void ApplyRoundedColors()
        {
            int r = 10;
            MakeRounded(color1, r); MakeRounded(color2, r);
            MakeRounded(color3, r); MakeRounded(color4, r);
            MakeRounded(color5, r); MakeRounded(color6, r);
            MakeRounded(color7, r); MakeRounded(color8, r);
            MakeRounded(color9, r); MakeRounded(color10, r);
        }

        private void MakeRounded(Control c, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(c.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(c.Width - radius, c.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, c.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            c.Region = new Region(path);
        }
        private void whiteBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridBackground = new Bitmap(Properties.Resources.WhiteBackground, drawBox.Width, drawBox.Height);
            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawImage(gridBackground, 0, 0);
            RefreshCanvas();
        }

        private void blackBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridBackground = new Bitmap(Properties.Resources.BlackBackground, drawBox.Width, drawBox.Height);
            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawImage(gridBackground, 0, 0);
            RefreshCanvas();
        }


        private void IncrementShapeCount()
        {
            shapesCount++;
            countShapes.Text = shapesCount.ToString();
        }
        private void crossToolStripMenuItem_Click(object sender, EventArgs e)
    => drawBox.Cursor = Cursors.Cross;

        private void arrowToolStripMenuItem_Click(object sender, EventArgs e)
            => drawBox.Cursor = Cursors.Arrow;

        private void handToolStripMenuItem_Click(object sender, EventArgs e)
            => drawBox.Cursor = Cursors.Hand;
        private void SetActiveTool(PictureBox tool)
        {
            // Remove highlight from the previously active tool
            if (activeTool != null)
            {
                activeTool.BackColor = Color.Transparent;
                activeTool.BorderStyle = BorderStyle.None;
            }

            activeTool = tool;

            // Highlight the newly active tool
            if (activeTool != null)
            {
                activeTool.BackColor = Color.FromArgb(80, 157, 161, 231);
                activeTool.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        // ?????????????????????????????????????????????????????????????????
        //  Constructor
        // ?????????????????????????????????????????????????????????????????
        public Form1()
        {
            InitializeComponent();

            if (drawBox.Image != null)
            {
                gridBackground = new Bitmap(drawBox.Image, drawBox.Width, drawBox.Height);
                canvas = new Bitmap(gridBackground);
            }
            else
            {
                gridBackground = new Bitmap(drawBox.Width, drawBox.Height);
                canvas = new Bitmap(drawBox.Width, drawBox.Height);
            }
            drawBox.Image = canvas;

            MakeRounded(colorPreview, 20);
            ApplyRoundedColors();
            menuStrip1.Renderer = new DarkMenuRenderer();
            menuStrip1.BackColor = Color.FromArgb(58, 58, 56);
            menuStrip1.ForeColor = Color.White;

            CToolName.AutoSize = false;        // stop it from resizing itself
            CToolName.Size = new Size(180, 80); // fixed width & height
            CToolName.TextAlign = ContentAlignment.MiddleCenter; // always centered

            // Wire circle menu
            directToolStripMenuItem.Click += directToolStripMenuItem_Click;
            polarToolStripMenuItem.Click += polarToolStripMenuItem_Click;
            itrativePolarToolStripMenuItem.Click += itrativePolarToolStripMenuItem_Click;
            bresenhamToolStripMenuItem1.Click += bresenhamToolStripMenuItem1_Click;
            modifiedBresenhamToolStripMenuItem.Click += modifiedBresenhamToolStripMenuItem_Click;

            // Wire ellipse menu
            ellipseToolStripMenuItem.Click += ellipseMenuToolStripMenuItem_Click;

            // Wire curves menu
            curvesToolStripMenuItem.Click += curvesMenuToolStripMenuItem_Click;

            // Wire fill menu
            fillingFloodToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.FloodFillRecursive, "Flood Fill (Recursive)");
            nonFillingFloodToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.FloodFillNonRecursive, "Flood Fill (Non-Recursive)");
            convexToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.ConvexFill, "Convex Fill");
            nonConvexToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.NonConvexFill, "Non-Convex Fill");
            quraterCircleFillingToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.CircleQuarterFill, "Circle Quarter Fill");
            quraterCircleFillingLinesToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.CircleQuarterFillLines, "Circle Quarter Lines");
            hermiteToolStripMenuItem1.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.HermiteSquareFill, "Hermite Square Fill");
            bezierHorToolStripMenuItem.Click += (s, e) => SetFillAlgorithm(FillAlgorithm.BezierRectFill, "Bezier Rect Fill");


            LineTool.Click += (s, e) => { SetActiveTool(LineTool); SetLineAlgorithm(LineAlgorithm.DDA, "DDA"); };
            CurveTool.Click += (s, e) => { SetActiveTool(CurveTool); SetCurveAlgorithm(CurveAlgorithm.Hermite, "Hermite"); };
            CircleTool.Click += (s, e) => { SetActiveTool(CircleTool); SetCircleAlgorithm(CircleAlgorithm.Bresenham, "Bresenham"); };
            EllipseTool.Click += (s, e) => { SetActiveTool(EllipseTool); SetEllipseAlgorithm(EllipseAlgorithm.MidPoint, "MidPoint"); };
            FillingTool.Click += (s, e) => { SetActiveTool(FillingTool); SetFillAlgorithm(FillAlgorithm.FloodFillNonRecursive, "Flood Fill"); };
            EraserTool.Click += (s, e) => { SetActiveTool(EraserTool); ResetAllTools(); CToolName.Text = "Eraser"; };



            WireClippingMenu();
        }

        // ?????????????????????????????????????????????????????????????????
        //  Canvas helpers
        // ?????????????????????????????????????????????????????????????????
        private void RefreshCanvas()
        {
            drawBox.Image = canvas;
            drawBox.Invalidate();
        }

        private Point ToBitmapCoords(int mouseX, int mouseY)
        {
            float scaleX = (float)canvas.Width / drawBox.Width;
            float scaleY = (float)canvas.Height / drawBox.Height;
            return new Point(
                (int)Math.Round(mouseX * scaleX),
                (int)Math.Round(mouseY * scaleY)
            );
        }

        // ?????????????????????????????????????????????????????????????????
        //  Reset
        // ?????????????????????????????????????????????????????????????????
        private void ResetAllTools()
        {
            currentLineAlgo = LineAlgorithm.None;
            currentCircleAlgo = CircleAlgorithm.None;
            currentEllipseAlgo = EllipseAlgorithm.None;
            currentCurveAlgo = CurveAlgorithm.None;
            currentFillAlgo = FillAlgorithm.None;
            ellipseClickCount = 0;
            hermiteClickCount = 0;
            cardinalActive = false;
            cardinalDoubleClickPending = false;
            cardinalPoints.Clear();
            shapesCount = 0;
            currentFaceMode = FaceMode.None;
            ResetClickCollection();
            ResetClipping();
        }

        private void ResetClickCollection()
        {
            clickPoints.Clear();
            collectingPoints = false;
        }

        // ?????????????????????????????????????????????????????????????????
        //  Set algorithm helpers
        // ?????????????????????????????????????????????????????????????????
        void SetLineAlgorithm(LineAlgorithm algo, string name)
        {
            ResetAllTools();
            currentLineAlgo = algo;
            CToolName.Text = "Line:\n" + name;
        }

        void SetCircleAlgorithm(CircleAlgorithm algo, string name)
        {
            ResetAllTools();
            currentCircleAlgo = algo;
            CToolName.Text = "Circle:\n" + name;
        }

        void SetEllipseAlgorithm(EllipseAlgorithm algo, string name)
        {
            ResetAllTools();
            currentEllipseAlgo = algo;
            CToolName.Text = "Ellipse:\n" + name;
        }

        void SetCurveAlgorithm(CurveAlgorithm algo, string name)
        {
            ResetAllTools();
            currentCurveAlgo = algo;

            if (algo == CurveAlgorithm.Hermite)
            {
                hermiteClickCount = 0;
                CToolName.Text = "Hermite:\nClick P0";
            }
            else if (algo == CurveAlgorithm.Cardinal)
            {
                cardinalActive = true;
                cardinalPoints.Clear();
                cardinalDoubleClickPending = false;
                CToolName.Text = "Cardinal:\nClick points\nDbl-click to draw";
            }
        }

        void SetFillAlgorithm(FillAlgorithm algo, string name)
        {
            ResetAllTools();
            currentFillAlgo = algo;
            CToolName.Text = "Fill:\n" + name;

            bool needsPoints = algo == FillAlgorithm.ConvexFill ||
                               algo == FillAlgorithm.NonConvexFill ||
                               algo == FillAlgorithm.CircleQuarterFill ||
                               algo == FillAlgorithm.CircleQuarterFillLines ||
                               algo == FillAlgorithm.HermiteSquareFill ||
                               algo == FillAlgorithm.BezierRectFill;

            collectingPoints = needsPoints;
            if (needsPoints) ShowHint(algo);
        }

        private void ShowHint(FillAlgorithm algo)
        {
            string hint = algo switch
            {
                FillAlgorithm.ConvexFill or
                FillAlgorithm.NonConvexFill => "Left-click to add vertices.\nRight-click to finish polygon.",
                FillAlgorithm.CircleQuarterFill or
                FillAlgorithm.CircleQuarterFillLines => "Click 1: center  |  Click 2: edge  |  Click 3: quarter",
                FillAlgorithm.HermiteSquareFill => "Click 1: top-left  |  Click 2: bottom-right",
                FillAlgorithm.BezierRectFill => "Click 1: top-left  |  Click 2: bottom-right",
                _ => ""
            };
            if (!string.IsNullOrEmpty(hint))
                MessageBox.Show(hint, "How to use", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ?????????????????????????????????????????????????????????????????
        //  Line menu handlers
        // ?????????????????????????????????????????????????????????????????
        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
            => SetLineAlgorithm(LineAlgorithm.DDA, "DDA");

        private void bresenhamToolStripMenuItem_Click(object sender, EventArgs e)
            => SetLineAlgorithm(LineAlgorithm.Bresenham, "Bresenham");

        private void parametricToolStripMenuItem_Click(object sender, EventArgs e)
            => SetLineAlgorithm(LineAlgorithm.Parametric, "Parametric");

        // ?????????????????????????????????????????????????????????????????
        //  Circle menu handlers
        // ?????????????????????????????????????????????????????????????????
        private void directToolStripMenuItem_Click(object sender, EventArgs e)
            => SetCircleAlgorithm(CircleAlgorithm.Direct, "Direct");

        private void polarToolStripMenuItem_Click(object sender, EventArgs e)
            => SetCircleAlgorithm(CircleAlgorithm.Polar, "Polar");

        private void itrativePolarToolStripMenuItem_Click(object sender, EventArgs e)
            => SetCircleAlgorithm(CircleAlgorithm.IterativePolar, "Iterative Polar");

        private void bresenhamToolStripMenuItem1_Click(object sender, EventArgs e)
            => SetCircleAlgorithm(CircleAlgorithm.Bresenham, "Bresenham");

        private void modifiedBresenhamToolStripMenuItem_Click(object sender, EventArgs e)
            => SetCircleAlgorithm(CircleAlgorithm.ModifiedBresenham, "Modified Bresenham");

        // ?????????????????????????????????????????????????????????????????
        //  Ellipse menu handler
        // ?????????????????????????????????????????????????????????????????
        private void ellipseMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EllipseAlgorithm next = currentEllipseAlgo switch
            {
                EllipseAlgorithm.None => EllipseAlgorithm.MidPoint,
                EllipseAlgorithm.MidPoint => EllipseAlgorithm.Direct,
                EllipseAlgorithm.Direct => EllipseAlgorithm.Polar,
                _ => EllipseAlgorithm.MidPoint
            };
            SetEllipseAlgorithm(next, next.ToString());
        }

        // ?????????????????????????????????????????????????????????????????
        //  Curves menu handler
        // ?????????????????????????????????????????????????????????????????
        private void curvesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurveAlgorithm next = currentCurveAlgo switch
            {
                CurveAlgorithm.None => CurveAlgorithm.Hermite,
                CurveAlgorithm.Hermite => CurveAlgorithm.Cardinal,
                _ => CurveAlgorithm.Hermite
            };
            SetCurveAlgorithm(next, next.ToString());
        }

        // ?????????????????????????????????????????????????????????????????
        //  Mouse events
        // ?????????????????????????????????????????????????????????????????
        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = ToBitmapCoords(e.X, e.Y);

            if (currentClipAlgo != ClipAlgorithm.None)
            {
                if (HandleClipClick(e)) return;
            }

            // FACE DRAWING
            if (currentFaceMode != FaceMode.None)
            {
                switch (currentFaceMode)
                {
                    case FaceMode.Happy:

                        Faces.HappyFace(
                            canvas,
                            p.X,
                            p.Y,
                            80,
                            Color.Black,
                            Color.Yellow);

                        break;

                    case FaceMode.Sad:

                        Faces.SadFace(
                            canvas,
                            p.X,
                            p.Y,
                            80,
                            Color.Black,
                            Color.Yellow);

                        break;
                }

                IncrementShapeCount();

                RefreshCanvas();

                return;
            }
            // ?? Right-click: finish polygon ???????????????????????????????
            if (e.Button == MouseButtons.Right)
            {
                if (collectingPoints &&
                   (currentFillAlgo == FillAlgorithm.ConvexFill ||
                    currentFillAlgo == FillAlgorithm.NonConvexFill))
                    FinishPolygonFill();
                return;
            }

            // ?? Ellipse: 3 clicks ?????????????????????????????????????????
            if (currentEllipseAlgo != EllipseAlgorithm.None)
            {
                ellipseClickCount++;
                if (ellipseClickCount == 1)
                {
                    ellipseCX = p.X; ellipseCY = p.Y;
                    CToolName.Text = "Ellipse:\nClick X-radius";
                }
                else if (ellipseClickCount == 2)
                {
                    ellipseA = Math.Abs(p.X - ellipseCX);
                    CToolName.Text = "Ellipse:\nClick Y-radius";
                }
                else if (ellipseClickCount == 3)
                {
                    int b = Math.Abs(p.Y - ellipseCY);
                    switch (currentEllipseAlgo)
                    {
                        case EllipseAlgorithm.Direct: DirectEllipse.Draw(canvas, ellipseCX, ellipseCY, ellipseA, b, selectedColor); break;
                        case EllipseAlgorithm.Polar: PolarEllipse.Draw(canvas, ellipseCX, ellipseCY, ellipseA, b, selectedColor); break;
                        case EllipseAlgorithm.MidPoint: MidPointEllipse.Draw(canvas, ellipseCX, ellipseCY, ellipseA, b, selectedColor); break;
                    }
                    IncrementShapeCount();
                    ellipseClickCount = 0;
                    CToolName.Text = "Ellipse:\n" + currentEllipseAlgo;
                    RefreshCanvas();
                }
                return;
            }

            // ?? Hermite curve: 4 clicks ???????????????????????????????????
            if (currentCurveAlgo == CurveAlgorithm.Hermite)
            {
                hermiteClickCount++;
                if (hermiteClickCount == 1)
                {
                    hermiteP0 = new PointF(p.X, p.Y);
                    CToolName.Text = "Hermite:\nClick T0 tip";
                }
                else if (hermiteClickCount == 2)
                {
                    float dx = p.X - hermiteP0.X;
                    float dy = p.Y - hermiteP0.Y;
                    float len = MathF.Sqrt(dx * dx + dy * dy);
                    float scale = 200f;
                    hermiteT0 = len > 0
                        ? new PointF(dx / len * scale, dy / len * scale)
                        : new PointF(scale, 0);
                    CToolName.Text = "Hermite:\nClick P1";
                }
                else if (hermiteClickCount == 3)
                {
                    hermiteP1 = new PointF(p.X, p.Y);
                    CToolName.Text = "Hermite:\nClick T1 tip";
                }
                else if (hermiteClickCount == 4)
                {
                    float dx = p.X - hermiteP1.X;
                    float dy = p.Y - hermiteP1.Y;
                    float len = MathF.Sqrt(dx * dx + dy * dy);
                    float scale = 200f;
                    PointF T1 = len > 0
                        ? new PointF(dx / len * scale, dy / len * scale)
                        : new PointF(scale, 0);
                    HermiteCurve.Draw(canvas, hermiteP0, hermiteT0, hermiteP1, T1, 1000, selectedColor);
                    IncrementShapeCount();
                    hermiteClickCount = 0;
                    CToolName.Text = "Hermite:\nClick P0";
                    RefreshCanvas();
                }
                return;
            }

            // ?? Cardinal Spline ???????????????????????????????????????????
            if (currentCurveAlgo == CurveAlgorithm.Cardinal)
            {
                if (e.Clicks == 2)
                {
                    if (cardinalPoints.Count > 0)
                        cardinalPoints.RemoveAt(cardinalPoints.Count - 1);
                    if (cardinalPoints.Count >= 3)
                    {
                        CardinalSpline.Draw(canvas, cardinalPoints.ToArray(), 0.5, 100, selectedColor);
                        IncrementShapeCount();
                        cardinalPoints.Clear();
                        CToolName.Text = "Cardinal:\nClick points\nDbl-click to draw";
                        RefreshCanvas();
                    }
                    else
                    {
                        CToolName.Text = $"Cardinal:\nNeed >=3 pts\n({cardinalPoints.Count} so far)";
                    }
                    cardinalDoubleClickPending = false;
                }
                else if (e.Clicks == 1)
                {
                    cardinalPoints.Add(new PointF(p.X, p.Y));
                    if (p.X >= 0 && p.X < canvas.Width && p.Y >= 0 && p.Y < canvas.Height)
                        canvas.SetPixel(p.X, p.Y, selectedColor);
                    CToolName.Text = $"Cardinal:\n{cardinalPoints.Count} pts\nDbl-click to draw";
                    RefreshCanvas();
                }
                return;
            }
            // ?? Flood fill: single click ??????????????????????????????????
            if (currentFillAlgo == FillAlgorithm.FloodFillRecursive ||
                currentFillAlgo == FillAlgorithm.FloodFillNonRecursive)
            {
                if (currentFillAlgo == FillAlgorithm.FloodFillNonRecursive)
                    FloodFill.DrawNonRecursive(canvas, p.X, p.Y, selectedColor, Color.Red);
                else
                    FloodFill.DrawRecursive(canvas, p.X, p.Y, selectedColor, Color.Red);
                RefreshCanvas();
                return;
            }

            // ?? Multi-click fill collection ???????????????????????????????
            if (collectingPoints)
            {
                clickPoints.Add(p);
                for (int dy = -2; dy <= 2; dy++)
                    for (int dx = -2; dx <= 2; dx++)
                        if (p.X + dx >= 0 && p.X + dx < canvas.Width &&
                            p.Y + dy >= 0 && p.Y + dy < canvas.Height)
                            canvas.SetPixel(p.X + dx, p.Y + dy, Color.Blue);
                RefreshCanvas();
                HandleMultiClickFill();
                return;
            }

            // ?? Lines & Circles: drag ?????????????????????????????????????
            startX = p.X;
            startY = p.Y;
            isDrawing = true;
        }

        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = ToBitmapCoords(e.X, e.Y);
            Xcoords.Text = p.X.ToString();
            Ycoords.Text = p.Y.ToString();
        }

        private void drawBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            isDrawing = false;

            Point p = ToBitmapCoords(e.X, e.Y);
            int endX = p.X;
            int endY = p.Y;

            switch (currentLineAlgo)
            {
                case LineAlgorithm.DDA:
                    DDA.Draw(canvas, startX, startY, endX, endY, selectedColor);
                    IncrementShapeCount();
                    break;
                case LineAlgorithm.Bresenham:
                    Bresenham.Draw(canvas, startX, startY, endX, endY, selectedColor);
                    IncrementShapeCount();
                    break;
                case LineAlgorithm.Parametric:
                    ParametricLine.Draw(canvas, startX, startY, endX, endY, selectedColor);
                    IncrementShapeCount();
                    break;
            }

            if (currentCircleAlgo != CircleAlgorithm.None)
            {
                int dx = endX - startX;
                int dy = endY - startY;
                int radius = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                switch (currentCircleAlgo)
                {
                    case CircleAlgorithm.Direct: DirectCircle.Draw(canvas, startX, startY, radius, selectedColor); break;
                    case CircleAlgorithm.Polar: PolarCircle.Draw(canvas, startX, startY, radius, selectedColor); break;
                    case CircleAlgorithm.IterativePolar: IterativePolarCircle.Draw(canvas, startX, startY, radius, selectedColor); break;
                    case CircleAlgorithm.Bresenham: BresenhamCircle.Draw(canvas, startX, startY, radius, selectedColor); break;
                    case CircleAlgorithm.ModifiedBresenham: ModifiedBresenhamCircle.Draw(canvas, startX, startY, radius, selectedColor); break;
                }
                IncrementShapeCount();
            }

            RefreshCanvas();
        }
        // ?????????????????????????????????????????????????????????????????
        //  Multi-click fill logic
        // ?????????????????????????????????????????????????????????????????
        private void HandleMultiClickFill()
        {
            switch (currentFillAlgo)
            {
                case FillAlgorithm.ConvexFill:
                case FillAlgorithm.NonConvexFill:
                    if (clickPoints.Count >= 2)
                    {
                        Point a = clickPoints[^2];
                        Point b = clickPoints[^1];
                        DDA.Draw(canvas, a.X, a.Y, b.X, b.Y, Color.Gray);
                        RefreshCanvas();
                    }
                    break;

                case FillAlgorithm.CircleQuarterFill:
                case FillAlgorithm.CircleQuarterFillLines:
                    if (clickPoints.Count == 2)
                    {
                        int dx = clickPoints[1].X - clickPoints[0].X;
                        int dy = clickPoints[1].Y - clickPoints[0].Y;
                        int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                        BresenhamCircle.Draw(canvas, clickPoints[0].X, clickPoints[0].Y, r, Color.Red);
                        RefreshCanvas();
                    }
                    else if (clickPoints.Count == 3)
                    {
                        int dx = clickPoints[1].X - clickPoints[0].X;
                        int dy = clickPoints[1].Y - clickPoints[0].Y;
                        int r = (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                        int q = CircleQuarterFill.GetQuarter(clickPoints[0], clickPoints[2]);
                        if (currentFillAlgo == FillAlgorithm.CircleQuarterFill)
                            CircleQuarterFill.FillQuarter(canvas, clickPoints[0].X, clickPoints[0].Y, r, q, selectedColor);
                        else
                            CircleQuarterFill.FillQuarterLines(canvas, clickPoints[0].X, clickPoints[0].Y, r, q, selectedColor);
                        IncrementShapeCount();
                        RefreshCanvas();
                        ResetClickCollection();
                        collectingPoints = true;
                    }
                    break;

                case FillAlgorithm.HermiteSquareFill:
                    if (clickPoints.Count == 2)
                    {
                        int x1 = clickPoints[0].X, y1 = clickPoints[0].Y;
                        int x2 = clickPoints[1].X, y2 = clickPoints[1].Y;
                        int size = Math.Min(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
                        int left = Math.Min(x1, x2);
                        int top = Math.Min(y1, y2);
                        DrawRect(left, top, size, size, Color.Red);
                        HermiteBezierFill.FillSquareHermiteVertical(canvas, left, top, size, selectedColor);
                        IncrementShapeCount();
                        RefreshCanvas();
                        ResetClickCollection();
                        collectingPoints = true;
                    }
                    break;

                case FillAlgorithm.BezierRectFill:
                    if (clickPoints.Count == 2)
                    {
                        int x1 = clickPoints[0].X, y1 = clickPoints[0].Y;
                        int x2 = clickPoints[1].X, y2 = clickPoints[1].Y;
                        int left = Math.Min(x1, x2);
                        int top = Math.Min(y1, y2);
                        int width = Math.Abs(x2 - x1);
                        int height = Math.Abs(y2 - y1);
                        DrawRect(left, top, width, height, Color.Red);
                        HermiteBezierFill.FillRectangleBezierHorizontal(canvas, left, top, width, height, selectedColor);
                        IncrementShapeCount();
                        RefreshCanvas();
                        ResetClickCollection();
                        collectingPoints = true;
                    }
                    break;
            }
        }
        private void FinishPolygonFill()
        {
            if (clickPoints.Count < 3) { ResetClickCollection(); collectingPoints = true; return; }
            Point[] pts = clickPoints.ToArray();
            if (currentFillAlgo == FillAlgorithm.ConvexFill)
                ConvexFill.Draw(canvas, pts, selectedColor);
            else
                NonConvexFill.Draw(canvas, pts, selectedColor);
            IncrementShapeCount();
            RefreshCanvas();
            ResetClickCollection();
            collectingPoints = true;
        }

        private void DrawRect(int left, int top, int width, int height, Color c)
        {
            DDA.Draw(canvas, left, top, left + width, top, c);
            DDA.Draw(canvas, left + width, top, left + width, top + height, c);
            DDA.Draw(canvas, left + width, top + height, left, top + height, c);
            DDA.Draw(canvas, left, top + height, left, top, c);
        }

        // ?????????????????????????????????????????????????????????????????
        //  Clear
        // ?????????????????????????????????????????????????????????????????
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawImage(gridBackground, 0, 0);
            shapesCount = 0;
            countShapes.Text = "0";
            ellipseClickCount = 0;
            hermiteClickCount = 0;
            cardinalPoints.Clear();
            cardinalDoubleClickPending = false;
            ResetClickCollection();
            RefreshCanvas();
            SetActiveTool(null);

        }

        // ?????????????????????????????????????????????????????????????????
        //  Color palette
        // ?????????????????????????????????????????????????????????????????
        private void SetColor(Color c)
        {
            selectedColor = c;
            colorPreview.BackColor = c;
            label4.Text = ColorNameFromColor(c);
            label4.ForeColor = c;
        }
        private string ColorNameFromColor(Color c)
        {
            if (c == color1.BackColor) return "Purple";
            if (c == color2.BackColor) return "Green";
            if (c == color3.BackColor) return "Orange";
            if (c == color4.BackColor) return "Pink";
            if (c == color5.BackColor) return "Yellow";
            if (c == color6.BackColor) return "Red";
            if (c == color7.BackColor) return "White";
            if (c == color8.BackColor) return "Gray";
            if (c == color9.BackColor) return "Blue";
            if (c == color10.BackColor) return "Black";

            // For custom colors picked from the color dialog
            return $"RGB({c.R},{c.G},{c.B})";
        }
        private void color1_Click(object sender, EventArgs e) => SetColor(color1.BackColor);
        private void color2_Click(object sender, EventArgs e) => SetColor(color2.BackColor);
        private void color3_Click(object sender, EventArgs e) => SetColor(color3.BackColor);
        private void color4_Click(object sender, EventArgs e) => SetColor(color4.BackColor);
        private void color5_Click(object sender, EventArgs e) => SetColor(color5.BackColor);
        private void color6_Click(object sender, EventArgs e) => SetColor(color6.BackColor);
        private void color7_Click(object sender, EventArgs e) => SetColor(color7.BackColor);
        private void color8_Click(object sender, EventArgs e) => SetColor(color8.BackColor);
        private void color9_Click(object sender, EventArgs e) => SetColor(color9.BackColor);
        private void color10_Click(object sender, EventArgs e) => SetColor(color10.BackColor);

        // ?????????????????????????????????????????????????????????????????
        //  Designer stubs
        // ?????????????????????????????????????????????????????????????????
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void quraterCircleFillingToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void bezierHorToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void hermiteToolStripMenuItem1_Click(object sender, EventArgs e) { }

        private void clearToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawImage(gridBackground, 0, 0);
            ellipseClickCount = 0;
            hermiteClickCount = 0;
            cardinalPoints.Clear();
            cardinalDoubleClickPending = false;
            ResetClickCollection();
            RefreshCanvas();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void bounsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void happyFaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetAllTools();

            currentFaceMode = FaceMode.Happy;

            CToolName.Text =
                "Happy Face\n" +
                "Click anywhere";
        }

        private void sadFaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetAllTools();

            currentFaceMode = FaceMode.Sad;

            CToolName.Text =
                "Sad Face\n" +
                "Click anywhere";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";
            sfd.Title = "Save Drawing";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            Color bgColor = gridBackground.GetPixel(0, 0); // background reference pixel

            var lines = new List<string>();
            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color c = canvas.GetPixel(x, y);
                    // Skip pixels that match the background exactly
                    if (c.ToArgb() == bgColor.ToArgb()) continue;
                    lines.Add($"{x},{y},{c.R},{c.G},{c.B}");
                }
            }

            File.WriteAllLines(sfd.FileName, lines);
            MessageBox.Show($"Saved {lines.Count} pixels.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            ofd.Title = "Load Drawing";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            // Reset canvas to background first
            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawImage(gridBackground, 0, 0);

            string[] lines = File.ReadAllLines(ofd.FileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length != 5) continue;

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                int r = int.Parse(parts[2]);
                int g2 = int.Parse(parts[3]);
                int b = int.Parse(parts[4]);

                if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
                    canvas.SetPixel(x, y, Color.FromArgb(r, g2, b));
            }

            RefreshCanvas();
            MessageBox.Show("Drawing loaded successfully.", "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}