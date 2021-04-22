using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LibraryFigures
{
    public class TetroFigure
    {
        public int pointX { get; set; }
        public int pointY { get; set; }
        public TetroFigure(int y, int x)
        {
            pointY = y;
            pointX = x;
        }
    }
    public class Figures : TetroFigure
    {
        public string NameFigure { get; set; }
        public SolidColorBrush CurrentColorsFigure { get; set; }
        public SolidColorBrush NextColorsFigure { get; set; }

        public int[,] figures = new int[,]
        {
            {0, 0, 0, 0 },
            {0, 0, 0, 0 },
            {0, 0, 0, 0 },
            {0, 0, 0, 0 }
        };
        public int[,] temp = new int[4, 4];
        public int[,] previousfigure = new int[4, 4];
        public int[,] nextFigure = new int[4, 4];
        public Figures(int _y, int _x)
            : base(_y, _x)
        {
        }

        int[,] I = new int[,]
        {
            { 0, 0, 0, 0 },
            { 1, 1, 1, 1 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
                    };
        int[,] O = new int[,]
        {
            { 0, 1, 1, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] L = new int[,]
        {
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] J = new int[,]
        {
            { 0, 0, 1, 0 },
            { 0, 0, 1, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] S = new int[,]
        {
            { 0, 0, 1, 1 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] Z = new int[,]
        {
            { 1, 1, 0, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] T = new int[,]
        {
            { 0, 1, 0, 0 },
            { 1, 1, 1, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };

        public void GiveFigure(string name)
        {
            switch (name)
            {
                case "I":
                    NameFigure = "I";
                    NextColorsFigure = new SolidColorBrush(Colors.Aqua);
                    Array.Copy(I, nextFigure, figures.Length);
                    break;

                case "O":
                    NameFigure = "O";
                    NextColorsFigure = new SolidColorBrush(Colors.Gold);
                    Array.Copy(O, nextFigure, figures.Length);
                    break;

                case "L":
                    NameFigure = "L";
                    NextColorsFigure = new SolidColorBrush(Colors.Orange);
                    Array.Copy(L, nextFigure, figures.Length);
                    break;

                case "J":
                    NameFigure = "J";
                    NextColorsFigure = new SolidColorBrush(Colors.MediumSlateBlue);
                    Array.Copy(J, nextFigure, figures.Length);
                    break;
                case "S":

                    NameFigure = "S";
                    NextColorsFigure = new SolidColorBrush(Colors.Red);
                    Array.Copy(S, nextFigure, figures.Length);
                    break;

                case "Z":
                    NameFigure = "Z";
                    NextColorsFigure = new SolidColorBrush(Colors.LimeGreen);
                    Array.Copy(Z, nextFigure, figures.Length);
                    break;

                case "T":
                    NameFigure = "T";
                    NextColorsFigure = new SolidColorBrush(Colors.Purple);
                    Array.Copy(T, nextFigure, figures.Length);
                    break;
            }
        }
    }
}
