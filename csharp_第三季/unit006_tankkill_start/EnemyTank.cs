using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unit006_tankkill_start.Properties;

namespace unit006_tankkill_start
{
    class EnemyTank:MoveThing
    {
        //随机数最好设置为成员变量/调用同一个种子生成的随机数。
        public int AttackSpeed { get; set; }
        public int ChangeDirSpeed { get; set; }
        private int attackCount =0;//计数器
        private int changeDirSpeed =0;//计数器
        private static Random r = new Random();
        public EnemyTank(int x, int y, int speed,Bitmap bmpUp, Bitmap bmpDown, Bitmap bmpLeft,Bitmap bmpRight)
        {
            
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapRight = bmpRight;
            BitmapLeft = bmpLeft;
            this.Dir = Direction.Down;
            AttackSpeed = 60;//一秒发射一个
            ChangeDirSpeed = 80;
        }
        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackSpeed) return;
            Attack();
            attackCount = 0;
        }
        private void Attack()
        {
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
            GameObjectManager.CreatBullet(x, y, Tag.EnemyTank, Dir);
        }
        private void AutoChangeDirection()
        {
            changeDirSpeed++;
            if (changeDirSpeed < ChangeDirSpeed) return;
            ChangeDirection();
            changeDirSpeed = 0;
        }
        /// <summary>
        /// 改变方向
        /// </summary>
        private void ChangeDirection()
        {
            //4
           
            while (true)
            {
                Direction dir = (Direction)r.Next(0, 4);

                if (dir == Dir)
                {
                    continue;
                }
                else 
                {
                    Dir = dir; 
                    break; 
                }
            }
            MoveCheck();
        }
        private void Move()
        {
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
        public override void Update()
        {
            //在移动前进行移动检测
            MoveCheck();
            Move();
            AttackCheck();//检测是否攻击方法
            AutoChangeDirection();
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
                    ChangeDirection();
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {

                    ChangeDirection();
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {

                    ChangeDirection();
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {

                    ChangeDirection();
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
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                ChangeDirection(); return;
            }
        }
    }
}
