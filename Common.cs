using System;

using System.Drawing;

namespace GXA
{
    class Common
    {
        static public Pen WhitePen = new Pen(Color.White);
        static public Pen BlackPen = new Pen(Color.Black);
        static public Pen RedPen = new Pen(Color.Red);
        static public Pen BluePen = new Pen(Color.Blue);
        static public Pen YellowPen = new Pen(Color.Yellow);
        static public Brush WhiteBrush = new SolidBrush(Color.Black);
        static public Brush BlackBrush = new SolidBrush(Color.Black);
        static public Brush RedBrush = new SolidBrush(Color.Red);
        static public Brush BlueBrush = new SolidBrush(Color.Blue);
        static public Brush YellowBrush = new SolidBrush(Color.Yellow);
        static public int GridSize = 64;
        static public string TestText = "";
        static public Font DefaultFont = new Font("Tahoma", 8);
        static public Font LargeFont = new Font("Tahoma", 12);
        static public Font PrizeFont = new Font("Tahoma", GridSize - 5);
        static public int IntersectTolerance = 6;
        static public Random SystemRandom = new Random();
        static public Brush ActiveBursh = new SolidBrush(Color.Blue);
        static public Font TitleFont = new Font("Times new Roman", 120);
    }
}
