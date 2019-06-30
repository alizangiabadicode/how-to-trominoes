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
using System.IO;
namespace howto_trominoes
{
    public partial class Form1 : Form
    {
        static int widthbycheckbox;
        public Form1()
        {
            widthbycheckbox = 12;

            InitializeComponent();
        }
        private static bool @null = true;
        private static float clickedx, clickedy;
        private enum Quadrants { NW, NE, SE, SW };
        private int SquaresPerSide = 0;
        private float SquareWidth, BoardWidth, Xmin, Ymin;
        private int MissingX, MissingY;
        private List<PointF[]> Chairs;
        private void Form1_Load(object sender, EventArgs e)
        {
            SquareWidth = widthbycheckbox;
            MakeBoard();
        }



        private void btnTile_Click(object sender, EventArgs e)
        {
            var date1 = DateTime.Now;
            SquareWidth = widthbycheckbox;
            MakeBoard();
            var date2 = DateTime.Now;
            time.Text = (date2 - date1).Milliseconds.ToString();
        }



        private void MakeBoard()
        {
            int n = int.Parse(txtN.Text);
            SquaresPerSide = (int)Math.Pow(2, n);
            //SquareWidth = float.Parse(((picBoard.ClientSize.Width * 1.0) / SquaresPerSide).ToString());
            //float hgt = float.Parse(((picBoard.ClientSize.Height * 1.0) / SquaresPerSide).ToString());
            //if (SquareWidth > hgt) SquareWidth = hgt;
            //SquareWidth = widthbycheckbox;
            BoardWidth = SquareWidth * SquaresPerSide;
            Xmin = 0;
            Ymin = 0;
            if (@null)
            {
                MissingX = 0;
                MissingY = 0;
            }
            else
            {
                float s = 0;
                clickedx -= Xmin;
                for (int i = 0; ; i++)
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
                for (int j = 0; ; j++)
                {
                    s += SquareWidth;
                    if (s > clickedy)
                    {
                        MissingY = j;
                        break;
                    }
                }
            }
            Chairs = new List<PointF[]>();
            SolveBoard(0, SquaresPerSide - 1, 0, SquaresPerSide - 1, MissingX, MissingY);
            picBoard.Refresh();
        }

        private void picBoard_Click(object sender, EventArgs e)
        {

        }

        private void picBoard_Paint_1(object sender, PaintEventArgs e)
        {
            Bitmap bt = new Bitmap((int)BoardWidth, (int)(BoardWidth));
            using (Graphics g = Graphics.FromImage(bt))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (PointF[] p in Chairs)
                {
                    g.DrawPolygon(Pens.Black, p);
                }
                float x = Xmin + MissingX * SquareWidth;
                float y = Ymin + MissingY * SquareWidth;
                g.FillRectangle(Brushes.Red, x, y, SquareWidth, SquareWidth);
            }
            picBoard.Image = bt;
            //bt.Save(@"D:\terme4\tarahi\imagetromino\tromino.jpg");
            //e.Graphics.Clear(picBoard.BackColor);
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //foreach (PointF[] points in Chairs)
            //{
            //    e.Graphics.DrawPolygon(Pens.Red, points);
            //}
            //float x = Xmin + MissingX * SquareWidth;
            //float y = Ymin + MissingY * SquareWidth;
            //e.Graphics.FillRectangle(Brushes.Black,
            //    x, y, SquareWidth, SquareWidth);

        }

        private void picBoard_MouseDown(object sender, MouseEventArgs e)
        {
            @null = false;
            clickedx = e.X;
            clickedy = e.Y;
            MakeBoard();

        }


