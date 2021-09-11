using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GXA
{
    public class Host
    {
        public Host(Timer timer)
        {
            this.timer = timer;
            this.State = HostState.hsStart;            
            initObjects();

        }

        /// <summary>
        /// Самый важный метод. Инициализация объектов.
        /// </summary>
        private void initObjects()
        {
            int mapCol = 18 + (this.Level + 2);
            int mapRow = 10 + (this.Level + 2);
            map = new Map(mapCol, mapRow);            
            boy = new Boy();
            boy.Pos = new Position(0, 0);
            boy.MoveSpeed = this.moveSpeed;
            this.walls.Clear();
            int wallcnt = (int)(this.map.Col * this.map.Row * 0.3);
            for (int i = 0; i < wallcnt; )
            {
                //Инициализация позиций стены
                int tCol = Common.SystemRandom.Next(0, map.Col - 1);
                int tRow = Common.SystemRandom.Next(0, map.Row - 1);
                if ((tCol == 0 && tRow == 0) || (tCol == 0 && tRow == 1) || (tCol == 1 && tRow == 0))
                    continue;
                if (map.Grids[tCol, tRow] == GridState.csBlack)
                {
                    Wall wall = new Wall();
                    this.walls.Add(wall);
                    wall.Pos.X = tCol * Common.GridSize;
                    wall.Pos.Y = tRow * Common.GridSize;
                    map.Grids[tCol, tRow] = GridState.csWall;
                    i++;
                }
            }

            enemis.Clear();
            int enemyCnt = 3 + this.Level;
            for (int i = 0; i < enemyCnt; )
            {
                //Инициалищзирую позиции врагов. Чем старше лвл бо\, тем больше врагов
                int tCol = Common.SystemRandom.Next(2, map.Col - 1);
                int tRow = Common.SystemRandom.Next(2, map.Row - 1);
                if (map.Grids[tCol, tRow] == GridState.csBlack)
                {
                    Enemy eny = new Enemy();
                    this.enemis.Add(eny);
                    eny.Pos.X = tCol * Common.GridSize;
                    eny.Pos.Y = tRow * Common.GridSize;  
                    i++;
                }
            }

            int prizeCount = 1 + this.Level;

            prizes.Clear();
            for (int i = 0; i < prizeCount; )
            {
                int tCol = Common.SystemRandom.Next(2, map.Col - 1);
                int tRow = Common.SystemRandom.Next(2, map.Row - 1);
                if (map.Grids[tCol, tRow] == GridState.csWall)
                {
                    Prize p = new Prize(PrizeType.ptPower);
                    int r = Common.SystemRandom.Next(0, 10);
                    if ((r == 1) || (r == 2) || (r == 3))
                    {
                        p = new Prize(PrizeType.ptBomb);
                    }
                    if ((r == 4) || (r == 5) || (r == 6))
                    {
                        p = new Prize(PrizeType.ptSpeed);
                    }
                    if (r == 7)
                    {
                        p = new Prize(PrizeType.ptPlayer);
                    }
                    p.Pos.X = tCol * Common.GridSize;
                    p.Pos.Y = tRow * Common.GridSize;
                    prizes.Add(p);
                    i++;
                } 
            }

            //Инициализирую переход на след. уровень
            while (true)
            {
                int tCol = Common.SystemRandom.Next(2, map.Col - 1);
                int tRow = Common.SystemRandom.Next(2, map.Row - 1);
                if (map.Grids[tCol, tRow] == GridState.csWall)
                {
                    door = new NextStageDoor();
                    door.Pos.X = tCol * Common.GridSize;
                    door.Pos.Y = tRow * Common.GridSize;
                    break;
                }
            }

            this.removeBombs.Clear();
            this.removeEnemis.Clear();
            this.removeFires.Clear();
            this.removePrizes.Clear();
            this.removeWalls.Clear();
            this.fires.Clear();

            if (MapSizeChanged != null)
            {
                MapSizeChanged();
            }
        }

        #region Fields
        private Timer timer;

        private Map map;

        public void PauseOrResume()
        {
            timer.Enabled = !timer.Enabled;
        }

        private Boy boy;

        private NextStageDoor door;

        private int StateChangeEslaped = 1;  

        private HostState state = HostState.hsStart;

        #endregion

        private HostState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                StateChangeEslaped = 1;
            }
        }

        private bool drawMaskOut(Graphics g)
        {
            if (StateChangeEslaped < 50)
            {
                int w = 0;
                int h = 0;
                GetSize(ref w, ref h);
                g.FillRectangle(Common.BlackBrush, 0, 0 + (StateChangeEslaped * 10), w, h);
                StateChangeEslaped++;
                return false;
            }
            return true;
        }

        private bool drawMaskIn(Graphics g)
        {
            int w = 0;
            int h = 0;
            GetSize(ref w, ref h);
            if (StateChangeEslaped < 50)
            {
                g.FillRectangle(Common.BlackBrush, 0, (0 - h) + (StateChangeEslaped * 7), w, h);
                StateChangeEslaped++;
                return false;
            }
            g.FillRectangle(Common.BlackBrush, 0, 0, w, h);
            return true;
        }

        #region Fields
        private IList<Enemy> enemis = new List<Enemy>();
        private IList<Bomb> bombs = new List<Bomb>();
        private IList<Wall> walls = new List<Wall>();
        private IList<Fire> fires = new List<Fire>();
        private IList<Prize> prizes = new List<Prize>();
        private IList<Fire> removeFires = new List<Fire>();
        private IList<Bomb> removeBombs = new List<Bomb>();
        private IList<Enemy> removeEnemis = new List<Enemy>();
        private IList<Wall> removeWalls = new List<Wall>();  
        private IList<Prize> removePrizes = new List<Prize>();

        private int bombCountLimit = 1;
        private int fireDistance = 1;
        private int moveSpeed = 3;
        public int Level = 1;
        private const int LevelLimit = 5;
        public int BoyCout = 3;
        public int score = 0;

        public event MapSizeEventHandler MapSizeChanged;
        #endregion
        
        private void drawStaticObject(Graphics g)
        {
            map.Draw(g);           

            door.Draw(g);

            for (int i = 0; i < this.prizes.Count; i++)
            {
                prizes[i].Draw(g);
            }         

            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(g);
            }

            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].Draw(g);
            }
            
        }

        public void Draw(Graphics g)
        {            
            if (State == HostState.hsStart)
            {
                ////////////////////////////Audio aud = new Audio();
                
                string title = "BoomberMan";
                Color c = (Common.ActiveBursh as SolidBrush).Color;
                int r = c.R;
                int gr = c.G;
                int b = c.B;
                if (r < 250)
                {
                    r += 4;
                }
                else
                {
                    r = 1;
                    gr += 5;
                    if (gr < 250)
                    {
                        gr += 4;
                    }
                    else
	                {                       
                        while (gr > 0)
                        {
                            gr -= 4;
                        }                      
	                }                        
                }
             
                c = Color.FromArgb(r, gr, b);
                (Common.ActiveBursh as SolidBrush).Color = c; 
                g.DrawString(title, Common.TitleFont, Common.ActiveBursh, (map.Col / 5) * Common.GridSize - 50, (map.Row /2) * Common.GridSize - 50 );
                string s = ("Нажмите Enter для продолжения... \n Нажмите Shift для кампании...");
                g.DrawString(s, Common.LargeFont, Common.WhiteBrush, 0, (map.Row - 1) * Common.GridSize);
                return;
            }

            if (State == HostState.hsStageClear)
            {                
                drawStaticObject(g);
                drawMaskIn(g);
                string s = "Уровень пройден! Поздравляю! Нажмите Enter для продолжения...";
                g.DrawString(s, Common.LargeFont, Common.WhiteBrush, 0, (map.Row - 1) * Common.GridSize);
                return;
            }

            if (State == HostState.hsBoydie)
            {                
                drawStaticObject(g);
                drawMaskIn(g);
                string s = "Вы мертвы. Нажмите Enter для продолжения...";
                g.DrawString(s, Common.LargeFont, Common.WhiteBrush, 0, (map.Row - 1) * Common.GridSize);
                return;
            }
            
            if (State == HostState.hsNormal)
            {
                #region draw static object

                drawStaticObject(g);

                #endregion

                #region draw active object

                boy.Draw(g);

                for (int i = 0; i < this.enemis.Count; i++)
                {
                    enemis[i].Draw(g);
                }

                #endregion

                #region draw film object

                for (int i = 0; i < fires.Count; i++)
                {
                    fires[i].Draw(g);
                }

                #endregion

                #region draw text

                string s = "Уровень:" + this.Level.ToString() + " P:" + this.BoyCout.ToString()
                    + " Сила:" + this.fireDistance.ToString()
                    + " Скорость:" + this.boy.MoveSpeed.ToString()
                    + " Бомбы:" + this.bombCountLimit.ToString()
                    + " Тек.счёт:" + this.score.ToString();

                g.DrawString(s, Common.LargeFont, Common.BlackBrush, 0, (map.Row - 1) * Common.GridSize);
                #endregion
                drawMaskOut(g);

                
                
            }           

        }

        private bool canMove(Character Character)
        {
            int Col = map.Col;
            int Row = map.Row;
            int minx = Character.Pos.X;
            int miny = Character.Pos.Y;
            int maxx = minx + Common.GridSize;
            int maxy = miny + Common.GridSize;

            //создание позиции персонажа           
            Position newPos = Character.Pos;

            switch (Character.Direction)
            {
                case Direction.dDown:
                    newPos.Y += Character.MoveSpeed;
                    break;
                case Direction.dLeft:
                    newPos.X -= Character.MoveSpeed;
                    break;
                case Direction.dRight:
                    newPos.X += Character.MoveSpeed;
                    break;
                case Direction.dUp:
                    newPos.Y -= Character.MoveSpeed;
                    break;
            }

            int minx_new = newPos.X;
            int miny_new = newPos.Y;
            int maxx_new = minx_new + Common.GridSize;
            int maxy_new = miny_new + Common.GridSize;

            //test Character in the map boudray
            if (minx_new < 0 || maxx_new > ((Col - 1) * Common.GridSize) || miny_new < 0 || maxy_new > ((Row - 1) * Common.GridSize))
            {
                return false;
            }

            //get the nearest fix grids' rectangle
            int tmpCol = minx_new / Common.GridSize;
            int tmpRow = miny_new / Common.GridSize;

            //test any porint in the rectangle
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    if (Character is Boy)
                    {
                        if (map.Grids[i, j] == GridState.csFix || map.Grids[i, j] == GridState.csWall)
                        {
                            if (Geometry.IntersectGrid(minx_new, miny_new, maxx_new, maxy_new, i, j))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (map.Grids[i, j] == GridState.csFix || map.Grids[i, j] == GridState.csBomb || map.Grids[i, j] == GridState.csWall)
                        {
                            if (Geometry.IntersectGrid(minx_new, miny_new, maxx_new, maxy_new, i, j))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            //can adjuest new position
            //Snap to road
            switch (Character.Direction)
            {
                case Direction.dDown:
                    AdjustX(Character);

                    break;
                case Direction.dLeft:
                    AdjustY(Character);
                    break;
                case Direction.dRight:
                    AdjustY(Character);
                    break;
                case Direction.dUp:
                    AdjustX(Character);
                    break;
            }

            return true;
        }

        private void AdjustX(Character Character)
        {
            int tCol = Character.Pos.X / Common.GridSize;
            int tx = tCol * Common.GridSize;
            if (Math.Abs(tx - Character.Pos.X) > Common.GridSize / 2)
            {
                Character.Pos.X = (tCol + 1) * Common.GridSize;
            }
            else
            {
                Character.Pos.X = tCol * Common.GridSize;
            }
        }

        private void AdjustY(Character Character)
        {
            int tRow = Character.Pos.Y / Common.GridSize;
            int ty = tRow * Common.GridSize;
            if (Math.Abs(ty - Character.Pos.Y) > Common.GridSize / 2)
            {
                Character.Pos.Y = (tRow + 1) * Common.GridSize;
            }
            else
            {
                Character.Pos.Y = tRow * Common.GridSize;
            }
        }

        private void getColAndRow(Character Character, ref int Col, ref int Row)
        {
            int tRow = Character.Pos.Y / Common.GridSize;
            int tCol = Character.Pos.X / Common.GridSize;
            int tx = tCol * Common.GridSize;
            int ty = tRow * Common.GridSize;
            
            if (Math.Abs(ty - Character.Pos.Y) > Common.GridSize / 2)
            {
                Row = tRow + 1;
            }
            else
            {
                Row = tRow;
            }
            if (Math.Abs(tx - Character.Pos.X) > Common.GridSize / 2)
            {
                Col = tCol + 1;
            }
            else
            {
                Col = tCol;
            }
        }

        private bool canCreateBomb()
        {
            if (bombCountLimit < 1)
            {
                return false;
            }
            int Col = 0, Row = 0;
            getColAndRow(boy, ref Col, ref Row);
            if (map.Grids[Col, Row] == GridState.csBomb)
            {
                return false;
            }

            return true;
        }

        private void ExplodeBomb(Bomb bomb)
        {
            int Col = 0;
            int Row = 0;
            getColAndRow(bomb, ref Col, ref Row);
            if (bombs.Contains(bomb))
            {
                bombs.Remove(bomb);
                map.Grids[Col, Row] = GridState.csBlack;
                this.bombCountLimit++;
            }
            //Отрисовка огня(одновременно) по направлениям.
            //учтена мощь взрыва + длина пробега огня.
            CreateFire(Col, Row);

            #region Left
            int tCol = Col - 1;
            for (int i = 0; i < this.fireDistance; i++)
            {
                if (tCol < 0) { break; }
                else
                {
                    if (map.Grids[tCol, Row] == GridState.csFix)
                    {
                        break;
                    }
                    if (map.Grids[tCol, Row] == GridState.csWall)
                    {
                        CreateFire(tCol, Row);
                        break;
                    }
                    if (map.Grids[tCol, Row] == GridState.csBlack || map.Grids[tCol, Row] == GridState.csBomb)
                    {
                        CreateFire(tCol, Row);
                    }
                }
                tCol--;
            }
            #endregion

            #region Right
            tCol = Col + 1;
            for (int i = 0; i < this.fireDistance; i++)
            {
                if (tCol > this.map.Col - 2) { break; }
                else
                {
                    if (map.Grids[tCol, Row] == GridState.csFix)
                    {
                        break;
                    }
                    if (map.Grids[tCol, Row] == GridState.csWall)
                    {
                        CreateFire(tCol, Row);
                        break;
                    }
                    if (map.Grids[tCol, Row] == GridState.csBlack)
                    {
                        CreateFire(tCol, Row);
                    }
                }
                tCol++;
            }
            #endregion

            #region Down
            int tRow = Row + 1;
            for (int i = 0; i < this.fireDistance; i++)
            {
                if (tRow > this.map.Row - 2) { break; }
                else
                {
                    if (map.Grids[Col, tRow] == GridState.csFix)
                    {
                        break;
                    }
                    if (map.Grids[Col, tRow] == GridState.csWall)
                    {
                        CreateFire(Col, tRow);
                        break;
                    }
                    if (map.Grids[Col, tRow] == GridState.csBlack)
                    {
                        CreateFire(Col, tRow);
                    }
                }
                tRow++;
            }
            #endregion

            #region Up
            tRow = Row - 1;
            for (int i = 0; i < this.fireDistance; i++)
            {
                if (tRow < 0) { break; }
                else
                {
                    if (map.Grids[Col, tRow] == GridState.csFix)
                    {
                        break;
                    }
                    if (map.Grids[Col, tRow] == GridState.csWall)
                    {
                        CreateFire(Col, tRow);
                        break;
                    }
                    if (map.Grids[Col, tRow] == GridState.csBlack)
                    {
                        CreateFire(Col, tRow);
                    }
                }
                tRow--;
            }
        }
        #endregion

        /// <summary>
        /// Создание огня. Зависит от размеров сетки
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Row"></param>
        private void CreateFire(int Col, int Row)
        {
            Fire fire = new Fire();
            fire.Pos.X = Col * Common.GridSize;
            fire.Pos.Y = Row * Common.GridSize;
            this.fires.Add(fire);
        }

        public void Update()
        {
            if (State == HostState.hsStart ||State == HostState.hsBoydie)
            {
                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Enter))
                {
                    State = HostState.hsNormal;
                }
                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Shift))
                {
                    State = HostState.hsCamp;
                } 
            }

            if (State == HostState.hsStageClear)
            {
                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Enter))
                {
                    this.Level = this.Level + 1; //увеличиваем уровень
                    initObjects();
                    State = HostState.hsNormal;
                }                
                return;
            }

            if (State == HostState.hsNormal)
            {
                #region MoveEnemies

                for (int i = 0; i < enemis.Count; i++)
                {
                    if (canMove(enemis[i]))
                    {
                        enemis[i].Move();
                        int r = Common.SystemRandom.Next(30);
                        if (r == 5)
                        {
                            ChangeEnemyDirection(enemis[i]);
                        }
                    }
                    else
                    {
                        ChangeEnemyDirection(enemis[i]);
                    }
                    if (Geometry.IntersectCharacter(enemis[i], boy))
                    {
                        //Смерть персонажа, если пересекаются координаты с 
                        //координатами врага
                        BoyDie();
                        return;
                    }
                }

                #endregion

                #region UpdateBombers

                removeBombs.Clear();
                for (int i = 0; i < bombs.Count; i++)
                {
                    bombs[i].Elapsed++;
                    //если бомба может взорваться
                    if (bombs[i].Elapsed > 70)
                    {
                        if (!removeBombs.Contains(bombs[i]))
                        {
                            removeBombs.Add(bombs[i]);
                        }
                    }
                }

                #endregion

                #region update fires
                removeFires.Clear();
                for (int i = 0; i < fires.Count; i++)
                {
                    fires[i].Elapsed++;
                    if (fires[i].Elapsed > 5)
                    {
                        removeFires.Add(fires[i]);
                    }
                    //update fire destroy Character
                    //boy
                    if (Geometry.IntersectCharacter(fires[i], boy))
                    {
                        //Player die
                        BoyDie();
                        return;
                    }
                    //enemies
                    removeEnemis.Clear();
                    for (int j = 0; j < enemis.Count; j++)
                    {
                        if (Geometry.IntersectCharacter(fires[i], enemis[j]))
                        {
                            //remove enemies
                            removeEnemis.Add(enemis[j]);
                        }
                    }

                    for (int j = 0; j < removeEnemis.Count; j++)
                    {
                        if (this.enemis.Contains(removeEnemis[j]))
                        {
                            enemis.Remove(removeEnemis[j]);
                            this.score += 100;
                        }
                    }
                    //wall
                    removeWalls.Clear();
                    for (int j = 0; j < walls.Count; j++)
                    {
                        if (Geometry.IntersectCharacter(fires[i], walls[j]))
                        {
                            //remove enemies
                            removeWalls.Add(walls[j]);
                        }
                    }

                    for (int j = 0; j < removeWalls.Count; j++)
                    {
                        if (this.walls.Contains(removeWalls[j]))
                        {
                            walls.Remove(removeWalls[j]);
                            int tCol = 0;
                            int tRow = 0;
                            getColAndRow(removeWalls[j], ref tCol, ref tRow);
                            map.Grids[tCol, tRow] = GridState.csBlack;
                            this.score += 10;
                        }
                    }
                    //bomb

                    for (int j = 0; j < bombs.Count; j++)
                    {
                        if (Geometry.IntersectCharacter(fires[i], bombs[j]))
                        {
                            if (!removeBombs.Contains(bombs[j]))
                            {
                                removeBombs.Add(bombs[j]);
                            }
                        }
                    }
                }

                for (int i = 0; i < removeFires.Count; i++)
                {
                    if (fires.Contains(removeFires[i]))
                    {
                        fires.Remove(removeFires[i]);
                    }
                }

                for (int i = 0; i < removeBombs.Count; i++)
                {
                    ExplodeBomb(removeBombs[i]);
                }

                #endregion

                #region update prize

                removePrizes.Clear();
                for (int i = 0; i < prizes.Count; i++)
                {
                    if (Geometry.IntersectCharacter(prizes[i], boy))
                    {
                        if (prizes[i].Enable)
                        {
                            switch (prizes[i].Type)
                            {
                                case PrizeType.ptBomb:
                                    this.bombCountLimit++;
                                    break;
                                case PrizeType.ptPlayer:
                                    this.BoyCout++;
                                    break;
                                case PrizeType.ptPower:
                                    this.fireDistance++;
                                    break;
                                case PrizeType.ptSpeed:
               
                                    if (this.moveSpeed < 6)
                                    {
                                        this.moveSpeed++;
                                        boy.MoveSpeed = this.moveSpeed;
                                    }
                                    break;
                            }
                            prizes[i].Enable = false;
                            if (!removePrizes.Contains(prizes[i]))
                            {
                                removePrizes.Add(prizes[i]);
                            }
                        }
                    }
                }

                for (int i = 0; i < removePrizes.Count; i++)
                {
                    if (prizes.Contains(removePrizes[i]))
                    {
                        this.prizes.Remove(removePrizes[i]);
                    }
                }

                #endregion

                #region update nextstage door

                if (this.enemis.Count == 0 && Geometry.IntersectCharacter(boy, door))
                {
                    if (this.Level < LevelLimit)
                    {
                        State = HostState.hsStageClear;
                    }
                    else
                    {
                        this.Level = 1;
                        this.score = 0;
                        State = HostState.hsStart;
                        initObjects();
                    }
                    return;
                }

                #endregion

                #region contorl the object

                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Left))
                {
                    boy.Direction = Direction.dLeft;
                    if (canMove(boy)) //проверяем, может ли двигаться персонаж влево
                    {
                        boy.Move();
                    }
                }

                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Right))
                {
                    boy.Direction = Direction.dRight;
                    if (canMove(boy)) //проверяем, может ли двигаться персонаж вправо
                    {
                        boy.Move();
                    }
                }

                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Up))
                {
                    boy.Direction = Direction.dUp;
                    if (canMove(boy)) //проверяем, может ли двигаться персонаж вврех
                    {
                        boy.Move();
                    }
                }

                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Down))
                {
                    boy.Direction = Direction.dDown;
                    if (canMove(boy)) //проверяем, может ли двигаться персонаж вниз
                    {
                        boy.Move();
                    }
                }

                if (KeyboardState.Instance.GetKeyStateIsDown(Keys.Space))
                {
                    //создаем бомбу
                    if (canCreateBomb()) //если только возможно создание бомбы
                    {
                        int Col = 0, Row = 0;
                        getColAndRow(boy, ref Col, ref Row);
                        Bomb bomb = new Bomb();
                        bomb.Pos.X = Col * Common.GridSize;
                        bomb.Pos.Y = Row * Common.GridSize;
                        this.bombs.Add(bomb);
                        map.Grids[Col, Row] = GridState.csBomb;
                        bombCountLimit--;
                    }
                }

                #endregion
            }
        }

        private void BoyDie() //смерть песонажа
        {
            this.BoyCout--;            
            if (BoyCout < 0) //а вот если счётчик жизней кончился, то мы обнуляем абсолютно всё
            {
                State = HostState.hsStart;
                this.Level = 1;
                this.score = 0;
                this.moveSpeed = 0;
                this.bombCountLimit = 1;
                this.fireDistance = 1;
                this.BoyCout = 3;
                initObjects();
            }
            else
            {
                State = HostState.hsBoydie;
                initObjects();
            }
        }    

        /// <summary>
        /// Метод. Заствляет сменить направление противника. 
        /// Двигается в случайном порядке
        /// </summary>
        /// <param name="eny"></param>
        private void ChangeEnemyDirection(Enemy eny)
        {
            int r = Common.SystemRandom.Next(0, 4);
            switch (r)
            {
                case 0:
                    eny.Direction = Direction.dDown;
                    break;
                case 1:
                    eny.Direction = Direction.dUp;
                    break;
                case 2:
                    eny.Direction = Direction.dRight;
                    break;
                case 3:
                    eny.Direction = Direction.dLeft;
                    break;
            }
        }

        public void GetSize(ref int Width, ref int Height)
        {
            Width = (map.Col - 1)* Common.GridSize + 10;
            Height = map.Row * Common.GridSize + 30;
        }
    }

    public delegate void MapSizeEventHandler();

    public enum HostState
    {
        hsCamp,
        hsStart,
        hsNormal,
        hsBoydie,
        hsStageClear
    }
}
