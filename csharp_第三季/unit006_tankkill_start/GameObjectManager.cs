using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unit006_tankkill_start.Properties;

namespace unit006_tankkill_start
{
    class GameObjectManager

    {
        private static List<NotMovething> wallList = new List<NotMovething>();
        private static List<NotMovething> steelList = new List<NotMovething>();
        private static NotMovething boss;
        private static MyTank myTank;

        public static void Update()
        {
            for(int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Update();
            }
            Console.WriteLine("drawmap end");
            
            for (int i = 0; i < steelList.Count; i++)
            {
                steelList[i].Update();
            }
            boss.Update();
            myTank.Update();
        }
        /// <summary>
        /// 检测是否在墙里
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            foreach (NotMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        /// <summary>
        /// 之前是每一帧直接绘制在窗口上，现在更改为->先绘制在一张与窗口相同大小的图片上。
        /// </summary>
        //public static void DrawMap()
        //{
        //    //foreach (NotMovething nm in wallLists)
        //    //{
        //    //    nm.DrawSelf();
        //    //    //Console.WriteLine("666");
        //    //}
        //    for (int i = 0; i < wallList.Count; i++)
        //    {
        //        wallList[i].DrawSelf();
        //    }
        //    Console.WriteLine("drawmap end");
        //    for (int i = 0; i < steelList.Count; i++)
        //    {
        //        steelList[i].DrawSelf();
        //    }
        //    boss.DrawSelf();
        //    Console.WriteLine("drawmap end");
        //}
        //public static void DrawMyTank()
        //{
        //    myTank.DrawSelf();
        //}
        public static void CreatMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;

            myTank = new MyTank(x, y, 2);
        }

        public static void CreatMap()
        {
            CreateWall(1, 1, 5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(0, 7, 1, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);

            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);

            CreateWall(9, 9, 3, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);


            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);

            CreateBoss(7, 14, Resources.Boss);

        }
        //创建墙
        private static void  CreateWall(int x,int y,int count, Image img ,List<NotMovething> wallList)//x,y,从第几个点开始创建
        {           
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {

                NotMovething wall1 = new NotMovething(xPosition, i, img);
                NotMovething wall2 = new NotMovething(xPosition + 15, i, img);
                wallList.Add(wall1);
                wallList.Add(wall2);
                Console.WriteLine(i);

            }           
        }
        //创建boss
        private static void CreateBoss(int x, int y, Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMovething(xPosition, yPosition, img);
        }

        //按键按下时
        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }
    }
}
