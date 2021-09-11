using System.Drawing;
using GXA.Properties;
using System.IO;
using System;
using System.Drawing;

namespace GXA
{
    public class MapCamp
    {
        private int number;

        public int Number {get;set;}
        
        int [,] arrayMap;

        /// <summary>
        /// констуктор для карты. Нужны только два параметра
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        public MapCamp (int Col, int Row)
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

        public void LoadMap ()
        {
            int [,] arrayMap = new int[Row, Col]; 
            string strToRead = number + ".txt";
            StreamReader str = new StreamReader(strToRead);
            string buffer;
            int width = 0, height = 0;
            while ((buffer = str.ReadLine()) != null) //ищу самую длинную. на всяк случай.
            {
                if (width < buffer.Length)
                    width = buffer.Length;
                height++;
            }
            str = new StreamReader(strToRead);
            arrayMap = new int[width, height];
            for (int x = 0; x < height; x++)
            {
                buffer = str.ReadLine();
                for (int y = 0; y < buffer.Length; y++)
                {
                    arrayMap[y, x] = (int)buffer[y];
                }
            }
        }        

        /// <summary>
        /// отрисовка карты
        /// </summary>
        /// <param name="g"></param>
        public void Draw (Graphics g)
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
                    switch (arrayMap[i,j])
                    {

                        case 0: g.DrawImage(Resources.Wall, i * GridSize, j * GridSize);
                            break;
                        case 1: g.DrawImage(Resources.FixGrid, i * GridSize, j * GridSize);
                            break;                        
                        default:
                            break;
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
}
