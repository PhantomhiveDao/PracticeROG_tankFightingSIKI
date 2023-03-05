using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unit005_tankkill.Properties;

namespace unit005_tankkill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            //设置窗体显示的位置。显示在焦点屏幕上
            
            //this.StartPosition = FormStartPosition.Manual;//设置窗体显示的位置。
            
            // this.Location = new Point(123, 1080);

            //GDI=> Graphics Device Interface图形设备接口//常用技术
            //屏幕坐标系y轴坐标轴朝下，以左上角为原点。

            Graphics g= this.CreateGraphics();//创建一个图形对象
            //Color c = new Color(0,0,255);
            Pen p = new Pen(Color.Red);
            g.DrawLine(p,3,1,5,4);



        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();//创建一个图形对象
            //Color c = new Color(0,0,255);
            Pen p = new Pen(Color.Red);
            g.DrawLine(p, 344, 1, 1245, 666);
            g.DrawString("1145143嘿嘿嘿嘿嘿4", new Font("隶书", 23), new SolidBrush(Color.Green), 33, 4);

            Image image=Properties.Resources.Boss;

            Bitmap bm = Properties.Resources.Star1;
            bm.MakeTransparent(Color.Black);//使黑色部分透明
            g.DrawImage(image, 200, 200);
            g.DrawImage(bm, 100, 300);
        }
    }
}
