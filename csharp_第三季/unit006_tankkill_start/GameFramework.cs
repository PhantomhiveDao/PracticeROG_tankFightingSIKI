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
        public static void Start()
        {
            GameObjectManager.CreatMap();
            Console.WriteLine("--创建地图--");

        }

        public static void Update()
        { //fps
            GameObjectManager.DrawMap();
            Console.WriteLine("--画地图--");


        }
    }
}
