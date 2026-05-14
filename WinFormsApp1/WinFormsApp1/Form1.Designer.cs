namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            whiteBackgroundToolStripMenuItem = new ToolStripMenuItem();
            whiteToolStripMenuItem = new ToolStripMenuItem();
            blackToolStripMenuItem = new ToolStripMenuItem();
            blackBackgroundToolStripMenuItem = new ToolStripMenuItem();
            crossToolStripMenuItem = new ToolStripMenuItem();
            arrowToolStripMenuItem = new ToolStripMenuItem();
            handToolStripMenuItem = new ToolStripMenuItem();
            linesToolStripMenuItem = new ToolStripMenuItem();
            dDAToolStripMenuItem = new ToolStripMenuItem();
            bresenhamToolStripMenuItem = new ToolStripMenuItem();
            parametricToolStripMenuItem = new ToolStripMenuItem();
            circlesToolStripMenuItem = new ToolStripMenuItem();
            directToolStripMenuItem = new ToolStripMenuItem();
            polarToolStripMenuItem = new ToolStripMenuItem();
            itrativePolarToolStripMenuItem = new ToolStripMenuItem();
            bresenhamToolStripMenuItem1 = new ToolStripMenuItem();
            modifiedBresenhamToolStripMenuItem = new ToolStripMenuItem();
            ellipseToolStripMenuItem = new ToolStripMenuItem();
            directEllipseToolStripMenuItem = new ToolStripMenuItem();
            polarEllipseToolStripMenuItem = new ToolStripMenuItem();
            midpointEllipseToolStripMenuItem = new ToolStripMenuItem();
            curvesToolStripMenuItem = new ToolStripMenuItem();
            hermiteToolStripMenuItem = new ToolStripMenuItem();
            cardinalSplineToolStripMenuItem = new ToolStripMenuItem();
            fillingToolStripMenuItem = new ToolStripMenuItem();
            convexToolStripMenuItem = new ToolStripMenuItem();
            nonConvexToolStripMenuItem = new ToolStripMenuItem();
            fillingFloodToolStripMenuItem = new ToolStripMenuItem();
            nonFillingFloodToolStripMenuItem = new ToolStripMenuItem();
            quraterCircleFillingToolStripMenuItem = new ToolStripMenuItem();
            quraterCircleFillingLinesToolStripMenuItem = new ToolStripMenuItem();
            hermiteToolStripMenuItem1 = new ToolStripMenuItem();
            bezierHorToolStripMenuItem = new ToolStripMenuItem();
            clippingToolStripMenuItem = new ToolStripMenuItem();
            rectangleToolStripMenuItem = new ToolStripMenuItem();
            pointToolStripMenuItem = new ToolStripMenuItem();
            lineToolStripMenuItem = new ToolStripMenuItem();
            polygonToolStripMenuItem = new ToolStripMenuItem();
            squareToolStripMenuItem = new ToolStripMenuItem();
            pointToolStripMenuItem1 = new ToolStripMenuItem();
            lineToolStripMenuItem1 = new ToolStripMenuItem();
            circleToolStripMenuItem = new ToolStripMenuItem();
            pointToolStripMenuItem2 = new ToolStripMenuItem();
            lineToolStripMenuItem2 = new ToolStripMenuItem();
            bounsToolStripMenuItem = new ToolStripMenuItem();
            happyFaceToolStripMenuItem = new ToolStripMenuItem();
            sadFaceToolStripMenuItem = new ToolStripMenuItem();
            leftPanel = new Panel();
            label12 = new Label();
            label11 = new Label();
            EraserTool = new PictureBox();
            FillingTool = new PictureBox();
            CircleTool = new PictureBox();
            EllipseTool = new PictureBox();
            CurveTool = new PictureBox();
            LineTool = new PictureBox();
            panel1 = new Panel();
            countShapes = new Label();
            label10 = new Label();
            label9 = new Label();
            Ycoords = new Label();
            Xcoords = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            color10 = new Panel();
            color9 = new Panel();
            color8 = new Panel();
            color7 = new Panel();
            color6 = new Panel();
            color5 = new Panel();
            color4 = new Panel();
            color3 = new Panel();
            color2 = new Panel();
            color1 = new Panel();
            label4 = new Label();
            colorPreview = new Panel();
            label3 = new Label();
            label2 = new Label();
            CToolName = new Label();
            label1 = new Label();
            drawBox = new PictureBox();
            panel3 = new Panel();
            label13 = new Label();
            menuStrip1.SuspendLayout();
            leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EraserTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FillingTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CircleTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EllipseTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CurveTool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LineTool).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)drawBox).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(58, 58, 56);
            menuStrip1.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, preferencesToolStripMenuItem, linesToolStripMenuItem, circlesToolStripMenuItem, ellipseToolStripMenuItem, curvesToolStripMenuItem, fillingToolStripMenuItem, clippingToolStripMenuItem, bounsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 10, 0, 10);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size(1498, 48);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearToolStripMenuItem, saveToolStripMenuItem, loadToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Font = new Font("Roboto", 12F);
            fileToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            fileToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(56, 28);
            fileToolStripMenuItem.Text = "File";
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(224, 28);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click_1;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(224, 28);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(224, 28);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 28);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { whiteBackgroundToolStripMenuItem, blackBackgroundToolStripMenuItem });
            preferencesToolStripMenuItem.Font = new Font("Roboto", 12F);
            preferencesToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            preferencesToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(133, 28);
            preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // whiteBackgroundToolStripMenuItem
            // 
            whiteBackgroundToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { whiteToolStripMenuItem, blackToolStripMenuItem });
            whiteBackgroundToolStripMenuItem.Name = "whiteBackgroundToolStripMenuItem";
            whiteBackgroundToolStripMenuItem.Size = new Size(214, 28);
            whiteBackgroundToolStripMenuItem.Text = "Background";
            // 
            // whiteToolStripMenuItem
            // 
            whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            whiteToolStripMenuItem.Size = new Size(146, 28);
            whiteToolStripMenuItem.Text = "White";
            whiteToolStripMenuItem.Click += whiteBackgroundToolStripMenuItem_Click;
            // 
            // blackToolStripMenuItem
            // 
            blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            blackToolStripMenuItem.Size = new Size(146, 28);
            blackToolStripMenuItem.Text = "Black";
            blackToolStripMenuItem.Click += blackBackgroundToolStripMenuItem_Click;
            // 
            // blackBackgroundToolStripMenuItem
            // 
            blackBackgroundToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { crossToolStripMenuItem, arrowToolStripMenuItem, handToolStripMenuItem });
            blackBackgroundToolStripMenuItem.Name = "blackBackgroundToolStripMenuItem";
            blackBackgroundToolStripMenuItem.Size = new Size(214, 28);
            blackBackgroundToolStripMenuItem.Text = "Cursor Shape";
            // 
            // crossToolStripMenuItem
            // 
            crossToolStripMenuItem.Name = "crossToolStripMenuItem";
            crossToolStripMenuItem.Size = new Size(147, 28);
            crossToolStripMenuItem.Text = "Cross";
            crossToolStripMenuItem.Click += crossToolStripMenuItem_Click;
            // 
            // arrowToolStripMenuItem
            // 
            arrowToolStripMenuItem.Name = "arrowToolStripMenuItem";
            arrowToolStripMenuItem.Size = new Size(147, 28);
            arrowToolStripMenuItem.Text = "Arrow";
            arrowToolStripMenuItem.Click += arrowToolStripMenuItem_Click;
            // 
            // handToolStripMenuItem
            // 
            handToolStripMenuItem.Name = "handToolStripMenuItem";
            handToolStripMenuItem.Size = new Size(147, 28);
            handToolStripMenuItem.Text = "Hand";
            handToolStripMenuItem.Click += handToolStripMenuItem_Click;
            // 
            // linesToolStripMenuItem
            // 
            linesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dDAToolStripMenuItem, bresenhamToolStripMenuItem, parametricToolStripMenuItem });
            linesToolStripMenuItem.Font = new Font("Roboto", 12F);
            linesToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            linesToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            linesToolStripMenuItem.Name = "linesToolStripMenuItem";
            linesToolStripMenuItem.Size = new Size(72, 28);
            linesToolStripMenuItem.Text = "Lines";
            // 
            // dDAToolStripMenuItem
            // 
            dDAToolStripMenuItem.Name = "dDAToolStripMenuItem";
            dDAToolStripMenuItem.Size = new Size(196, 28);
            dDAToolStripMenuItem.Text = "DDA";
            dDAToolStripMenuItem.Click += dDAToolStripMenuItem_Click;
            // 
            // bresenhamToolStripMenuItem
            // 
            bresenhamToolStripMenuItem.Name = "bresenhamToolStripMenuItem";
            bresenhamToolStripMenuItem.Size = new Size(196, 28);
            bresenhamToolStripMenuItem.Text = "Bresenham";
            bresenhamToolStripMenuItem.Click += bresenhamToolStripMenuItem_Click;
            // 
            // parametricToolStripMenuItem
            // 
            parametricToolStripMenuItem.Name = "parametricToolStripMenuItem";
            parametricToolStripMenuItem.Size = new Size(196, 28);
            parametricToolStripMenuItem.Text = "Parametric";
            parametricToolStripMenuItem.Click += parametricToolStripMenuItem_Click;
            // 
            // circlesToolStripMenuItem
            // 
            circlesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { directToolStripMenuItem, polarToolStripMenuItem, itrativePolarToolStripMenuItem, bresenhamToolStripMenuItem1, modifiedBresenhamToolStripMenuItem });
            circlesToolStripMenuItem.Font = new Font("Roboto", 12F);
            circlesToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            circlesToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            circlesToolStripMenuItem.Name = "circlesToolStripMenuItem";
            circlesToolStripMenuItem.Size = new Size(85, 28);
            circlesToolStripMenuItem.Text = "Circles";
            // 
            // directToolStripMenuItem
            // 
            directToolStripMenuItem.Name = "directToolStripMenuItem";
            directToolStripMenuItem.Size = new Size(278, 28);
            directToolStripMenuItem.Text = "Direct";
            // 
            // polarToolStripMenuItem
            // 
            polarToolStripMenuItem.Name = "polarToolStripMenuItem";
            polarToolStripMenuItem.Size = new Size(278, 28);
            polarToolStripMenuItem.Text = "Polar";
            // 
            // itrativePolarToolStripMenuItem
            // 
            itrativePolarToolStripMenuItem.Name = "itrativePolarToolStripMenuItem";
            itrativePolarToolStripMenuItem.Size = new Size(278, 28);
            itrativePolarToolStripMenuItem.Text = "iterative Polar";
            // 
            // bresenhamToolStripMenuItem1
            // 
            bresenhamToolStripMenuItem1.Name = "bresenhamToolStripMenuItem1";
            bresenhamToolStripMenuItem1.Size = new Size(278, 28);
            bresenhamToolStripMenuItem1.Text = "Bresenham";
            // 
            // modifiedBresenhamToolStripMenuItem
            // 
            modifiedBresenhamToolStripMenuItem.Name = "modifiedBresenhamToolStripMenuItem";
            modifiedBresenhamToolStripMenuItem.Size = new Size(278, 28);
            modifiedBresenhamToolStripMenuItem.Text = "Modified Bresenham";
            // 
            // ellipseToolStripMenuItem
            // 
            ellipseToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { directEllipseToolStripMenuItem, polarEllipseToolStripMenuItem, midpointEllipseToolStripMenuItem });
            ellipseToolStripMenuItem.Font = new Font("Roboto", 12F);
            ellipseToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            ellipseToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            ellipseToolStripMenuItem.Size = new Size(82, 28);
            ellipseToolStripMenuItem.Text = "Ellipse";
            // 
            // directEllipseToolStripMenuItem
            // 
            directEllipseToolStripMenuItem.Name = "directEllipseToolStripMenuItem";
            directEllipseToolStripMenuItem.Size = new Size(235, 28);
            directEllipseToolStripMenuItem.Text = "Direct Ellipse";
            // 
            // polarEllipseToolStripMenuItem
            // 
            polarEllipseToolStripMenuItem.Name = "polarEllipseToolStripMenuItem";
            polarEllipseToolStripMenuItem.Size = new Size(235, 28);
            polarEllipseToolStripMenuItem.Text = "Polar Ellipse";
            // 
            // midpointEllipseToolStripMenuItem
            // 
            midpointEllipseToolStripMenuItem.Name = "midpointEllipseToolStripMenuItem";
            midpointEllipseToolStripMenuItem.Size = new Size(235, 28);
            midpointEllipseToolStripMenuItem.Text = "Midpoint Ellipse";
            // 
            // curvesToolStripMenuItem
            // 
            curvesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hermiteToolStripMenuItem, cardinalSplineToolStripMenuItem });
            curvesToolStripMenuItem.Font = new Font("Roboto", 12F);
            curvesToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            curvesToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            curvesToolStripMenuItem.Name = "curvesToolStripMenuItem";
            curvesToolStripMenuItem.Size = new Size(86, 28);
            curvesToolStripMenuItem.Text = "Curves";
            // 
            // hermiteToolStripMenuItem
            // 
            hermiteToolStripMenuItem.Name = "hermiteToolStripMenuItem";
            hermiteToolStripMenuItem.Size = new Size(228, 28);
            hermiteToolStripMenuItem.Text = "Hermite";
            // 
            // cardinalSplineToolStripMenuItem
            // 
            cardinalSplineToolStripMenuItem.Name = "cardinalSplineToolStripMenuItem";
            cardinalSplineToolStripMenuItem.Size = new Size(228, 28);
            cardinalSplineToolStripMenuItem.Text = "Cardinal Spline";
            // 
            // fillingToolStripMenuItem
            // 
            fillingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { convexToolStripMenuItem, nonConvexToolStripMenuItem, fillingFloodToolStripMenuItem, nonFillingFloodToolStripMenuItem, quraterCircleFillingToolStripMenuItem, quraterCircleFillingLinesToolStripMenuItem, hermiteToolStripMenuItem1, bezierHorToolStripMenuItem });
            fillingToolStripMenuItem.Font = new Font("Roboto", 12F);
            fillingToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            fillingToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            fillingToolStripMenuItem.Name = "fillingToolStripMenuItem";
            fillingToolStripMenuItem.Size = new Size(77, 28);
            fillingToolStripMenuItem.Text = "Filling";
            // 
            // convexToolStripMenuItem
            // 
            convexToolStripMenuItem.Name = "convexToolStripMenuItem";
            convexToolStripMenuItem.Size = new Size(341, 28);
            convexToolStripMenuItem.Text = "Convex";
            // 
            // nonConvexToolStripMenuItem
            // 
            nonConvexToolStripMenuItem.Name = "nonConvexToolStripMenuItem";
            nonConvexToolStripMenuItem.Size = new Size(341, 28);
            nonConvexToolStripMenuItem.Text = "Non-Convex";
            // 
            // fillingFloodToolStripMenuItem
            // 
            fillingFloodToolStripMenuItem.Name = "fillingFloodToolStripMenuItem";
            fillingFloodToolStripMenuItem.Size = new Size(341, 28);
            fillingFloodToolStripMenuItem.Text = "Filling Flood";
            // 
            // nonFillingFloodToolStripMenuItem
            // 
            nonFillingFloodToolStripMenuItem.Name = "nonFillingFloodToolStripMenuItem";
            nonFillingFloodToolStripMenuItem.Size = new Size(341, 28);
            nonFillingFloodToolStripMenuItem.Text = "Non-Filling Flood";
            // 
            // quraterCircleFillingToolStripMenuItem
            // 
            quraterCircleFillingToolStripMenuItem.Name = "quraterCircleFillingToolStripMenuItem";
            quraterCircleFillingToolStripMenuItem.Size = new Size(341, 28);
            quraterCircleFillingToolStripMenuItem.Text = "Qurater Circle Filling";
            quraterCircleFillingToolStripMenuItem.Click += quraterCircleFillingToolStripMenuItem_Click;
            // 
            // quraterCircleFillingLinesToolStripMenuItem
            // 
            quraterCircleFillingLinesToolStripMenuItem.Name = "quraterCircleFillingLinesToolStripMenuItem";
            quraterCircleFillingLinesToolStripMenuItem.Size = new Size(341, 28);
            quraterCircleFillingLinesToolStripMenuItem.Text = "Qurater Circle Filling Lines";
            // 
            // hermiteToolStripMenuItem1
            // 
            hermiteToolStripMenuItem1.Name = "hermiteToolStripMenuItem1";
            hermiteToolStripMenuItem1.Size = new Size(341, 28);
            hermiteToolStripMenuItem1.Text = "Hermite Vertical Square";
            hermiteToolStripMenuItem1.Click += hermiteToolStripMenuItem1_Click;
            // 
            // bezierHorToolStripMenuItem
            // 
            bezierHorToolStripMenuItem.Name = "bezierHorToolStripMenuItem";
            bezierHorToolStripMenuItem.Size = new Size(341, 28);
            bezierHorToolStripMenuItem.Text = "Bezier Horizontal Rectangle";
            bezierHorToolStripMenuItem.Click += bezierHorToolStripMenuItem_Click;
            // 
            // clippingToolStripMenuItem
            // 
            clippingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rectangleToolStripMenuItem, squareToolStripMenuItem, circleToolStripMenuItem });
            clippingToolStripMenuItem.Font = new Font("Roboto", 12F);
            clippingToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            clippingToolStripMenuItem.Margin = new Padding(0, 0, 30, 0);
            clippingToolStripMenuItem.Name = "clippingToolStripMenuItem";
            clippingToolStripMenuItem.Size = new Size(96, 28);
            clippingToolStripMenuItem.Text = "Clipping";
            // 
            // rectangleToolStripMenuItem
            // 
            rectangleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pointToolStripMenuItem, lineToolStripMenuItem, polygonToolStripMenuItem });
            rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            rectangleToolStripMenuItem.Size = new Size(183, 28);
            rectangleToolStripMenuItem.Text = "Rectangle";
            // 
            // pointToolStripMenuItem
            // 
            pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            pointToolStripMenuItem.Size = new Size(165, 28);
            pointToolStripMenuItem.Text = "Point";
            // 
            // lineToolStripMenuItem
            // 
            lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            lineToolStripMenuItem.Size = new Size(165, 28);
            lineToolStripMenuItem.Text = "Line";
            // 
            // polygonToolStripMenuItem
            // 
            polygonToolStripMenuItem.Name = "polygonToolStripMenuItem";
            polygonToolStripMenuItem.Size = new Size(165, 28);
            polygonToolStripMenuItem.Text = "Polygon";
            // 
            // squareToolStripMenuItem
            // 
            squareToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pointToolStripMenuItem1, lineToolStripMenuItem1 });
            squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            squareToolStripMenuItem.Size = new Size(183, 28);
            squareToolStripMenuItem.Text = "Square";
            // 
            // pointToolStripMenuItem1
            // 
            pointToolStripMenuItem1.Name = "pointToolStripMenuItem1";
            pointToolStripMenuItem1.Size = new Size(141, 28);
            pointToolStripMenuItem1.Text = "Point";
            // 
            // lineToolStripMenuItem1
            // 
            lineToolStripMenuItem1.Name = "lineToolStripMenuItem1";
            lineToolStripMenuItem1.Size = new Size(141, 28);
            lineToolStripMenuItem1.Text = "Line";
            // 
            // circleToolStripMenuItem
            // 
            circleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pointToolStripMenuItem2, lineToolStripMenuItem2 });
            circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            circleToolStripMenuItem.Size = new Size(183, 28);
            circleToolStripMenuItem.Text = "Circle";
            // 
            // pointToolStripMenuItem2
            // 
            pointToolStripMenuItem2.Name = "pointToolStripMenuItem2";
            pointToolStripMenuItem2.Size = new Size(141, 28);
            pointToolStripMenuItem2.Text = "Point";
            // 
            // lineToolStripMenuItem2
            // 
            lineToolStripMenuItem2.Name = "lineToolStripMenuItem2";
            lineToolStripMenuItem2.Size = new Size(141, 28);
            lineToolStripMenuItem2.Text = "Line";
            // 
            // bounsToolStripMenuItem
            // 
            bounsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { happyFaceToolStripMenuItem, sadFaceToolStripMenuItem });
            bounsToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            bounsToolStripMenuItem.Name = "bounsToolStripMenuItem";
            bounsToolStripMenuItem.Size = new Size(79, 28);
            bounsToolStripMenuItem.Text = "Bonus";
            bounsToolStripMenuItem.Click += bounsToolStripMenuItem_Click;
            // 
            // happyFaceToolStripMenuItem
            // 
            happyFaceToolStripMenuItem.Name = "happyFaceToolStripMenuItem";
            happyFaceToolStripMenuItem.Size = new Size(198, 28);
            happyFaceToolStripMenuItem.Text = "Happy Face";
            happyFaceToolStripMenuItem.Click += happyFaceToolStripMenuItem_Click;
            // 
            // sadFaceToolStripMenuItem
            // 
            sadFaceToolStripMenuItem.Name = "sadFaceToolStripMenuItem";
            sadFaceToolStripMenuItem.Size = new Size(198, 28);
            sadFaceToolStripMenuItem.Text = "Sad Face";
            sadFaceToolStripMenuItem.Click += sadFaceToolStripMenuItem_Click;
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.FromArgb(44, 44, 42);
            leftPanel.Controls.Add(label12);
            leftPanel.Controls.Add(label11);
            leftPanel.Controls.Add(EraserTool);
            leftPanel.Controls.Add(FillingTool);
            leftPanel.Controls.Add(CircleTool);
            leftPanel.Controls.Add(EllipseTool);
            leftPanel.Controls.Add(CurveTool);
            leftPanel.Controls.Add(LineTool);
            leftPanel.Location = new Point(0, 47);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(75, 763);
            leftPanel.TabIndex = 1;
            leftPanel.Paint += panel1_Paint;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Akira Expanded", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Gray;
            label12.Location = new Point(15, 156);
            label12.Name = "label12";
            label12.Size = new Size(46, 14);
            label12.TabIndex = 23;
            label12.Text = "___";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Akira Expanded", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.Gray;
            label11.Location = new Point(15, 395);
            label11.Name = "label11";
            label11.Size = new Size(46, 14);
            label11.TabIndex = 22;
            label11.Text = "___";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EraserTool
            // 
            EraserTool.BackgroundImageLayout = ImageLayout.Center;
            EraserTool.Image = (Image)resources.GetObject("EraserTool.Image");
            EraserTool.Location = new Point(18, 503);
            EraserTool.Name = "EraserTool";
            EraserTool.Padding = new Padding(5);
            EraserTool.Size = new Size(40, 40);
            EraserTool.SizeMode = PictureBoxSizeMode.StretchImage;
            EraserTool.TabIndex = 6;
            EraserTool.TabStop = false;
            // 
            // FillingTool
            // 
            FillingTool.BackgroundImageLayout = ImageLayout.Center;
            FillingTool.Image = (Image)resources.GetObject("FillingTool.Image");
            FillingTool.Location = new Point(18, 444);
            FillingTool.Name = "FillingTool";
            FillingTool.Padding = new Padding(5);
            FillingTool.Size = new Size(40, 40);
            FillingTool.SizeMode = PictureBoxSizeMode.StretchImage;
            FillingTool.TabIndex = 5;
            FillingTool.TabStop = false;
            // 
            // CircleTool
            // 
            CircleTool.BackgroundImageLayout = ImageLayout.Center;
            CircleTool.Image = (Image)resources.GetObject("CircleTool.Image");
            CircleTool.Location = new Point(18, 268);
            CircleTool.Name = "CircleTool";
            CircleTool.Padding = new Padding(5);
            CircleTool.Size = new Size(40, 40);
            CircleTool.SizeMode = PictureBoxSizeMode.StretchImage;
            CircleTool.TabIndex = 3;
            CircleTool.TabStop = false;
            // 
            // EllipseTool
            // 
            EllipseTool.BackgroundImageLayout = ImageLayout.Center;
            EllipseTool.Image = (Image)resources.GetObject("EllipseTool.Image");
            EllipseTool.Location = new Point(18, 340);
            EllipseTool.Name = "EllipseTool";
            EllipseTool.Padding = new Padding(5);
            EllipseTool.Size = new Size(40, 40);
            EllipseTool.SizeMode = PictureBoxSizeMode.StretchImage;
            EllipseTool.TabIndex = 2;
            EllipseTool.TabStop = false;
            EllipseTool.Click += pictureBox3_Click;
            // 
            // CurveTool
            // 
            CurveTool.BackgroundImageLayout = ImageLayout.Center;
            CurveTool.Image = (Image)resources.GetObject("CurveTool.Image");
            CurveTool.Location = new Point(18, 195);
            CurveTool.Name = "CurveTool";
            CurveTool.Padding = new Padding(5);
            CurveTool.Size = new Size(40, 40);
            CurveTool.SizeMode = PictureBoxSizeMode.StretchImage;
            CurveTool.TabIndex = 1;
            CurveTool.TabStop = false;
            CurveTool.Click += pictureBox2_Click;
            // 
            // LineTool
            // 
            LineTool.BackgroundImageLayout = ImageLayout.Center;
            LineTool.Image = (Image)resources.GetObject("LineTool.Image");
            LineTool.Location = new Point(17, 113);
            LineTool.Name = "LineTool";
            LineTool.Padding = new Padding(5);
            LineTool.Size = new Size(40, 40);
            LineTool.SizeMode = PictureBoxSizeMode.StretchImage;
            LineTool.TabIndex = 0;
            LineTool.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 44, 42);
            panel1.Controls.Add(countShapes);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(Ycoords);
            panel1.Controls.Add(Xcoords);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(color10);
            panel1.Controls.Add(color9);
            panel1.Controls.Add(color8);
            panel1.Controls.Add(color7);
            panel1.Controls.Add(color6);
            panel1.Controls.Add(color5);
            panel1.Controls.Add(color4);
            panel1.Controls.Add(color3);
            panel1.Controls.Add(color2);
            panel1.Controls.Add(color1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(colorPreview);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(CToolName);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(1119, 47);
            panel1.Name = "panel1";
            panel1.Size = new Size(379, 763);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint_1;
            // 
            // countShapes
            // 
            countShapes.AutoSize = true;
            countShapes.Font = new Font("Georgia", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            countShapes.ForeColor = Color.FromArgb(128, 112, 216);
            countShapes.Location = new Point(255, 704);
            countShapes.Name = "countShapes";
            countShapes.Size = new Size(38, 39);
            countShapes.TabIndex = 21;
            countShapes.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(129, 130, 130);
            label10.Location = new Point(35, 716);
            label10.Name = "label10";
            label10.Size = new Size(159, 24);
            label10.TabIndex = 20;
            label10.Text = "SHAPES DRAWN";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Akira Expanded", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(58, 58, 56);
            label9.Location = new Point(31, 663);
            label9.Name = "label9";
            label9.Size = new Size(325, 21);
            label9.TabIndex = 19;
            label9.Text = "_______________";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Ycoords
            // 
            Ycoords.AutoSize = true;
            Ycoords.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Ycoords.ForeColor = Color.FromArgb(129, 130, 130);
            Ycoords.Location = new Point(193, 611);
            Ycoords.Name = "Ycoords";
            Ycoords.Size = new Size(22, 24);
            Ycoords.TabIndex = 18;
            Ycoords.Text = "--";
            // 
            // Xcoords
            // 
            Xcoords.AutoSize = true;
            Xcoords.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Xcoords.ForeColor = Color.FromArgb(129, 130, 130);
            Xcoords.Location = new Point(57, 611);
            Xcoords.Name = "Xcoords";
            Xcoords.Size = new Size(22, 24);
            Xcoords.TabIndex = 17;
            Xcoords.Text = "--";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(129, 130, 130);
            label8.Location = new Point(168, 611);
            label8.Name = "label8";
            label8.Size = new Size(27, 24);
            label8.TabIndex = 16;
            label8.Text = "Y:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(129, 130, 130);
            label7.Location = new Point(31, 611);
            label7.Name = "label7";
            label7.Size = new Size(28, 24);
            label7.TabIndex = 15;
            label7.Text = "X:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(129, 130, 130);
            label6.Location = new Point(31, 564);
            label6.Name = "label6";
            label6.Size = new Size(160, 24);
            label6.TabIndex = 14;
            label6.Text = "MOUSE COORDS";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Akira Expanded", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(58, 58, 56);
            label5.Location = new Point(31, 512);
            label5.Name = "label5";
            label5.Size = new Size(325, 21);
            label5.TabIndex = 13;
            label5.Text = "_______________";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // color10
            // 
            color10.BackColor = Color.Black;
            color10.Location = new Point(308, 430);
            color10.Name = "color10";
            color10.Size = new Size(55, 55);
            color10.TabIndex = 10;
            color10.Click += color10_Click;
            // 
            // color9
            // 
            color9.BackColor = Color.FromArgb(66, 165, 245);
            color9.Location = new Point(308, 354);
            color9.Name = "color9";
            color9.Size = new Size(55, 55);
            color9.TabIndex = 9;
            color9.Click += color9_Click;
            // 
            // color8
            // 
            color8.BackColor = Color.Gray;
            color8.Location = new Point(238, 430);
            color8.Name = "color8";
            color8.Size = new Size(55, 55);
            color8.TabIndex = 12;
            color8.Click += color8_Click;
            // 
            // color7
            // 
            color7.BackColor = Color.White;
            color7.Location = new Point(168, 430);
            color7.Name = "color7";
            color7.Size = new Size(55, 55);
            color7.TabIndex = 11;
            color7.Click += color7_Click;
            // 
            // color6
            // 
            color6.BackColor = Color.FromArgb(214, 39, 40);
            color6.Location = new Point(96, 430);
            color6.Name = "color6";
            color6.Size = new Size(55, 55);
            color6.TabIndex = 10;
            color6.Click += color6_Click;
            // 
            // color5
            // 
            color5.BackColor = Color.FromArgb(249, 170, 30);
            color5.Location = new Point(26, 430);
            color5.Name = "color5";
            color5.Size = new Size(55, 55);
            color5.TabIndex = 9;
            color5.Click += color5_Click;
            // 
            // color4
            // 
            color4.BackColor = Color.FromArgb(238, 78, 113);
            color4.Location = new Point(238, 354);
            color4.Name = "color4";
            color4.Size = new Size(55, 55);
            color4.TabIndex = 8;
            color4.Click += color4_Click;
            // 
            // color3
            // 
            color3.BackColor = Color.FromArgb(239, 119, 59);
            color3.Location = new Point(168, 354);
            color3.Name = "color3";
            color3.Size = new Size(55, 55);
            color3.TabIndex = 7;
            color3.Click += color3_Click;
            // 
            // color2
            // 
            color2.BackColor = Color.FromArgb(46, 204, 113);
            color2.Location = new Point(96, 354);
            color2.Name = "color2";
            color2.Size = new Size(55, 55);
            color2.TabIndex = 6;
            color2.Click += color2_Click;
            // 
            // color1
            // 
            color1.BackColor = Color.FromArgb(128, 112, 216);
            color1.Location = new Point(26, 354);
            color1.Name = "color1";
            color1.Size = new Size(55, 55);
            color1.TabIndex = 5;
            color1.Click += color1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Georgia", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(128, 112, 216);
            label4.Location = new Point(111, 268);
            label4.Name = "label4";
            label4.Size = new Size(118, 35);
            label4.TabIndex = 5;
            label4.Text = "Purble";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label4_Click;
            // 
            // colorPreview
            // 
            colorPreview.BackColor = Color.FromArgb(128, 112, 216);
            colorPreview.Location = new Point(26, 248);
            colorPreview.Name = "colorPreview";
            colorPreview.Size = new Size(70, 70);
            colorPreview.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(129, 130, 130);
            label3.Location = new Point(23, 195);
            label3.Name = "label3";
            label3.Size = new Size(73, 24);
            label3.TabIndex = 3;
            label3.Text = "COLOR";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Akira Expanded", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(58, 58, 56);
            label2.Location = new Point(31, 146);
            label2.Name = "label2";
            label2.Size = new Size(325, 21);
            label2.TabIndex = 2;
            label2.Text = "_______________";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            // 
            // CToolName
            // 
            CToolName.AutoSize = true;
            CToolName.Font = new Font("Georgia", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CToolName.ForeColor = Color.FromArgb(157, 161, 231);
            CToolName.Location = new Point(89, 74);
            CToolName.Name = "CToolName";
            CToolName.Size = new Size(208, 64);
            CToolName.TabIndex = 1;
            CToolName.Text = "ACTIVE \r\nALGORITHM";
            CToolName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(129, 130, 130);
            label1.Location = new Point(100, 27);
            label1.Name = "label1";
            label1.Size = new Size(193, 24);
            label1.TabIndex = 0;
            label1.Text = "ACTIVE ALGORITHM";
            label1.Click += label1_Click;
            // 
            // drawBox
            // 
            drawBox.Image = (Image)resources.GetObject("drawBox.Image");
            drawBox.Location = new Point(75, 47);
            drawBox.Name = "drawBox";
            drawBox.Size = new Size(1044, 763);
            drawBox.SizeMode = PictureBoxSizeMode.StretchImage;
            drawBox.TabIndex = 3;
            drawBox.TabStop = false;
            drawBox.MouseDown += drawBox_MouseDown;
            drawBox.MouseMove += drawBox_MouseMove;
            drawBox.MouseUp += drawBox_MouseUp;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(35, 35, 35);
            panel3.Controls.Add(label13);
            panel3.Location = new Point(0, 809);
            panel3.Name = "panel3";
            panel3.Size = new Size(1498, 35);
            panel3.TabIndex = 5;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Akira Expanded", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.FromArgb(129, 130, 130);
            label13.Location = new Point(568, 10);
            label13.Name = "label13";
            label13.Size = new Size(350, 16);
            label13.TabIndex = 22;
            label13.Text = "DESIGNED BY ABDALLAH GAMAL";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 24);
            ClientSize = new Size(1498, 844);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(drawBox);
            Controls.Add(leftPanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EraserTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)FillingTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)CircleTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)EllipseTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)CurveTool).EndInit();
            ((System.ComponentModel.ISupportInitialize)LineTool).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)drawBox).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private ToolStripMenuItem linesToolStripMenuItem;
        private ToolStripMenuItem circlesToolStripMenuItem;
        private ToolStripMenuItem ellipseToolStripMenuItem;
        private ToolStripMenuItem curvesToolStripMenuItem;
        private ToolStripMenuItem fillingToolStripMenuItem;
        private ToolStripMenuItem clippingToolStripMenuItem;
        private Panel leftPanel;
        private Panel panel1;
        private PictureBox drawBox;
        private Label label1;
        private Label CToolName;
        private Label label2;
        private Label label3;
        private Panel colorPreview;
        private Label label4;
        private Panel color8;
        private Panel color7;
        private Panel color6;
        private Panel color5;
        private Panel color4;
        private Panel color3;
        private Panel color2;
        private Panel color1;
        private Label label5;
        private Panel color10;
        private Panel color9;
        private Label Ycoords;
        private Label Xcoords;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label10;
        private Label label9;
        private Label countShapes;
        private PictureBox LineTool;
        private PictureBox CurveTool;
        private PictureBox FillingTool;
        private PictureBox CircleTool;
        private PictureBox EllipseTool;
        private Label label12;
        private Label label11;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem dDAToolStripMenuItem;
        private ToolStripMenuItem bresenhamToolStripMenuItem;
        private ToolStripMenuItem parametricToolStripMenuItem;
        private ToolStripMenuItem directToolStripMenuItem;
        private ToolStripMenuItem polarToolStripMenuItem;
        private ToolStripMenuItem itrativePolarToolStripMenuItem;
        private ToolStripMenuItem bresenhamToolStripMenuItem1;
        private ToolStripMenuItem modifiedBresenhamToolStripMenuItem;
        private ToolStripMenuItem directEllipseToolStripMenuItem;
        private ToolStripMenuItem polarEllipseToolStripMenuItem;
        private ToolStripMenuItem midpointEllipseToolStripMenuItem;
        private ToolStripMenuItem hermiteToolStripMenuItem;
        private ToolStripMenuItem cardinalSplineToolStripMenuItem;
        private ToolStripMenuItem convexToolStripMenuItem;
        private ToolStripMenuItem nonConvexToolStripMenuItem;
        private ToolStripMenuItem fillingFloodToolStripMenuItem;
        private ToolStripMenuItem nonFillingFloodToolStripMenuItem;
        private ToolStripMenuItem quraterCircleFillingToolStripMenuItem;
        private ToolStripMenuItem quraterCircleFillingLinesToolStripMenuItem;
        private ToolStripMenuItem hermiteToolStripMenuItem1;
        private ToolStripMenuItem bezierHorToolStripMenuItem;
        private ToolStripMenuItem whiteBackgroundToolStripMenuItem;
        private ToolStripMenuItem blackBackgroundToolStripMenuItem;
        private ToolStripMenuItem whiteToolStripMenuItem;
        private ToolStripMenuItem blackToolStripMenuItem;
        private ToolStripMenuItem crossToolStripMenuItem;
        private ToolStripMenuItem arrowToolStripMenuItem;
        private ToolStripMenuItem handToolStripMenuItem;
        private PictureBox EraserTool;
        private Panel panel3;
        private Label label13;
        private ToolStripMenuItem rectangleToolStripMenuItem;
        private ToolStripMenuItem pointToolStripMenuItem;
        private ToolStripMenuItem lineToolStripMenuItem;
        private ToolStripMenuItem polygonToolStripMenuItem;
        private ToolStripMenuItem squareToolStripMenuItem;
        private ToolStripMenuItem pointToolStripMenuItem1;
        private ToolStripMenuItem lineToolStripMenuItem1;
        private ToolStripMenuItem circleToolStripMenuItem;
        private ToolStripMenuItem pointToolStripMenuItem2;
        private ToolStripMenuItem lineToolStripMenuItem2;
        private ToolStripMenuItem bounsToolStripMenuItem;
        private ToolStripMenuItem happyFaceToolStripMenuItem;
        private ToolStripMenuItem sadFaceToolStripMenuItem;
    }
}
