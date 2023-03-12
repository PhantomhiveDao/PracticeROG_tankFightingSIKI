using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unit006_tankkill_start.Properties;

namespace unit006_tankkill_start
{
    class Explosion : GameObject
    {
        public bool IsNeedDestroy { get; set; }

        private int playSpeed = 1;
        private int playCount = 0;//  1 /2 = 0
        //2 /2  1
        private int index = 0;
        private Bitmap[] bmpArry = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Explosion(int x, int y)
        {
            foreach (Bitmap bmp in bmpArry)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArry[0].Width / 2;
            this.Y = y - bmpArry[0].Height / 2;
            IsNeedDestroy = false;
        }
        protected override Image GetImage()
        {
            if (index > 4)return bmpArry[4]; 
            return bmpArry[index];
        }
        public override void Update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;
            if (index > 4)
            {
                IsNeedDestroy = true;
            }

            base.Update();
        }
    }
}
