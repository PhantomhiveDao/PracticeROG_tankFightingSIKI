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
        public int Hp = 5;
        private int originalX;
        private int originalY;
        public MyTank(int x, int y, int speed)
        {
            IsMoving = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapRight = Resources.MyTankRight;
            BitmapLeft = Resources.MyTankLeft;
            this.Dir = Direction.Up;

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
                if (Y + Speed + Height > 450)
                {

                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {

                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {

                    IsMoving = false; return;
                }
            }
            #endregion

            //检查是否与墙碰撞-碰撞检测放在gameManager里
            //Rectangle；->传的参数值：左上角的坐标点，矩形的长和宽

            //通过移动之后的位置进行判断
            Rectangle rect = GetRectangle();
            switch (Dir)
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
            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoving = false; return;
            }
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

        //gamgMainThread 和keydown的线程冲突
        //处理方式：1.减少keydown调用次数。2.多线程中资源共享-线程1和线程2使用同一个对象时会出现资源访问冲突。
        //2.->使用锁的方式
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
                case Keys.Space:
                    //发射子弹
                    Attack();
                    //GameObjectManager.CreatBullet();
                    break;
            }
        }
       
        private void Attack()
        {
            SoundManager.PlayFire();
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;
            }
            GameObjectManager.CreatBullet(x, y, Tag.MyTank, Dir);
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
        public void TakeDamage()
        {
            Hp--;
            if (Hp <= 0)
            {
                X = originalX;
                Y = originalY;
                Hp = 4;
            }
        }
    }
}
