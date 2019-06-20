using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;

namespace howto_trominoes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool @null = true;
        private static float clickedx, clickedy;
        private enum Quadrants { NW, NE, SE, SW };

        private int SquaresPerSide = 0;
        private float SquareWidth, BoardWidth, Xmin, Ymin;

        // The location of the missing square.
        private int MissingX, MissingY;

        private List<PointF[]> Chairs;

        // Make and solve a new board.
        private async void Form1_Load(object sender, EventArgs e)
        {
            MakeBoard();
        }
        private void btnTile_Click(object sender, EventArgs e)
        {
            var date1 = DateTime.Now;
            MakeBoard();
            var date2 = DateTime.Now;
            time.Text = (date2 - date1).Milliseconds.ToString();
            //makeimage();
        }
        private  void MakeBoard()
        {
            int n = int.Parse(txtN.Text);
            SquaresPerSide = (int)Math.Pow(2, n);

            // Set board parameters.
            //SquareWidth = (picBoard.ClientSize.Width - 10f) / SquaresPerSide;
            //float hgt = (picBoard.ClientSize.Height - 10f) / SquaresPerSide;
            //if (SquareWidth > hgt) SquareWidth = hgt;
            SquareWidth = 5;
            BoardWidth = SquareWidth * SquaresPerSide;
            picBoard.Width = picBoard.Height = Convert.ToInt32(BoardWidth + 10f);
            Xmin = (picBoard.ClientSize.Width - BoardWidth) / 2;
            Ymin = (picBoard.ClientSize.Height - BoardWidth) / 2;

            // Pick a random empty square.
            if (@null)
            {
                Random rand = new Random();
                MissingX = rand.Next(SquaresPerSide);
                MissingY = rand.Next(SquaresPerSide);
            }
            else
            {
                float s = 0;
                clickedx -= Xmin;
                for (int i = 0;; i++)
                {
                    s += SquareWidth;
                    if (s > clickedx)
                    {
                        MissingX = i;
                        break;
                    }
                }

                s = 0;
                clickedy -= Ymin;
                for (int j = 0;; j++)
                {
                    s += SquareWidth;
                    if (s > clickedy)
                    {
                        MissingY = j;
                        break;
                    }
                }
            }

            // Solve the board.
            Chairs = new List<PointF[]>();

            SolveBoard(
                0, SquaresPerSide - 1,
                0, SquaresPerSide - 1,
                MissingX, MissingY);

            //makeimage();

            picBoard.Refresh();
        }

        private void save_Click(object sender, EventArgs e)
        {
            picBoard.Image.Save(@"D:\terme4\tarahi\howto_trominoes\aaa.jpeg", ImageFormat.Jpeg);
        }

        public Task makeimage()
        {
            // Redraw.
            Bitmap bt = new Bitmap(picBoard.Width, picBoard.Width);
            using (Graphics g = Graphics.FromImage(bt))
            {
                //g.Clear(bt.BackColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw the chairs.
                foreach (PointF[] points in Chairs)
                {
                    //Thread.Sleep(1000);
                    g.DrawPolygon(Pens.Red, points);

                }

                // Draw the missing square.
                float xx = Xmin + MissingX * SquareWidth;
                float yy = Ymin + MissingY * SquareWidth;
                g.FillRectangle(Brushes.Black,
                    xx, yy, SquareWidth, SquareWidth);
            }
            picBoard.Image = bt;
            bt.Save(@"D:\terme4\tarahi\howto_trominoes\aaa.jpeg", ImageFormat.Jpeg);
            return Task.CompletedTask;


        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void picBoard_MouseClick(object sender, MouseEventArgs e)
        {
            @null = false;
            clickedx = e.X;
            clickedy = e.Y;
            MakeBoard();
        }

        // Solve the board.
        private void SolveBoard(int imin, int imax, int jmin, int jmax,
            int imissing, int jmissing)
        {
            // See if this is a 2x2 square.
            if (imax - imin == 1)
            {
                // It is a 2x2 square. Make its chair.
                Chairs.Add(MakeChair(imin, imax, jmin, jmax,
                    imissing, jmissing));
                return;
            }

            // Not a 2x2 square. Divide into 4 pieces and recurse.
            int imid = (imin + imax) / 2;
            int jmid = (jmin + jmax) / 2;
            switch (QuadrantToIgnore(imin, imax, jmin, jmax,
                imissing, jmissing))
            {
                case Quadrants.NW:
                    // Make the chair in the middle.
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid, jmid));

                    // Recurse.
                    SolveBoard(imin, imid, jmin, jmid, imissing, jmissing);         // NW
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);         // NE
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1); // SE
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);         // SW
                    break;
                case Quadrants.NE:
                    // Make the chair in the middle.
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid));

                    // Recurse.
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);                 // NW
                    SolveBoard(imid + 1, imax, jmin, jmid, imissing, jmissing);     // NE
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1); // SE
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);         // SW
                    break;
                case Quadrants.SE:
                    // Make the chair in the middle.
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid + 1));

                    // Recurse.
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);                 // NW
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);         // NE
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imissing, jmissing); // SE
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);         // SW
                    break;
                case Quadrants.SW:
                    // Make the chair in the middle.
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid, jmid + 1));

                    // Recurse.
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);                 // NW
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);         // NE
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1); // SE
                    SolveBoard(imin, imid, jmid + 1, jmax, imissing, jmissing);     // SW
                    break;
            }
        }

        // Make a chair polygon.
        private PointF[] MakeChair(int imin, int imax,
            int jmin, int jmax, int imissing, int jmissing)
        {
            // Make the initial points.
            float xmin = Xmin + imin * SquareWidth;
            float ymin = Ymin + jmin * SquareWidth;
            PointF[] points =
            {
                new PointF(xmin, ymin),
                new PointF(xmin + SquareWidth, ymin),
                new PointF(xmin + SquareWidth * 2, ymin),
                new PointF(xmin + SquareWidth * 2, ymin + SquareWidth),
                new PointF(xmin + SquareWidth * 2, ymin + SquareWidth * 2),
                new PointF(xmin + SquareWidth, ymin + SquareWidth * 2),
                new PointF(xmin, ymin + SquareWidth * 2),
                new PointF(xmin, ymin + SquareWidth),
            };

            // Push in the appropriate corner.
            PointF middle = new PointF(
                xmin + SquareWidth,
                ymin + SquareWidth);
            switch (QuadrantToIgnore(imin, imax, jmin, jmax,
                imissing, jmissing))
            {
                case Quadrants.NW:
                    points[0] = middle;
                    break;
                case Quadrants.SW:
                    points[6] = middle;
                    break;
                case Quadrants.NE:
                    points[2] = middle;
                    break;
                case Quadrants.SE:
                    points[4] = middle;
                    break;
            }

            return points;
        }

        // Draw the board.
        private  void picBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(picBoard.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the chairs.
            foreach (PointF[] points in Chairs)
            {
                e.Graphics.DrawPolygon(Pens.Red, points);
            }

            // Draw the missing square.
            float x = Xmin + MissingX * SquareWidth;
            float y = Ymin + MissingY * SquareWidth;
            e.Graphics.FillRectangle(Brushes.Black,
                x, y, SquareWidth, SquareWidth);
        }

        // Return the quadrant holding the square to be ignored for this square.
        private Quadrants QuadrantToIgnore(int imin, int imax, int jmin, int jmax,
            int imissing, int jmissing)
        {
            int imid = (imin + imax) / 2;
            int jmid = (jmin + jmax) / 2;
            if (imissing <= imid)      // West.
            {
                if (jmissing <= jmid) return Quadrants.NW;
                return Quadrants.SW;
            }
            else                        // East.
            {
                if (jmissing <= jmid) return Quadrants.NE;
                return Quadrants.SE;
            }
        }
    }
}
