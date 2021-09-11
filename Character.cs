using System.Drawing;

namespace GXA
{
    public class Character
    {
        public virtual void Draw(Graphics g)
        {

        }

        public Position Pos;

        public Direction Direction;

        public int MoveSpeed = 1; //уровень скорости

        public void Move()
        {
            //движение персонажа, т.е. изменение его координаты
            switch (this.Direction)
            {
                case Direction.dDown:
                    Pos.Y += MoveSpeed;
                    break;
                case Direction.dLeft:
                    Pos.X -= MoveSpeed;
                    break;
                case Direction.dRight:
                    Pos.X += MoveSpeed;
                    break;
                case Direction.dUp:
                    Pos.Y -= MoveSpeed;
                    break;
            }
        }   
    }

    public enum Direction
    {
        dNeutral,
        dUp,
        dDown,
        dLeft,
        dRight
    }
}
