using LibraryFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGame
{
    public class WorkGame
    {
        public int[,] playfield = new int[,]
           {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
           };
        public int[,] smallfield = new int[4, 4];
        public const int HEIGHT = 20;
        public const int WIDTH = 10;

        public Figures currentFigures = new Figures(0, 3);
        public WorkGame()
        {

        }

        public bool isCollision()
        {
            for (int y = 0; y < currentFigures.figures.GetLength(0); y++)
            {
                for (int x = 0; x < currentFigures.figures.GetLength(1); x++)
                {
                    if (currentFigures.figures[y, x] == 1 && (currentFigures.pointY + y == playfield.GetLength(0)
                        || currentFigures.pointX + x < 0 || currentFigures.pointX + x == playfield.GetLength(1)))
                    {
                        return true;
                    }
                    else if (currentFigures.figures[y, x] == 1 && playfield[currentFigures.pointY + y, currentFigures.pointX + x] == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void AddFigure()
        {
            ClearFigures();
            for (int y = 0; y < currentFigures.figures.GetLength(0); y++)
            {
                for (int x = 0; x < currentFigures.figures.GetLength(1); x++)
                {
                    if (currentFigures.figures[y, x] == 1)
                    {
                        if (currentFigures.pointX + x == 10)
                        {
                            currentFigures.pointX--;
                            playfield[currentFigures.pointY + y, currentFigures.pointX + x] = currentFigures.figures[y, x];
                        }
                        else if (currentFigures.pointX + x == -1)
                        {
                            currentFigures.pointX++;
                            playfield[currentFigures.pointY + y, currentFigures.pointX + x] = currentFigures.figures[y, x];
                        }
                        else if (currentFigures.pointY + y == 20)
                        {
                            currentFigures.pointY--;
                            playfield[currentFigures.pointY + y, currentFigures.pointX + x] = currentFigures.figures[y, x];
                        }
                        else
                        {
                            playfield[currentFigures.pointY + y, currentFigures.pointX + x] = currentFigures.figures[y, x];
                        }
                    }
                }
            }

        }

        public void NewAddFigure()
        {
            currentFigures.pointX = (10 - currentFigures.figures.GetLength(0)) / 2;
            currentFigures.pointY = 0;
            Array.Copy(currentFigures.nextFigure, currentFigures.figures, currentFigures.figures.Length);
            currentFigures.CurrentColorsFigure = currentFigures.NextColorsFigure;
        }
        public void NewFigure()
        {
            // Метод додавання нової фігури.
            Random random = new Random();
            int randFigur = random.Next(0, 7);
            string name_figures = "IOLJSZT";
            for (int i = 0; i < name_figures.Length; i++)
            {
                if (i == randFigur)
                {
                    currentFigures.GiveFigure(Char.ToString(name_figures[i]));
                    break;
                }
            }
        }
        public void ClearFigures()
        {
            // Простий метод очищення фігур на полі, перетираємо на нолі.
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    if (playfield[y, x] == 1)
                    {
                        playfield[y, x] = 0;
                    }
                }
            }
        }
        public void FigureMoveDown()
        {
            currentFigures.pointY++;
        }
        public void FigureMoveRight()
        {
            // Переміщення фігурки вправо.           
            currentFigures.pointX++;
        }
        public void FigureMoveLeft()
        {
            // Переміщення фігурки вліво.
            currentFigures.pointX--;
        }
        public void RotateFigure()
        {
            int k = 0;
            int f = 0;
            Array.Copy(currentFigures.figures, currentFigures.previousfigure, currentFigures.previousfigure.Length);
            for (int i = currentFigures.figures.GetLength(0) - 1; i >= 0; i--, f++)
            {
                for (int j = 0; j < currentFigures.figures.GetLength(0); j++)
                {
                    switch (j)
                    {
                        case 0:
                            if (k == 0)
                            {
                                currentFigures.temp[k, f] = currentFigures.figures[i, j];
                                k++;
                            }
                            break;
                        case 1:
                            if (k == 1)
                            {
                                currentFigures.temp[k, f] = currentFigures.figures[i, j];
                                k++;
                            }
                            break;
                        case 2:
                            if (k == 2)
                            {
                                currentFigures.temp[k, f] = currentFigures.figures[i, j];
                                k++;
                            }
                            break;
                        case 3:
                            if (k == 3)
                            {
                                currentFigures.temp[k, f] = currentFigures.figures[i, j];
                                k++;
                            }
                            break;
                    }
                }
                k = 0;

            }
            Array.Copy(currentFigures.temp, currentFigures.figures, currentFigures.figures.Length);
        }
        public void FixFigure()
        {
            // Метод який закріплює фігурки на полі.
            for (int y = HEIGHT - 1; y >= 0; y--)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    if (playfield[y, x] == 1)
                    {
                        playfield[y, x] = 2;
                    }
                }
            }
        }

        public void NextFigures()
        {
            NewFigure();
            NextAddFigures();
        }
        public void NextAddFigures()
        {
            for (int y = 0; y < currentFigures.nextFigure.GetLength(0); y++)
            {
                for (int x = 0; x < currentFigures.nextFigure.GetLength(1); x++)
                {
                    smallfield[y, x] = currentFigures.nextFigure[y, x];
                }
            }
        }
    }
    public class Level
    {
        public int Score { get; set; }
        public int GameSpeed { get; set; }
        public int NextLevelScore { get; set; }
    }
    public class UserLevel : Level
    {
        public void GiveLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Score = 20;
                    GameSpeed = 550;
                    NextLevelScore = 500;
                    break;
                case 2:
                    Score = 40;
                    GameSpeed = 450;
                    NextLevelScore = 1200;
                    break;
                case 3:
                    Score = 60;
                    GameSpeed = 400;
                    NextLevelScore = 2500;
                    break;
                case 4:
                    Score = 80;
                    GameSpeed = 350;
                    NextLevelScore = 4000;
                    break;
                case 5:
                    Score = 120;
                    GameSpeed = 300;
                    NextLevelScore = 5500;
                    break;
                case 6:
                    Score = 160;
                    GameSpeed = 250;
                    NextLevelScore = 6500;
                    break;
                case 7:
                    Score = 200;
                    GameSpeed = 200;
                    NextLevelScore = 20000;
                    break;
                default:
                    Score = 10;
                    GameSpeed = 800;
                    NextLevelScore = 500;
                    break;
            }
        }
    }
}
