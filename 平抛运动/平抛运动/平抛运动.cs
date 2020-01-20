using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 平抛运动
{
    public partial class 平抛运动 : Form
    {
        public 平抛运动()
        {
            InitializeComponent();
        }
        private double Xs(double t)
        {
            double v0 = double.Parse(textBox1.Text);
            return v0 * t;
        }
        private double Ys(double t)
        {
            double g = double.Parse(textBox3.Text);
            return 0.5 * g * t * t;
        }
        Dictionary<double, double> ts = new Dictionary<double, double>();
        string str;
   
        public Dictionary<double, double> con()
        {
            return ts;
        }
        public string name()
        {
            return str;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            double t;
            double ht = Math.Sqrt(2 * double.Parse(textBox2.Text) / double.Parse(textBox3.Text));
            for (t = 0; t <= ht; t += 0.1)
            {
                double x = Xs(t) + 300;
                double y = Ys(t) - 400;
                ts.Add(x, -y);
            }
            str = string.Format("当地重力加速度：{0}\n小球水平方向初速度：{1}\n小球下落高度：{2}\n小球落地时间：{3}\n小球水平方向位移：{4}\n小球总位移：{5}\n小球平抛运动轨迹方程：y=-({6})*x^2", double.Parse(textBox3.Text), double.Parse(textBox1.Text), double.Parse(textBox2.Text), ht, double.Parse(textBox1.Text) * ht, Math.Sqrt(Math.Pow(double.Parse(textBox1.Text) * ht, 2) + Math.Pow(double.Parse(textBox2.Text), 2)), 0.5 * double.Parse(textBox3.Text) / Math.Pow(double.Parse(textBox1.Text), 2));
            this.Hide();
        }
    }
}
