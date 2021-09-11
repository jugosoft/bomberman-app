using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GXA
{
    public class Geometry
    {
        static public bool IntersectGrid(int minx, int miny, int maxx, int maxy, int tCol, int tRow)
        {
            int tminx = tCol * Common.GridSize;
            int tminy = tRow * Common.GridSize;
            int tmaxx = tminx + Common.GridSize;
            int tmaxy = tminy + Common.GridSize;

            if (PtInRectangle(minx, miny, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(minx + Common.GridSize / 2, miny, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(minx, miny + Common.GridSize / 2, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(minx, maxy, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(minx + Common.GridSize / 2, maxy, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(maxx, miny, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }
            if (PtInRectangle(maxx, miny + Common.GridSize / 2, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            if (PtInRectangle(maxx, maxy, tminx, tminy, tmaxx, tmaxy))
            {
                return true;
            }

            return false;
        }

        static public bool PtInRectangle(int x, int y, int tminx, int tminy, int tmaxx, int tmaxy)
        {
            if (x > tminx && x < tmaxx && y > tminy && y < tmaxy)
            {
                int dx1 = x - tminx;
                int dx2 = tmaxx - x;
                int dx = (dx1 > dx2) ? dx2 : dx1;

                int dy1 = y - tminy;
                int dy2 = tmaxy - y;
                int dy = (dy1 > dy2) ? dy2 : dy1;

                if (dx > Common.IntersectTolerance && dy > Common.IntersectTolerance)
                {
                    return true;
                }
            }

            return false;
        }

        static public bool IntersectCharacter(Character Character, Character antoher)
        {
            Rectangle rect1 = new Rectangle(Character.Pos.X + 10, Character.Pos.Y + 10, Common.GridSize - 10, Common.GridSize - 10);
            Rectangle rect2 = new Rectangle(antoher.Pos.X + 10, antoher.Pos.Y + 10, Common.GridSize - 10, Common.GridSize - 10);
            return rect1.IntersectsWith(rect2);
        }
    }
}
