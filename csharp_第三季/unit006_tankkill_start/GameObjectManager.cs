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
        private static List<NotMovething> wallLists = new List<NotMovething>();

        public static void DrawMap()
        {
            foreach (NotMovething nm in wallLists)
            {
                nm.DrawSelf();
                //Console.WriteLine("666");
            }
            //for (int i = 0; i < wallList.Count; i++)
            //{
            //    wallList[i].DrawSelf();
            //}
            //Console.WriteLine("drawmap end");
        }

        public static void CreatMap()
        {
             CreateWall(1,1, 5,wallLists);
             CreateWall(8, 4, 5,wallLists);

        }
        //创建墙
        private static void  CreateWall(int x,int y,int count, List<NotMovething> wallList)//x,y,从第几个点开始创建
        {           
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {

                NotMovething wall1 = new NotMovething(xPosition, i, Resources.wall);
                NotMovething wall2 = new NotMovething(xPosition + 15, i,Resources.wall);
                wallList.Add(wall1);
                wallList.Add(wall2);
                Console.WriteLine(i);

            }
           // return wallList;
            
        }

    }
}
