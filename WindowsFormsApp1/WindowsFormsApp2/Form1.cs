using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
  public partial class Form1 : Form
  {
    private Point pole;//центр спирали
    private Point center;//центр окружности
    private Timer timer = new Timer();//таймер
    private Pen pen = new Pen(Color.Red, 2);//Перо для рисования окружности
    private Pen curvepen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
    private float diameter = 30f;//Диаметр окружности
    private float a = 10;//Шаг спирали
    private float phi;//угол поворота спирали
    private List<Point> pts;
    private Point[] ptar;
    public Form1()
    {
      InitializeComponent();
      pole = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
      pts = new List<Point>();
      timer.Tick += new EventHandler(timer1_Tick);
      this.Paint += new PaintEventHandler(Form1_Paint);
      timer.Interval = 100;
      timer.Enabled = true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      center = new Point();
      center.X = (int)((a / (Math.PI * 2)) * phi * Math.Cos(phi));
      center.Y = (int)((a / (Math.PI * 2)) * phi * Math.Sin(phi));
      pts.Add(center);
      ptar = new Point[pts.Count];
      pts.CopyTo(ptar);
      this.Refresh();
      phi += 0.8f;
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
      try
      {
        if (timer.Enabled)
          using (Graphics g = e.Graphics)
          {
            //Перенос начала координат в центр клиентской части формы
            g.TranslateTransform(pole.X, pole.Y);
            //рисование окружности в заданных координатах
            g.DrawEllipse(pen, center.X - diameter / 2, center.Y - diameter / 2, diameter, diameter);
            //Прорисовка спирали
            g.DrawCurve(curvepen, ptar);
            /*граничные условия. Если центр окружности выходит за клиентскую 
            область формы, то начинаем сначала*/
            center.Offset(pole);
            if (!this.ClientRectangle.Contains(center)) Reset();
          }
      }
      catch (Exception) { }
    }
    private void Reset()
    {
      phi = 0;
      pts.Clear();
      ptar = null;
    }
  }
}
