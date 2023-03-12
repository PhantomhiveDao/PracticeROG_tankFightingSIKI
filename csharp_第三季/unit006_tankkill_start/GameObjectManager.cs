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
        private static int enemyBornSpeed = 60;//坦克生成速度，一帧一个
        private static int enemyBornCount = 60;//坦克生成计数器
        private static Point[] points=new Point[3];//坦克生成计数器
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> expList = new List<Explosion>();
        private static Object _bulletLock = new object();
        /// <summary>
        /// 三个敌人只有位置不固定，用start做初始化
        /// </summary>
        public static void Start()
        {
            //创建三个位置
            points[0].X= 0;points[0].Y= 0;
            points[1].X= 7*30;points[1].Y= 0;
            points[2].X= 14*30;points[2].Y= 0;
            
        }
        public static void Update()
        {
            for(int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Update();
            }
            //Console.WriteLine("drawmap end");
            
            for (int i = 0; i < steelList.Count; i++)
            {
                steelList[i].Update();
            }
            //敌人坦克
            foreach (EnemyTank tank in tankList)
            {
                tank.Update();
            }
            //遍历集合的时候不能修改集合
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }
            foreach (Explosion exp in expList)
            {
                exp.Update();
            }
            CheckAndDestoryBullet();
            //子弹
           
            boss.Update();
            myTank.Update();
            EnemyBorn();//敌人生成的方法
            CheckAndDestroyExplosion();

        }
        /// <summary>
        /// 检测消失的子弹
        /// </summary>
        private static void CheckAndDestoryBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.IsDestroy == true)
                {
                    needToDestroy.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestroy)
            {
                bulletList.Remove(bullet);
            }
            
        }
        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.IsNeedDestroy == true)
                {
                    needToDestroy.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }
        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tag"></param>
        /// <param name="dir"></param>
        public static void CreatBullet(int x,int y,Tag tag,Direction dir) 
        {
            lock (_bulletLock)
            {
                Bullet bullet = new Bullet(x, y, 5, dir, tag);
                bulletList.Add(bullet);
            }
            
        }
        public static void CreatExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x,y);
            expList.Add(exp);
            
        }
        public static void DestroyWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }
        public static void DestroyEnemyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private static void EnemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;

            //在0-2随一个
            Random rd = new Random();
            int index=rd.Next(0,3);
            Point position = points[index];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreatEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    CreatEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreatEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreatEnemyTank4(position.X, position.Y);
                    break;
            }
            enemyBornCount = 0;
        }
        /// <summary>
        /// 生成四种敌人坦克
        /// </summary>
        private static void CreatEnemyTank1(int x,int y)
        {
            //每调用一次这个方法，就会新创建一个坦克
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
            tankList.Add(tank);
        }        
        private static void CreatEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            tankList.Add(tank);
        }        
        private static void CreatEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            tankList.Add(tank);
        }        
        private static void CreatEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 21, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            tankList.Add(tank);
        }

        /// <summary>
        /// 检测是否在墙里、钢墙里
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
        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NotMovething steel in steelList)
            {
                if (steel.GetRectangle().IntersectsWith(rt))
                {
                    return steel;
                }
            }
            return null;
        }
        public static bool IsCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);      
          
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank tank in tankList)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
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
