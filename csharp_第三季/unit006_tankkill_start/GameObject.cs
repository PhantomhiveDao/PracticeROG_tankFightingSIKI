using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit006_tankkill_start
{
    abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        //需要子类重写
        protected abstract Image GetImage();


        public virtual void DrawSelf()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(),X,Y);


        }


    }
    
}
