using SuperMap.Data;
using SuperMap.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 平抛运动
{
    public partial class Form1 : CCWin.CCSkinMain
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<double, double> ts = new Dictionary<double, double>();
        List<double> Xx = new List<double>();
        List<double> Yy = new List<double>();
        GeoStyle geoStyle_P = new GeoStyle();
        string str;
        private void 开始模拟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                geoStyle_P.MarkerAngle = 14.0;
                geoStyle_P.MarkerSize = new Size2D(5, 5);
                geoStyle_P.LineColor = Color.Red;
                平抛运动 open = new 平抛运动();
                open.ShowDialog();
                str = open.name();
                ts = open.con();
                Map map = mapControl1.Map;
                bool h = true;
                Point2D p = map.Center;
                Point2D pl = map.Center;
                int m = 0;
                Xx = ts.Keys.ToList<double>();
                Yy = ts.Values.ToList<double>();

                Point2Ds point2Ds = new Point2Ds();
                for (int i = 0; i < Xx.Count; i++)
                {
                    Point2D point2D = new Point2D(Xx[i], Yy[i]);
                    point2Ds.Add(point2D);
                }
                GeoLine geoLine = new GeoLine(point2Ds);
                mapControl1.Map.TrackingLayer.Add(geoLine, "线");
                mapControl1.Map.Refresh();
                foreach (var t in ts)
                {
                    if (h)
                    {
                        p.X = t.Key;
                        p.Y = t.Value + 10;
                        h = false;
                    }
                    m++;
                    if (m == ts.Count)
                    {
                        pl.X = t.Key;
                        pl.Y = t.Value;
                    }
                }
                Rectangle2D rectangle = new Rectangle2D(p, pl);
                mapControl1.Map.ViewBounds = rectangle;
                mapControl1.Map.Refresh();
                this.timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("模拟失败，请确保参数输入正确");
            }
        }
        int hh = 0;
        int m;
        bool n = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (n)
            {
                m = timer1.Interval;
                n = false;
            }
            if (this.timer1.Interval % 2 == 0)
            {
                GeoPoint point = new GeoPoint();
                point.X = Xx[hh];
                point.Y = Yy[hh];
                point.Style = geoStyle_P;
                mapControl1.Map.TrackingLayer.Add(point, "点");
                mapControl1.Map.Refresh();
                hh++;
            }
            m++;
            if (hh == Xx.Count)
            {
                this.richTextBox1.Show();
                richTextBox1.Text = str;
                this.timer1.Interval = 100;
                n = true;
                hh = 0;
                this.timer1.Enabled = false;

            }
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Hide();
            mapControl1.Map.TrackingLayer.Clear();
            mapControl1.Map.Refresh();
        }

        private void 退出程序ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
