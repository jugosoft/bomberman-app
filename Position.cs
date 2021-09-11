using System;

namespace GXA
{
    public struct Position : IComparable
    {
        public int X;

        public int Y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #region IComparable 

        int IComparable.CompareTo(object obj)
        {
            Position another = (Position)obj;
            return (this.X == another.X && this.Y == another.Y) ? 0 : -1;
        }

        #endregion
    }
}
