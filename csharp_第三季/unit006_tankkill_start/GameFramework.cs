using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit006_tankkill_start
{
    class GameFramework
    {


        public static Graphics g;

        //游戏开始时做的事情
        public static void Start()
        {
            GameObjectManager.CreatMap();
        }

        //持续要做的事情
        public static void Update()
        { //fps
            GameObjectManager.DrawMap();
            

            //Console.WriteLine("在画地图");


        }

    }
}
