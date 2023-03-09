using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit006_tankkill_start
{
    class GameFramework
    {


        public static Graphics g;

        //游戏开始时做的事情
        public static void Start()
        {
            GameObjectManager.CreatMap();
            GameObjectManager.CreatMyTank();
        }

        //持续要做的事情
        public static void Update()
        { //fps
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();            
            GameObjectManager.Update();

            //Console.WriteLine("在画地图");


        }

        //按键按下时
        public static void KeyDown(KeyEventArgs args)
        {
        }
        public static void KeyUp(KeyEventArgs args)
        {
        }
    }
}
