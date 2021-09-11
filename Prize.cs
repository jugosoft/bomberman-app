using System;
using System.Collections.Generic;
using System.Text;

namespace GXA
{
    public class Prize:Character
    {
        public Prize(PrizeType type)
        {
            this.Type = type;
        }
        
        //на поле выводятся объекты призов.
        public override void Draw(System.Drawing.Graphics g)
        {
            string s = "";
            switch (Type)
            {
                case PrizeType.ptBomb: //увленичение одновременых числа бомб
                    s = "B";
                    break;
                case PrizeType.ptPlayer: //жизней
                    s = "+";
                    break;
                case PrizeType.ptPower: //силы
                    s = "P";
                    break;
                case PrizeType.ptSpeed: //скорости
                    s = "S";
                    break;
                case PrizeType.ptComplex: //добавляет всего по чуть-чуть
                    s = "C";
                    break;
            }
            g.DrawString(s, Common.PrizeFont, Common.YellowBrush, Pos.X, Pos.Y);
        }

        public PrizeType Type;

        public bool Enable = true;
    }

    public enum PrizeType
    {
        ptComplex,
        ptPower,
        ptBomb,
        ptSpeed,
        ptPlayer
    }
}
