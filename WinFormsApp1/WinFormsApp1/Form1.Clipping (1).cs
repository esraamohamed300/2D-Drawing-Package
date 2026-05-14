// Form1.Clipping.cs
// Drop this file into your WinFormsApp1 project alongside Form1.cs.
// It is a partial class that wires the Clipping menu to the algorithms
// in Algorithms/ClippingAlgorithms.cs
//
// HOW CLICKS WORK (matches your existing pattern):
//   • The mode is set via the menu → SetClipAlgorithm()
//   • Mouse clicks are collected in clickPoints (same list used by fills)
//   • When the required number of clicks is reached the algorithm runs automatically
//   • The result is printed to the Console (as required by the assignment)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Algorithms;

namespace WinFormsApp1
{
    public partial class Form1
    {
        // ─────────────────────────────────────────────────────────────────────
        //  Clipping state
        // ─────────────────────────────────────────────────────────────────────
        private enum ClipAlgorithm
        {
            None,
            RectPoint,      // 3 clicks: point, TL corner, BR corner
            RectLine,       // 4 clicks: P1, P2, TL corner, BR corner
            RectPolygon,    // N≥3 clicks + right-click to finish, then 2 corners
            SquarePoint,    // 3 clicks: point, centre, any edge pt (defines half)
            SquareLine,     // 4 clicks: P1, P2, centre, any edge pt (defines half)
            CirclePoint,    // 3 clicks: point, centre, edge pt (defines radius)
            CircleLine      // 4 clicks: P1, P2, centre, edge pt (defines radius)
        }

        private ClipAlgorithm currentClipAlgo = ClipAlgorithm.None;

        // Polygon for RectPolygon: vertices collected before the rect corners
        private readonly List<Point> clipPolyVertices = new();
        private bool clipCollectingPolyVerts = false;

        // Required click counts for each mode
        private static int ClipClicksNeeded(ClipAlgorithm algo) => algo switch
        {
            ClipAlgorithm.RectPoint   => 3,
            ClipAlgorithm.RectLine    => 4,
            ClipAlgorithm.RectPolygon => 2,   // just the 2 rect corners; poly uses clipPolyVertices
            ClipAlgorithm.SquarePoint => 3,
            ClipAlgorithm.SquareLine  => 4,
            ClipAlgorithm.CirclePoint => 3,
            ClipAlgorithm.CircleLine  => 4,
            _                         => 0
        };

        // ─────────────────────────────────────────────────────────────────────
        //  Wire menu items – call this from the Form1 constructor ONCE
        // ─────────────────────────────────────────────────────────────────────
        private void WireClippingMenu()
        {
            // Rectangle sub-menu
            pointToolStripMenuItem.Click   += (s, e) => SetClipAlgorithm(ClipAlgorithm.RectPoint,   "Rect Point Clip");
            lineToolStripMenuItem.Click    += (s, e) => SetClipAlgorithm(ClipAlgorithm.RectLine,    "Rect Line Clip");
            polygonToolStripMenuItem.Click += (s, e) => SetClipAlgorithm(ClipAlgorithm.RectPolygon, "Rect Polygon Clip");

            // Square sub-menu
            pointToolStripMenuItem1.Click  += (s, e) => SetClipAlgorithm(ClipAlgorithm.SquarePoint, "Square Point Clip");
            lineToolStripMenuItem1.Click   += (s, e) => SetClipAlgorithm(ClipAlgorithm.SquareLine,  "Square Line Clip");

            // Circle sub-menu
            pointToolStripMenuItem2.Click  += (s, e) => SetClipAlgorithm(ClipAlgorithm.CirclePoint, "Circle Point Clip");
            lineToolStripMenuItem2.Click   += (s, e) => SetClipAlgorithm(ClipAlgorithm.CircleLine,  "Circle Line Clip");
        }

