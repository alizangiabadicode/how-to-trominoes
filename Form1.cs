﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace howto_trominoes
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        static Random rnd = new Random();

        static int widthbycheckbox;

        private List<chair> chairs;
        private List<PointF[]> Chairs;

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


        private void Form1_Load(object sender, EventArgs e)
        {
            SquareWidth = widthbycheckbox;
            MakeBoard();
        }

        private void btnTile_Click(object sender, EventArgs e)
        {
            MakeBoard();
        }



        private async void MakeBoard()
        {
            SquareWidth = widthbycheckbox;
            var date1 = DateTime.Now;
            if (!color.Checked)
            {
                int n = int.Parse(txtN.Text);
                SquaresPerSide = (int)Math.Pow(2, n);
                BoardWidth = SquareWidth * SquaresPerSide;
                if (auto.Checked)
                {
                    SquareWidth = float.Parse(((picBoard.ClientSize.Width * 1.0) / SquaresPerSide).ToString());
                    float hgt = float.Parse(((picBoard.ClientSize.Height * 1.0) / SquaresPerSide).ToString());
                    if (SquareWidth > hgt) SquareWidth = hgt;
                }
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
                var date2 = DateTime.Now;
                zaman.Text = (date2 - date1).TotalMilliseconds.ToString() + "ms";

                if (!justgenerate.Checked)
                {
                    Task t = Task.Run(() => method());
                    await Task.WhenAll(t);
                }
            }
            else
            {
                int n = int.Parse(txtN.Text);
                SquaresPerSide = (int)Math.Pow(2, n);
                if (auto.Checked)
                {
                    SquareWidth = float.Parse(((picBoard.ClientSize.Width * 1.0) / SquaresPerSide).ToString());
                    float hgt = float.Parse(((picBoard.ClientSize.Height * 1.0) / SquaresPerSide).ToString());
                    if (SquareWidth > hgt) SquareWidth = hgt;
                }
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
                chairs = new List<chair>();
                SolveBoard(
                    0, SquaresPerSide - 1,
                    0, SquaresPerSide - 1,
                    MissingX, MissingY);
                
                Task tt = Task.Run(() => findcoloring());
                await Task.WhenAll(tt);
                var date2 = DateTime.Now;
                zaman.Text = (date2 - date1).TotalMilliseconds.ToString() + "ms";
                if (!justgenerate.Checked)
                {
                    Task t = Task.Run(() => method());
                    await Task.WhenAll(t);
                }
            }
        }
        public async Task method()
        {
            Bitmap bt = new Bitmap((int)BoardWidth, (int)(BoardWidth));
            using (Graphics g = Graphics.FromImage(bt))
            {
                Random rnd = new Random();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (color.Checked)
                {
                    if (tavakol.Checked)
                    {
                        foreach (chair c in chairs)
                        {
                            c.draw(g, shomare.Checked, rnd);
                            if (slowmotion.Checked)
                            {
                                await Task.Delay(500);
                                c.draw(g, shomare.Checked, rnd);
                                Invoke(new MethodInvoker(() =>
                                {
                                    picBoard.Image = bt;
                                }));
                            }
                            else
                            {
                                c.draw(g, shomare.Checked, rnd);
                            }

                        }
                    }
                    else if (rndcolor.Checked)
                    {
                        foreach (chair c in chairs)
                        {
                            if (slowmotion.Checked)
                            {
                                await Task.Delay(500);
                                c.draw(g, shomare.Checked, c.cc);
                                Invoke(new MethodInvoker(() =>
                                {
                                    picBoard.Image = bt;
                                }));
                            }
                            else
                            {
                                c.draw(g, shomare.Checked, c.cc);
                            }
                        }
                    }
                    else
                    {
                        foreach (chair c in chairs)
                        {
                            if (slowmotion.Checked)
                            {
                                await Task.Delay(500);
                                c.draw(g, shomare.Checked);
                                Invoke(new MethodInvoker(() =>
                                {
                                    picBoard.Image = bt;
                                }));
                            }
                            else
                            {
                                c.draw(g, shomare.Checked);
                            }

                        }
                        //c.draw(g, shomare.Checked, rnd);

                    }
                    float x = MissingX * SquareWidth;
                    float y = MissingY * SquareWidth;
                    g.FillRectangle(Brushes.White, x, y, SquareWidth, SquareWidth);
                }
                else
                {
                    foreach (PointF[] p in Chairs)
                    {
                        if (slowmotion.Checked)
                        {
                            await Task.Delay(500);
                            Invoke(new MethodInvoker(() =>
                            {
                                picBoard.Image = bt;
                            }));

                        }
                        g.DrawPolygon(Pens.Black, p);
                    }
                    float x = MissingX * SquareWidth;
                    float y = MissingY * SquareWidth;
                    g.FillRectangle(Brushes.Green, x, y, SquareWidth, SquareWidth);
                }
            }
            if (!slowmotion.Checked)
            {
                Invoke(new MethodInvoker(() =>
                {
                    picBoard.Image = bt;
                }));
            }

        }

        private void picBoard_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void picBoard_MouseDown(object sender, MouseEventArgs e)
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

        private void checkBox4_Click(object sender, EventArgs e)
        {
            MakeBoard();
        }

        private void SolveBoard(int imin, int imax, int jmin, int jmax, int imissing, int jmissing)
        {
            if (!color.Checked)
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
            else
            {
                if (imax - imin == 1)
                {
                    chairs.Add(MakeChaircolor(imin, imax, jmin, jmax,
                        imissing, jmissing));
                    return;
                }

                int imid = (imin + imax) / 2;
                int jmid = (jmin + jmax) / 2;
                switch (QuadrantToIgnore(imin, imax, jmin, jmax,
                    imissing, jmissing))
                {
                    case Quadrants.NW:
                        chairs.Add(MakeChaircolor(imid, imid + 1, jmid, jmid + 1, imid, jmid));
                        SolveBoard(imin, imid, jmin, jmid, imissing, jmissing);
                        SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                        SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                        SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                        break;
                    case Quadrants.NE:
                        chairs.Add(MakeChaircolor(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid));
                        SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                        SolveBoard(imid + 1, imax, jmin, jmid, imissing, jmissing);
                        SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                        SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                        break;
                    case Quadrants.SE:
                        chairs.Add(MakeChaircolor(imid, imid + 1, jmid, jmid + 1, imid + 1, jmid + 1));
                        SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                        SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                        SolveBoard(imid + 1, imax, jmid + 1, jmax, imissing, jmissing);
                        SolveBoard(imin, imid, jmid + 1, jmax, imid, jmid + 1);
                        break;
                    case Quadrants.SW:
                        chairs.Add(MakeChaircolor(imid, imid + 1, jmid, jmid + 1, imid, jmid + 1));
                        SolveBoard(imin, imid, jmin, jmid, imid, jmid);
                        SolveBoard(imid + 1, imax, jmin, jmid, imid + 1, jmid);
                        SolveBoard(imid + 1, imax, jmid + 1, jmax, imid + 1, jmid + 1);
                        SolveBoard(imin, imid, jmid + 1, jmax, imissing, jmissing);
                        break;
                }
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




        private chair MakeChaircolor(int imin, int imax, int jmin, int jmax, int imissing, int jmissing)
        {
            chair chair = new chair();
            chair.number = chairs.Count;
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
                    chair.squars.Add(new Point(imin, jmax));
                    chair.squars.Add(new Point(imax, jmax));
                    chair.squars.Add(new Point(imax, jmin));
                    break;
                case Quadrants.SW:
                    points[6] = middle;
                    chair.squars.Add(new Point(imin, jmin));
                    chair.squars.Add(new Point(imax, jmin));
                    chair.squars.Add(new Point(imax, jmax));
                    break;
                case Quadrants.NE:
                    points[2] = middle;
                    chair.squars.Add(new Point(imin, jmin));
                    chair.squars.Add(new Point(imin, jmax));
                    chair.squars.Add(new Point(imax, jmax));
                    break;
                case Quadrants.SE:
                    points[4] = middle;
                    chair.squars.Add(new Point(imin, jmin));
                    chair.squars.Add(new Point(imin, jmax));
                    chair.squars.Add(new Point(imax, jmin));
                    break;
            }

            chair.points = points;
            return chair;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bt = null;
            if (!color.Checked)
            {
                bt = new Bitmap((int)BoardWidth, (int)(BoardWidth));
                using (Graphics g = Graphics.FromImage(bt))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    foreach (PointF[] p in Chairs)
                    {
                        g.DrawPolygon(Pens.White, p);
                    }
                    float x = MissingX * SquareWidth;
                    float y = MissingY * SquareWidth;
                    g.FillRectangle(Brushes.Green, x, y, SquareWidth, SquareWidth);
                }
            }
            else
            {
                bt = new Bitmap(picBoard.Image);
            }
            DialogResult res;
            string path;
            using (SaveFileDialog s = new SaveFileDialog())
            {
                s.CheckFileExists = false;
                s.CheckPathExists = true;
                res = s.ShowDialog();
                path = s.FileName;
            }
            if (res == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(path+".png", FileMode.Create))
                {
                    fs.Write(converterDemo(bt), 0, converterDemo(bt).Length);
                }
            }

        }
        public static byte[] converterDemo(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        private void checkBox4_Click_1(object sender, EventArgs e)
        {
            if (auto.Checked)
            {
                // widthbycheckbox = 12;
                checkboxchagned(auto);
            }
            else
            {
                auto.Checked = true;
            }

        }
        private void checkBox4_Click_2(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                widthbycheckbox = 20;
                checkboxchagned(checkBox4);
            }
            else
            {
                checkBox4.Checked = true;
            }

        }

        private void findcoloring()
        {
            try
            {
                if (!tavakol.Checked)
                    findneigburs();

                foreach (chair c in chairs)
                {
                    c.bgbrushnum = -1;
                }
                if (!tavakol.Checked)
                {
                    int n = chair.brushes.Length;
                    if (rndcolor.Checked)
                    {


                        for (int i = 0; i < chairs.Count; i++)
                        {
                            var color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                            if (chairs[i].colorAllowedcolor(color))
                            {
                                chairs[i].cc = color;
                            }
                            else
                            {
                                i--;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < chairs.Count; i++)
                        {
                            chairs[i].bgbrushnum = chairs[i].giveTheRightColor();
                        }
                    }

                }
            }
            catch (Exception)
            {            }
        }
        private void findneigburs()
        {
            int n = chairs.Count;
            foreach (chair c in chairs)
            {
                c.neighbors = new List<chair>();
            }
            for (int i = 0; i < n - 1; i++)
            {
                if (chairs[i].neighbors.Count == 6)
                {
                    continue;
                }
                for (int j = i + 1; j < n; j++)
                {
                    if (chairs[i].neighbors.Count == 6)
                    {
                        break;
                    }

                    if (chairs[i].isneigburs(chairs[j]))
                    {
                        chairs[i].neighbors.Add(chairs[j]);
                        chairs[j].neighbors.Add(chairs[i]);
                        if (chairs[i].neighbors.Count == 6)
                        {
                            break;
                        }
                    }
                }
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

        private Quadrants QuadrantToIgnore(int imin, int imax, int jmin, int jmax, int imissing, int jmissing)
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
    }
















    public class chair
    {
        public static Brush[] brushes =
        {
        Brushes.Green,Brushes.Yellow,Brushes.Orange,Brushes.DimGray
    };
        public Color cc;
        public int number;
        public int bgbrushnum = 0;
        public List<Point> squars = new List<Point>();
        public PointF[] points;
        public List<chair> neighbors;

        public void draw(Graphics g, bool b, Color c)
        {
            if (points == null)
            {
                return;
            }

            var bb = new SolidBrush(c);
            g.FillPolygon(bb, points);
            g.DrawPolygon(Pens.Black, points);

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

        public void draw(Graphics g, bool b, Random rnd)
        {
            if (points == null)
            {
                return;
            }
            var bb = new SolidBrush(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            g.FillPolygon(bb, points);
            g.DrawPolygon(Pens.Black, points);

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

        public void draw(Graphics g, bool b)
        {
            if (points == null)
            {
                return;
            }
            g.FillPolygon(brushes[bgbrushnum], points);
            g.DrawPolygon(Pens.Black, points);

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
            return new PointF(x / points.Length, y / points.Length);
        }


        public static bool areneighburs(Point p1, Point p2)
        {
            if (p1.X == p2.X && Math.Abs(p2.Y - p1.Y) == 1)
            {
                return true;
            }
            if (p1.Y == p2.Y && Math.Abs(p1.X - p2.X) == 1)
            {
                return true;
            }
            return false;
        }


        public bool isneigburs(chair c)
        {
            foreach (Point p1 in squars)
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

            foreach (chair c in neighbors)
            {
                if (c.bgbrushnum == num)
                {
                    return false;
                }
            }
            return true;
        }

        static Random rnd = new Random();

        public int giveTheRightColor()
        {       List<int> color = new List<int>() { 0, 1, 2, 3 };

            foreach (chair i in neighbors)
            {
                if (i.bgbrushnum != -1)
                    color.Remove(i.bgbrushnum);
            }
            return color.Last();
            //return color[rnd.Next(color.Count);


        }
        public bool colorAllowedcolor(Color c)
        {
            foreach (chair i in neighbors)
            {
                if (i.cc == c)
                {
                    return false;
                }
            }
            return true;
        }

    }
}