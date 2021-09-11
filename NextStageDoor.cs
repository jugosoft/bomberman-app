using GXA.Properties;
namespace GXA
{
    public class NextStageDoor:Character
    {
        /// <summary>
        /// отрисовка перехода на след. уровень
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
        {            
            if (cnt == 0 || cnt == 1 ||cnt ==2)                          //здесь делается мигание.
            {                                                            //здесь делается мигание.
                g.DrawImage(Resources.door1, this.Pos.X, this.Pos.Y);    //здесь делается мигание.
            }                                                            //здесь делается мигание.
            if (cnt == 3 || cnt == 4 || cnt == 5)                        //здесь делается мигание.
            {                                                            //здесь делается мигание.
                g.DrawImage(Resources.door2, this.Pos.X, this.Pos.Y);    //здесь делается мигание.
            }                                                            //здесь делается мигание.
            if (cnt == 6 || cnt == 7 || cnt == 8)                        //здесь делается мигание.
            {                                                            //здесь делается мигание.
                g.DrawImage(Resources.door3, this.Pos.X, this.Pos.Y);    //здесь делается мигание.
            }                                                            //здесь делается мигание.
            cnt++;                                                       //здесь делается мигание.
            if (cnt > 8)                                                 //здесь делается мигание.
            {                                                            //здесь делается мигание.
                cnt = 0;                                                 //здесь делается мигание.
            }
        }

        private int cnt = 0;
    }
}
