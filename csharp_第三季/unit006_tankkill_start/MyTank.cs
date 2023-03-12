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
    class MyTank : MoveThing
    {
        public bool IsMoving { get; set; }
        public MyTank(int x, int y, int speed)
        {
            IsMoving = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapRight = Resources.MyTankRight;
            BitmapLeft = Resources.MyTankLeft;
            this.Dir = Direction.Up;

        }
        private void Move()
        {
            if (IsMoving == false) return;

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    Console.WriteLine("按下了W");
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
            }
        }
        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }
        public override void Update()
        {
            //在移动前进行移动检测
            MoveCheck();
            Move();

            base.Update();
        }
        private void MoveCheck()
        {
            #region 检查是否超出窗体边界。
            //检查是否超出窗体边界。
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false; return;
                }                
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed+Height  > 450)
                {
                    
                    IsMoving = false;return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X -Speed <0)
                {

                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X+Speed+Width > 450)
                {

                    IsMoving = false; return;
                }
            }
            #endregion

            //检查是否与墙碰撞-碰撞检测放在gameManager里
            //Rectangle；->传的参数值：左上角的坐标点，矩形的长和宽

            //通过移动之后的位置进行判断
            Rectangle rect = GetRectangle();
            switch(Dir)
                {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
                  
            }
            if (GameObjectManager.IsCollidedWall(rect)!=null)
            {
                IsMoving = false;return;
            }
        }
        
    }
}
