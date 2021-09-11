using System.Drawing;
using GXA.Properties;

namespace GXA
{
    public class Map
    {
        /// <summary>
        /// констуктор для карты. Нужны только два параметра
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        public Map(int Col, int Row)
        {
            this.Col = Col;
            this.Row = Row;
            Grids = new GridState[Col, Row];
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Grids[i, j] = GridState.csBlack;
                }
            }
            for (int i = 1; i < Col / 2; i++) //делаем неубиваемые блоки
            {
                for (int j = 1; j < Row / 2; j++)
                {
                    Grids[2 * i - 1, 2 * j - 1] = GridState.csFix;
                }
            }
        }

        /// <summary>
        /// отрисовка карты
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            int GridSize = Common.GridSize;
            g.DrawLine(Common.WhitePen, 0, 0, (Col - 1) * GridSize, 0);
            g.DrawLine(Common.WhitePen, 0, 0, 0, (Row - 1) * GridSize);
            g.DrawLine(Common.WhitePen, (Col - 1) * GridSize, 0, (Col - 1) * GridSize, (Row - 1) * GridSize);
            g.DrawLine(Common.WhitePen, 0, (Row - 1) * GridSize, (Col - 1) * GridSize, (Row - 1) * GridSize);
            
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    if (Grids[i, j] == GridState.csFix)
                    {
                        Point p1 = new Point(i * GridSize, j * GridSize);                        
                        g.DrawImage(Resources.FixGrid, p1.X, p1.Y);
                    }
                }
            }

            //draw test string
            g.DrawString(Common.TestText, Common.DefaultFont, Common.BlackBrush, 0, Row * GridSize);
        }        

        public GridState[,] Grids;

        public int Row = 14;

        public int Col = 20;

    }

    public enum GridState
    {
        csBlack,
        csFix,
        csWall,
        csBomb
    }
}
