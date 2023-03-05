using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit006_tankkill_start
{
    public partial class Form1 : Form
    {
        private Thread t;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//运行时显示在中间
            //FPS=帧率

            //添加画布
            g = this.CreateGraphics();
            GameFramework.g = g;

            //开启线程
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();

        }
        private static void GameMainThread()
        {
            //GameFrameWork
            GameFramework.Start();
            int SleepTinme = 1000 / 60;
            bool ThreadControl = true;
            //此处60帧时理想情况
            while (true)
            {
                //按照某个颜色清空画布
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update();
                Thread.Sleep(SleepTinme);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }
    }
}
