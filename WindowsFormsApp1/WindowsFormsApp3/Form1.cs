using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }
    private static PointF point1 = new PointF(100, 800);
    private static PointF point2 = new PointF(200, 900);
    private static PointF point3 = new PointF(100, 1000);
    private static PointF point4 = new PointF(250, 900);//center
    private static PointF helic = new PointF(250, 900);
    private static PointF[] bumerang = { point1, point2, point3, point4 };
    private static PointF start = new PointF(100, 800);
    private static PointF stop = new PointF(900, 80);
    
         private void button1_Click(object sender, EventArgs e)
        {
      timer2.Stop();
      timer1.Start();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      timer1.Stop();
      timer2.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      for (int i = 0; i < bumerang.Length; i++)
      {
        bumerang[i].X += 6;
        bumerang[i].Y -= 3;
      }
      Invalidate();
    }

    private void timer2_Tick(object sender, EventArgs e)
    {
      if (bumerang[0] != start)
        for (int i = 0; i < bumerang.Length; i++)
        {
          bumerang[i].X -= 6;
          bumerang[i].Y += 3;
        }

      Invalidate();
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
      Graphics paint = e.Graphics;
      SolidBrush S = new SolidBrush(Color.Brown);

      paint.FillClosedCurve(S, bumerang);
    }
  }
  }
