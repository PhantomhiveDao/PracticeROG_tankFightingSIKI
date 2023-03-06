using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit006_tankkill_start
{
    /// 不可以移动的物体

    
    class NotMovething:GameObject
    {
        //绘制时使用的图片
        public Image Img { get; set; }

        protected override Image GetImage()
        {
            return Img;
        }
        public NotMovething(int x, int y, Image img)
        {
            this.X = x;
            this.Y = y;
            this.Img = img;

        }
    }
}
