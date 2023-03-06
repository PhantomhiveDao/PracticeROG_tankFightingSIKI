using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace unit006_tankkill_start
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;
        //main主线程
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//运行时显示在中间

            //FPS=帧率
            //添加画布--直接在画布上绘制会产生闪烁现象。
            //更改策略：将所有元素先绘制在图片上作为整体，再将图片绘制在画布上 
            windowG = this.CreateGraphics();

            tempBmp = new Bitmap(450,450);
            Graphics bmpG=Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;

            //开启线程-必须传递一个静态方法，如果是普通方法只能通过对象来调用。
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();

        }
        //游戏逻辑的线程
        private static void GameMainThread()
        {
            //GameFrameWork
            GameFramework.Start();

            int sleepTinme = 1000 / 60;
            bool ThreadControl = true;
            //此处60帧时理想情况

            while (ThreadControl)
            {
                //按照某个颜色清空画布
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update();

                windowG.DrawImage(tempBmp, 0, 0);
                Thread.Sleep(sleepTinme);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }
    }
}
