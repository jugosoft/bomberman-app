using System.Drawing;
using GXA.Properties;

namespace GXA
{
    class Bomb:Character
    {
        /// <summary>
        /// Отрисовка бомбы
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {            
            if (cnt==0||cnt == 1)
            {
                g.DrawImage(Resources.Bomb1, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 2|| cnt == 3)
            {
                g.DrawImage(Resources.Bomb2, this.Pos.X, this.Pos.Y);
            }
            if (cnt == 4 || cnt == 5)
            {
                g.DrawImage(Resources.Bomb3, this.Pos.X, this.Pos.Y);
            }
            cnt++;
            if (cnt > 5)
            {
                cnt = 0;
            }
        }

        private int cnt = 0;

        public int Elapsed = 0;
    }
}
