using System;
using System.Collections.Generic;

using System.Text;
using GXA.Properties;

namespace GXA
{
    public class Fire:Character
    {
        /// <summary>
        /// отрисовка взрыва
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
        {           
            if (cnt == 0)
            {
                g.DrawImage(Resources.Explode1, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 1)
            {
                g.DrawImage(Resources.Explode2, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 2)
            {
                g.DrawImage(Resources.Explode3, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 3)
            {
                g.DrawImage(Resources.Explode4, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 4)
            {
                g.DrawImage(Resources.Explode5, this.Pos.X, this.Pos.Y);
            }
            cnt++;
            if (cnt > 4)
            {
                cnt = 0;
            }
        }

        private int cnt = 0;

        public int Elapsed = 0;
    }
}
