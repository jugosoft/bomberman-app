using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GXA.Properties;

namespace GXA
{
    class Boy:Character
    {
        public Boy()
        {
            this.MoveSpeed = 2;
        }       
        int count = 0;

        public override void Draw(Graphics g)
        {   
            Point[] pts = new Point[3];
            
            switch (this.Direction)
            {
                case Direction.dDown:
                    //g.DrawImage(Resources.boy_down0, this.Pos.X, this.Pos.Y); 
                            if (count == 0) {g.DrawImage(Resources.boy_down00, this.Pos.X, this.Pos.Y);}
                            if (count == 1) {g.DrawImage(Resources.boy_down01, this.Pos.X, this.Pos.Y);}
                            if (count == 2) {g.DrawImage(Resources.boy_down02, this.Pos.X, this.Pos.Y);}
                            if (count == 3) {g.DrawImage(Resources.boy_down03, this.Pos.X, this.Pos.Y);}                
                                
                            if (count > 3){count = 0;}
                            count++;
                            break;
                case Direction.dLeft:                            
                            if (count == 0-this.MoveSpeed) {g.DrawImage(Resources.boy_left00, this.Pos.X, this.Pos.Y);}                           
                            if (count == 6-this.MoveSpeed) {g.DrawImage(Resources.boy_left01, this.Pos.X, this.Pos.Y);}
                            if (count == 7-this.MoveSpeed) {g.DrawImage(Resources.boy_left02, this.Pos.X, this.Pos.Y);}
                            if (count == 8-this.MoveSpeed) {g.DrawImage(Resources.boy_left03, this.Pos.X, this.Pos.Y);}                
                                
                            if (count > 8-this.MoveSpeed){count = 0;}
                            count++;
                            break;
                   
                case Direction.dRight:                    
                            if (count == 0-this.MoveSpeed) {g.DrawImage(Resources.boy_right00, this.Pos.X, this.Pos.Y);}
                            if (count == 6-this.MoveSpeed) {g.DrawImage(Resources.boy_right01, this.Pos.X, this.Pos.Y);}
                            if (count == 7-this.MoveSpeed) {g.DrawImage(Resources.boy_right02, this.Pos.X, this.Pos.Y);}
                            if (count == 8-this.MoveSpeed) {g.DrawImage(Resources.boy_right03, this.Pos.X, this.Pos.Y);}                
                                
                            if (count >= 8-this.MoveSpeed){count = 0;}
                            count++;
                            break;
                case Direction.dUp:
                            if (count == 0-this.MoveSpeed) {g.DrawImage(Resources.boy_up00, this.Pos.X, this.Pos.Y);}
                            if (count == 6-this.MoveSpeed) {g.DrawImage(Resources.boy_up01, this.Pos.X, this.Pos.Y);}
                            if (count == 7-this.MoveSpeed) {g.DrawImage(Resources.boy_up02, this.Pos.X, this.Pos.Y);}
                            if (count == 8-this.MoveSpeed) {g.DrawImage(Resources.boy_up03, this.Pos.X, this.Pos.Y);}                
                                
                            if (count >= 8-this.MoveSpeed) {count = 0;}
                            count++;
                            break;
            }            
        }
    }
}