        private void picBoard_MouseClick(object sender, MouseEventArgs e)
        {
            @null = false;
            clickedx = e.X;
            clickedy = e.Y;
            MakeBoard();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                widthbycheckbox = 12;
                checkboxchagned(checkBox1);
            }
            else
            {
                checkBox1.Checked = true;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                widthbycheckbox = 6;
                checkboxchagned(checkBox2);
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                widthbycheckbox = 3;
                checkboxchagned(checkBox3);
            }
            else
            {
                checkBox3.Checked = true;
            }

        }

        private void SolveBoard(int imin, int imax, int jmin, int jmax, int imissing, int jmissing)
        {
            if (imax - imin == 1)
            {
                Chairs.Add(MakeChair(imin, imax, jmin, jmax, imissing, jmissing));
                return;
            }
            int imid = (imin + imax) / 2;
            int jmid = (jmin + jmax) / 2;
            switch (QuadrantToIgnore(imin, imax, jmin, jmax, imissing, jmissing))
            {
                case Quadrants.NW:
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid, jmid));
                    SolveBoard(imin, imid, jmin, jmid, imissing, jmissing);
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                    break;
                case Quadrants.NE:
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid));
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                    SolveBoard(imid + 1, imax, jmin, jmid, imissing, jmissing);
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                    break;
                case Quadrants.SE:
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid + 1));
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imissing, jmissing);
                    SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                    break;
                case Quadrants.SW:
                    Chairs.Add(MakeChair(imid, imid + 1, jmid, jmid + 1, imid, jmid + 1));
                    SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                    SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                    SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                    SolveBoard(imin, imid, jmid + 1, jmax, imissing, jmissing);
                    break;
            }
        }




        private PointF[] MakeChair(int imin, int imax, int jmin, int jmax, int imissing, int jmissing)
        {
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

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {
            //Bitmap bt = new Bitmap((int)(Xmin + SquareWidth+5f), (int)(Ymin + SquareWidth+5f));
            //using(Graphics g = Graphics.FromImage(bt))
            //{
            //    g.SmoothingMode = SmoothingMode.AntiAlias;
            //    foreach (PointF[] p in Chairs)
            //    {
            //        g.DrawPolygon(Pens.Red, p);
            //    }
            //    float x = Xmin + MissingX * SquareWidth;
            //    float y = Ymin + MissingY * SquareWidth;
            //    g.FillRectangle(Brushes.Black, x, y, SquareWidth, SquareWidth);
            //}
            //picBoard.Image = bt;
            //bt.Save(@"D:\terme4\tarahi\imagetromino\tromino.jpg");
            //if (File.Exists(@"D:\terme4\tarahi\imagetromino\tromino.jpg"))
            //{
            //    File.Delete(@"D:\terme4\tarahi\imagetromino\tromino.jpg");
            //}
            //FileStream fs = new FileStream(@"D:\terme4\tarahi\imagetromino\tromino.jpg", FileMode.Create);
            //using(StreamWriter )
            e.Graphics.Clear(picBoard.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (PointF[] points in Chairs)
            {
                e.Graphics.DrawPolygon(Pens.Black, points);
            }
            float x = Xmin + MissingX * SquareWidth;
            float y = Ymin + MissingY * SquareWidth;
            e.Graphics.FillRectangle(Brushes.Red,
                x, y, SquareWidth, SquareWidth);
        }

        private Quadrants QuadrantToIgnore(int imin, int imax, int jmin, int jmax,
            int imissing, int jmissing)
        {
            int imid = (imin + imax) / 2;
            int jmid = (jmin + jmax) / 2;
            if (imissing <= imid)
            {
                if (jmissing <= jmid) return Quadrants.NW;
                return Quadrants.SW;
            }
            else
            {
                if (jmissing <= jmid) return Quadrants.NE;
                return Quadrants.SE;
            }
        }

        public void checkboxchagned(CheckBox cc)
        {
            foreach (Control c in panel2.Controls)
            {
                if (c is CheckBox)
                {
                    if (!((CheckBox)c == cc))
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
                
            }
        }
    }
}









public class chair
{
    public static Brush[] brushes =
    {
        Brushes.Red,Brushes.Blue,Brushes.Yellow
    };

    public int number;
    public int bgbrushnum = 0;
    public List<Point> squars = new List<Point>();
    public PointF[] points;
    public List<chair> neighbors;


    public void draw(Graphics g,bool b)
    {
        if(points == null)
        {
            return;
        }


        g.FillPolygon(brushes[bgbrushnum], points);
        g.DrawPolygon(Pens.Black,points);

        if (b)
        {
            using (Font font = new Font("Times New Roman", 10))
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    g.DrawString(number.ToString(), font, Brushes.Black, center(), sf);
                }
            }
        }
    }


    public PointF center()
    {
        float x = 0;
        float y = 0;
        foreach (PointF p in points)
        {
            x += p.X;
            y += p.Y;
        }
        return new PointF(x / points.Length, y / points.Length);// nmifmmesh!
    }


    public static bool areneighburs(Point p1,Point p2)
    {
        if(p1.X == p2.X && Math.Abs(p2.Y - p1.Y) == 1)
        {
            return true;
        }
        if(p1.Y == p2.Y && Math.Abs(p1.X - p2.Y) == 1)
        {
            return true;
        }
        return false;
    }


    public bool isneigburs(chair c)
    {
        foreach(Point p1 in squars)
        {
            foreach (Point p2 in c.squars)
            {
                if (areneighburs(p1, p2))
                {
                    return true;
                }
            }
        }
        return false;
    }




    public bool colorallowd(int num)
    {
        foreach(chair c in neighbors)
        {
            if(c.bgbrushnum == num)
            {
                return false;
            }
        }
        return true;
    }





}