using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit006_tankkill_start
{
    //枚举类型的朝向
    enum Direction
    { 
        Up=0,
        Down=1,
        Left=2,
        Right=3
    }
    class MoveThing : GameObject
    {
        //添加一个锁，解决多线程的问题
        private Object _lock = new Object();

        public int Speed { get; set; }
        public Bitmap BitmapUp {get;set;}
        public Bitmap BitmapDown {get;set;}
        public Bitmap BitmapLeft {get;set;}
        public Bitmap BitmapRight {get;set;}

        //public bool IsMoving { get; set; }
        //朝向
        private Direction dir;
        public Direction Dir { get { return dir; } 
            set {
                dir = value;
                Bitmap bmp = null;

                switch (dir)
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;
                }
                lock (_lock)
                { 
                    //多线程冲突问题。长时间触发按下按键这个方法
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
               
            } 
        }
        /// <summary>
        /// 更换图片
        /// </summary>
        /// <returns></returns>
        protected override Image GetImage()
        {

            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.Up:
                    bitmap= BitmapUp;
                    //Console.WriteLine("改了图");
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
            }
            
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }
        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();

            }
        }
    }
}
