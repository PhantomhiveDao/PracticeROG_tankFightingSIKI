using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unit006_tankkill_start.Properties;

namespace unit006_tankkill_start
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    class Bullet : MoveThing
    {
        public Tag Tag{ get; set; }
        public bool IsDestroy { get; set; }

        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            IsDestroy = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapRight = Resources.BulletRight;
            BitmapLeft = Resources.BulletLeft;
            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
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
                if (Y + Height/2+3 < 0)
                {//把自身销毁
                    //GameObjectManager.DeleteBullet(this);return;
                    IsDestroy = true;return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y+ Height-3 > 450)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X +Width/2-3 < 0)
                {
                    IsDestroy = true; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width/2+3 > 450)
                {

                    IsDestroy = true; return;
                }
            }
            #endregion

            //检查是否与墙碰撞-碰撞检测放在gameManager里
            //Rectangle；->传的参数值：左上角的坐标点，矩形的长和宽

            //通过移动之后的位置进行判断
            Rectangle rect = GetRectangle();
            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Height = 3;
            rect.Width = 3;
            //爆炸中心点
            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;
            //墙、钢墙、坦克
            NotMovething wall = null;
            if ((wall=GameObjectManager.IsCollidedWall(rect)) != null)
            {
                IsDestroy = true;
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreatExplosion(xExplosion,yExplosion);
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsDestroy = true;return;
            }
            if (Tag == Tag.MyTank)
            {
                EnemyTank tank = null;
                if ((tank=GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestroy = true;
                    GameObjectManager.DestroyEnemyTank(tank);
                    return;
                }
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                //IsMoving = false; return;
            }
        }
        private void Move()
        {
            //if (IsMoving == false) return;

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

    }
}
