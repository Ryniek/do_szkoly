using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace majcher_rynski_projekt
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        int width = 300, height = 300, secondLine = 140, minuteLine = 110, hourLine = 80;

        int cx, cy;

        Bitmap bitmap;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(width + 1, height + 1);

            cx = width / 2;    //srodek zegara
            cy = height / 2;

            this.BackColor = Color.Black;

            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.timer_tick);
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bitmap);

            int sekunda = DateTime.Now.Second;
            int minuta = DateTime.Now.Minute;
            int godzina = DateTime.Now.Hour;

            int[] handCoord = new int[2];


            g.Clear(Color.Black);

            g.DrawEllipse(new Pen(Color.White, 1f), 0, 0, width, height);

            g.DrawString("12", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(140, 2));
            g.DrawString("1", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(210, 25));
            g.DrawString("2", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(260, 75));
            g.DrawString("3", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(286, 140));
            g.DrawString("4", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(260, 207));
            g.DrawString("5", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(210, 257));
            g.DrawString("6", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(142, 282));
            g.DrawString("7", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(70, 257));
            g.DrawString("8", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(20, 207));
            g.DrawString("9", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(0, 140));
            g.DrawString("10", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(20, 75));
            g.DrawString("11", new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold), Brushes.White, new Point(70, 25));

            handCoord = ms_coord(sekunda, secondLine);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = ms_coord(minuta, minuteLine);
            g.DrawLine(new Pen(Color.White, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hr_coord(godzina%12, minuta, hourLine);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            pictureBox1.Image = bitmap;

            this.Text = "Obecny czas: " + godzina + ":" + minuta + ":" + sekunda;

            g.Dispose();
        }

        private int[] ms_coord(int val, int length)  //KOORDYNATY DLA SEKUND I MINUT
        {
            int[] coord = new int[2];
            val *= 6;     //kazda minuta i sekunda to 6 stopni

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(length * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(length * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(length * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(length * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] hr_coord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            int val = (int)((hval * 30) + (mval * 0.5));   //Kazda godzina to 30 stopni

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
    }
}
