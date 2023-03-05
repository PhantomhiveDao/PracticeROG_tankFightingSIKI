using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unit006_tankkill_start.Properties;

namespace unit006_tankkill_start
{
    class GameObjectManager

    {
        private static List<NotMovething> wallList = new List<NotMovething>();

        public static void DrawMap()
        {
            foreach (NotMovething nm in wallList)
            {
                nm.DrawSelf();
                //Console.WriteLine("666");
            }
        }

        public static void CreatMap()
        {
             CreateWall(1,1, 5, wallList);
            

        }

        private static void  CreateWall(int x,int y,int count, List<NotMovething> wallList)//x,y,从第几个点开始创建
        {           
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i=i+15)
            {
                NotMovething wall1 = new NotMovething(xPosition, i, Resources.wall);
                NotMovething wall2 = new NotMovething(xPosition+15, i, Resources.wall);
                wallList.Add(wall1);
                wallList.Add(wall2);

            }

            
        }

    }
}