        // ─────────────────────────────────────────────────────────────────────
        //  SetClipAlgorithm
        // ─────────────────────────────────────────────────────────────────────
        private void SetClipAlgorithm(ClipAlgorithm algo, string name)
        {
            ResetAllTools();

            currentClipAlgo = algo;
            clipPolyVertices.Clear();
            clipCollectingPolyVerts = (algo == ClipAlgorithm.RectPolygon);

            collectingPoints = true;   // re-use the existing multi-click collection flag
            clickPoints.Clear();

            // Update UI label
            CToolName.Text = "Clip:\n" + name;

            // Print instructions to Console (assignment requirement)
            string instructions = algo switch
            {
                ClipAlgorithm.RectPoint   => "Click 1: Point | Click 2: Top-Left of rect | Click 3: Bottom-Right of rect",
                ClipAlgorithm.RectLine    => "Click 1: Line start | Click 2: Line end | Click 3: Top-Left | Click 4: Bottom-Right",
                ClipAlgorithm.RectPolygon => "Left-click: add polygon vertices | Right-click: finish polygon | Then click Top-Left & Bottom-Right of rect",
                ClipAlgorithm.SquarePoint => "Click 1: Point | Click 2: Square centre | Click 3: Any edge point (defines half-size)",
                ClipAlgorithm.SquareLine  => "Click 1: Line start | Click 2: Line end | Click 3: Square centre | Click 4: Any edge point",
                ClipAlgorithm.CirclePoint => "Click 1: Point | Click 2: Circle centre | Click 3: Any point on circle edge",
                ClipAlgorithm.CircleLine  => "Click 1: Line start | Click 2: Line end | Click 3: Circle centre | Click 4: Any point on circle edge",
                _                         => ""
            };
            Console.WriteLine($"[Clipping] Mode: {name}");
            Console.WriteLine($"[Clipping] {instructions}");

            MessageBox.Show(instructions, $"Clipping – {name}",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  HandleClipClick  –  called from drawBox_MouseDown
        //  Returns true if the click was consumed by clipping logic.
        // ─────────────────────────────────────────────────────────────────────
        private bool HandleClipClick(MouseEventArgs e)
        {
            if (currentClipAlgo == ClipAlgorithm.None) return false;

            Point p = ToBitmapCoords(e.X, e.Y);

            // ── Polygon mode: right-click finishes vertex collection ──────────
            if (currentClipAlgo == ClipAlgorithm.RectPolygon && clipCollectingPolyVerts)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (clipPolyVertices.Count < 3)
                    {
                        Console.WriteLine("[Clipping] Need at least 3 polygon vertices!");
                        return true;
                    }
                    clipCollectingPolyVerts = false;
                    Console.WriteLine($"[Clipping] Polygon finished ({clipPolyVertices.Count} vertices). Now click Top-Left of rect.");
                    CToolName.Text = "Clip Poly:\nClick TL rect";
                    return true;
                }

                // Left-click: add vertex
                clipPolyVertices.Add(p);
                // Draw a small dot so the user can see each vertex
                ClippingHelpers.DrawDot(canvas, p.X, p.Y, Color.Gray, 5);
                // Draw edge to previous vertex
                if (clipPolyVertices.Count >= 2)
                {
                    Point prev = clipPolyVertices[clipPolyVertices.Count - 2];
                    DDA.Draw(canvas, prev.X, prev.Y, p.X, p.Y, Color.Gray);
                }
                Console.WriteLine($"[Clipping] Polygon vertex {clipPolyVertices.Count}: ({p.X}, {p.Y}) — Right-click to finish.");
                RefreshCanvas();
                return true;
            }

            // ── All other modes: accumulate in clickPoints ────────────────────
            if (e.Button != MouseButtons.Left) return false;

            clickPoints.Add(p);
            Console.WriteLine($"[Clipping] Click {clickPoints.Count}: ({p.X}, {p.Y})");

            // Update label with progress
            int needed = ClipClicksNeeded(currentClipAlgo);
            CToolName.Text = $"Clip:\n{clickPoints.Count}/{needed} clicks";

            // For polygon mode the 2 rect corners are collected here after poly is done
            int required = (currentClipAlgo == ClipAlgorithm.RectPolygon) ? 2 : needed;

            if (clickPoints.Count >= required)
                RunClipAlgorithm();

            return true;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  RunClipAlgorithm
        // ─────────────────────────────────────────────────────────────────────
        private void RunClipAlgorithm()
        {
            switch (currentClipAlgo)
            {
                // ── Rectangle – Point ─────────────────────────────────────────
                case ClipAlgorithm.RectPoint:
                {
                    Point pt  = clickPoints[0];
                    Point tl  = clickPoints[1];
                    Point br  = clickPoints[2];
                    bool inside = RectPointClipping.Clip(canvas,
                                      pt.X, pt.Y,
                                      tl.X, tl.Y, br.X, br.Y);
                    Console.WriteLine($"[Clipping] Point ({pt.X},{pt.Y}) is {(inside ? "INSIDE" : "OUTSIDE")} the rectangle.");
                    CToolName.Text = $"Rect Point:\n{(inside ? "INSIDE" : "OUTSIDE")}";
                    break;
                }

                // ── Rectangle – Line ─────────────────────────────────────────
                case ClipAlgorithm.RectLine:
                {
                    Point p1 = clickPoints[0], p2 = clickPoints[1];
                    Point tl = clickPoints[2], br = clickPoints[3];
                    bool visible = RectLineClipping.Clip(canvas,
                                       p1.X, p1.Y, p2.X, p2.Y,
                                       tl.X, tl.Y, br.X, br.Y);
                    Console.WriteLine($"[Clipping] Line {(visible ? "CLIPPED and drawn in green" : "is OUTSIDE the rectangle")}.");
                    CToolName.Text = $"Rect Line:\n{(visible ? "Clipped ✓" : "Outside ✗")}";
                    break;
                }

                // ── Rectangle – Polygon ───────────────────────────────────────
                case ClipAlgorithm.RectPolygon:
                {
                    Point tl = clickPoints[0], br = clickPoints[1];
                    var result = RectPolygonClipping.Clip(canvas,
                                     clipPolyVertices,
                                     tl.X, tl.Y, br.X, br.Y);
                    Console.WriteLine($"[Clipping] Polygon clipped to {result.Count} vertices.");
                    CToolName.Text = $"Rect Poly:\n{result.Count} verts";
                    break;
                }

                // ── Square – Point ────────────────────────────────────────────
                case ClipAlgorithm.SquarePoint:
                {
                    Point pt     = clickPoints[0];
                    Point centre = clickPoints[1];
                    Point edge   = clickPoints[2];
                    int half = Math.Max(Math.Abs(edge.X - centre.X),
                                        Math.Abs(edge.Y - centre.Y));
                    bool inside = SquarePointClipping.Clip(canvas,
                                      pt.X, pt.Y,
                                      centre.X, centre.Y, half);
                    Console.WriteLine($"[Clipping] Point ({pt.X},{pt.Y}) is {(inside ? "INSIDE" : "OUTSIDE")} the square. Half={half}");
                    CToolName.Text = $"Square Point:\n{(inside ? "INSIDE" : "OUTSIDE")}";
                    break;
                }

                // ── Square – Line ─────────────────────────────────────────────
                case ClipAlgorithm.SquareLine:
                {
                    Point p1     = clickPoints[0], p2 = clickPoints[1];
                    Point centre = clickPoints[2];
                    Point edge   = clickPoints[3];
                    int half = Math.Max(Math.Abs(edge.X - centre.X),
                                        Math.Abs(edge.Y - centre.Y));
                    bool visible = SquareLineClipping.Clip(canvas,
                                       p1.X, p1.Y, p2.X, p2.Y,
                                       centre.X, centre.Y, half);
                    Console.WriteLine($"[Clipping] Square line {(visible ? "CLIPPED" : "OUTSIDE")}. Half={half}");
                    CToolName.Text = $"Square Line:\n{(visible ? "Clipped ✓" : "Outside ✗")}";
                    break;
                }

                // ── Circle – Point ────────────────────────────────────────────
                case ClipAlgorithm.CirclePoint:
                {
                    Point pt   = clickPoints[0];
                    Point cen  = clickPoints[1];
                    Point edge = clickPoints[2];
                    int r = (int)Math.Round(Math.Sqrt(
                        Math.Pow(edge.X - cen.X, 2) + Math.Pow(edge.Y - cen.Y, 2)));
                    bool inside = CirclePointClipping.Clip(canvas,
                                      pt.X, pt.Y,
                                      cen.X, cen.Y, r);
                    Console.WriteLine($"[Clipping] Point ({pt.X},{pt.Y}) is {(inside ? "INSIDE" : "OUTSIDE")} the circle. r={r}");
                    CToolName.Text = $"Circle Point:\n{(inside ? "INSIDE" : "OUTSIDE")}";
                    break;
                }

                // ── Circle – Line ─────────────────────────────────────────────
                case ClipAlgorithm.CircleLine:
                {
                    Point p1   = clickPoints[0], p2 = clickPoints[1];
                    Point cen  = clickPoints[2];
                    Point edge = clickPoints[3];
                    int r = (int)Math.Round(Math.Sqrt(
                        Math.Pow(edge.X - cen.X, 2) + Math.Pow(edge.Y - cen.Y, 2)));
                    bool visible = CircleLineClipping.Clip(canvas,
                                       p1.X, p1.Y, p2.X, p2.Y,
                                       cen.X, cen.Y, r);
                    Console.WriteLine($"[Clipping] Circle line {(visible ? "CLIPPED" : "OUTSIDE")}. r={r}");
                    CToolName.Text = $"Circle Line:\n{(visible ? "Clipped ✓" : "Outside ✗")}";
                    break;
                }
            }

            IncrementShapeCount();
            RefreshCanvas();

            // Reset for next use (stay in same mode so user can try again)
            clickPoints.Clear();
            clipPolyVertices.Clear();
            clipCollectingPolyVerts = (currentClipAlgo == ClipAlgorithm.RectPolygon);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  ResetClipping – called by ResetAllTools()
        // ─────────────────────────────────────────────────────────────────────
        private void ResetClipping()
        {
            currentClipAlgo = ClipAlgorithm.None;
            clipPolyVertices.Clear();
            clipCollectingPolyVerts = false;
        }
    }
}
