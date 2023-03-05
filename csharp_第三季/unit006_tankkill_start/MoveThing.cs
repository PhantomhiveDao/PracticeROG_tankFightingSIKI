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
        Up,
        Down,
        Left,
        Right
    }
    class MoveThing : GameObject
    {
        

        public int speed { get; set; }
        public Bitmap BitmapUp {get;set;}
        public Bitmap BitmapDown {get;set;}
        public Bitmap BitmapLeft {get;set;}
        public Bitmap BitmapRight {get;set;}
        //朝向
        public Direction Dir { get; set; }

        protected override Image GetImage()
        {

            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.Up:
                    bitmap= BitmapUp;
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
            return BitmapUp;
        }
    }
}
