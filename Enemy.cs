using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using GXA.Properties;

namespace GXA
{
    class Enemy:Character
    {
        public Enemy()
        {
            this.MoveSpeed = 1;
        }
        /// <summary>
        /// отрисовка врага
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            Point[] pts = new Point[3];
            
            switch (this.Direction)
            {
                case Direction.dDown:                    
                    g.DrawImage(Resources.ene_Down, this.Pos.X, this.Pos.Y);
                    break;
                case Direction.dLeft:                    
                    g.DrawImage(Resources.ene_Left, this.Pos.X, this.Pos.Y);
                    break;
                case Direction.dRight:                    
                    g.DrawImage(Resources.ene_right, this.Pos.X, this.Pos.Y);
                    break;
                case Direction.dUp:                   
                    g.DrawImage(Resources.ene_up, this.Pos.X, this.Pos.Y);
                    break;
            }           
        }
    }
}
