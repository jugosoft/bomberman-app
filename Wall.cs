using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GXA.Properties;

namespace GXA
{
    class Wall:Character
    {
        /// <summary>
        /// отрисовка стены
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {           
            g.DrawImage(Resources.Wall, this.Pos.X, this.Pos.Y);
        }
    }
}
