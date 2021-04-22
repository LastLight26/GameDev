using CoreGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Line rowLine;                                                       // Рядки ігрового поля.
        Line columnLine;                                                    // Колонки ігрового поля.
        const int RETREAT = 20;                                             // Відступ.
        const int SIZECELL = 20;                                            // Розмір комірки.
        const int DELTASIZECELL = 18;                                       // Розмір пікселя.
        const int ROWGRID = 20;                                             // Кількість рядків в ігровому полі.
        const int COLUMNGRID = 10;                                          // Кількість колонок в ігровому полі.

        /* -----Маленьке превю поле фігури------- */

        Line smallRowLine;                                                  // Рядки превю поля.
        Line smallColumnLine;                                               // Колонки превю поля.
        const int SMALLROWGRID = 4;                                         // Кількість рядків в превю поля.
        const int SMALLCOLUMNGRID = 4;                                      // Кількість колонок в превю поля.

        DispatcherTimer timer;
        Path pix;
        RectangleGeometry shellpixel;
        int score = 0;
        int allLine = 0;
        int counterLevel = 1;
        bool isPause = false;
        bool isGameOver = false;
        UserLevel userLevel = new UserLevel();

        public MainWindow()
        {
            InitializeComponent();
            InitializationContent();
            DrawGrid();
            SmallGridShow();

        }
        void InitializationContent()
        {
            Uri iconUri = new Uri("C:/Users/Serik/Documents/Visual Studio 2019/Project/WPF/TetrisGame/TetrisGame/image/Tetris32.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
        }
        WorkGame core_tetris = new WorkGame();

        void DrawGrid()
        {
            for (int i = 0; i <= COLUMNGRID; i++)
            {
                rowLine = new Line();
                rowLine.X1 = RETREAT + i * SIZECELL;
                rowLine.Y1 = RETREAT;
                rowLine.X2 = RETREAT + i * SIZECELL;
                rowLine.Y2 = 420;
                rowLine.Stroke = Brushes.Black;
                FieldGrid.Children.Add(rowLine);
                Grid.SetColumn(rowLine, 0);
                Grid.SetRow(rowLine, 1);
            }
            for (int i = 0; i <= ROWGRID; i++)
            {
                columnLine = new Line();
                columnLine.X1 = 220;
                columnLine.Y1 = RETREAT + i * SIZECELL;
                columnLine.X2 = RETREAT;
                columnLine.Y2 = RETREAT + i * SIZECELL;
                columnLine.Stroke = Brushes.Black;
                FieldGrid.Children.Add(columnLine);
                Grid.SetColumn(columnLine, 0);
                Grid.SetRow(columnLine, 1);
            }
        }

        void SmallGridShow()
        {
            for (int i = 0; i <= SMALLCOLUMNGRID; i++)
            {
                smallRowLine = new Line();
                smallRowLine.X1 = RETREAT + i * SIZECELL;
                smallRowLine.Y1 = RETREAT;
                smallRowLine.X2 = RETREAT + i * SIZECELL;
                smallRowLine.Y2 = 100;
                smallRowLine.Stroke = Brushes.Black;
                BaseGrid.Children.Add(smallRowLine);
                Grid.SetColumn(smallRowLine, 1);
                Grid.SetRow(smallRowLine, 3);
            }
            for (int i = 0; i <= SMALLROWGRID; i++)
            {
                smallColumnLine = new Line();
                smallColumnLine.X1 = 100;
                smallColumnLine.Y1 = RETREAT + i * SIZECELL;
                smallColumnLine.X2 = RETREAT;
                smallColumnLine.Y2 = RETREAT + i * SIZECELL;
                smallColumnLine.Stroke = Brushes.Black;
                BaseGrid.Children.Add(smallColumnLine);
                Grid.SetColumn(smallColumnLine, 1);
                Grid.SetRow(smallColumnLine, 3);
            }
        }
        void PrevDrawFigure()
        {
            SmallGrid.Children.Clear();
            for (int dot_y = 0; dot_y < 4; dot_y++)
            {
                for (int dot_x = 0; dot_x < 4; dot_x++)
                {
                    if (core_tetris.smallfield[dot_y, dot_x] == 1)
                    {
                        pix = new Path();
                        shellpixel = new RectangleGeometry();
                        shellpixel.Rect = new Rect((RETREAT + dot_x * SIZECELL + 1), (RETREAT + dot_y * SIZECELL + 1), DELTASIZECELL, DELTASIZECELL);
                        pix.Data = shellpixel;
                        pix.Fill = core_tetris.currentFigures.NextColorsFigure;

                        SmallGrid.Children.Add(pix);
                        Grid.SetColumn(pix, 1);
                        Grid.SetRow(pix, 3);
                    }
                }
            }
        }

        void DrawFigure()
        {
            for (int dot_y = 0; dot_y < WorkGame.HEIGHT; dot_y++)
            {
                for (int dot_x = 0; dot_x < WorkGame.WIDTH; dot_x++)
                {
                    if (core_tetris.playfield[dot_y, dot_x] == 1)
                    {
                        SetColorPixel(dot_x, dot_y, core_tetris.currentFigures.CurrentColorsFigure);
                    }
                    else if (core_tetris.playfield[dot_y, dot_x] == 2)
                    {
                        SetColorFixPixel(dot_x, dot_y);
                    }
                }
            }
        }
        void SetColorPixel(int _x, int _y, SolidColorBrush color)
        {
            pix = new Path();
            shellpixel = new RectangleGeometry();
            shellpixel.Rect = new Rect((RETREAT + _x * SIZECELL + 1), (RETREAT + _y * SIZECELL + 1), DELTASIZECELL, DELTASIZECELL);
            pix.Data = shellpixel;
            pix.Fill = color;

            FieldGrid.Children.Add(pix);
            Grid.SetColumn(pix, 0);
            Grid.SetRow(pix, 1);
        }
        void ClearField()
        {
            FieldGrid.Children.Clear();
        }

        void ReDrawPixel()
        {
            for (int dot_y = 0; dot_y < WorkGame.HEIGHT; dot_y++)
            {
                for (int dot_x = 0; dot_x < WorkGame.WIDTH; dot_x++)
                {
                    if (core_tetris.playfield[dot_y, dot_x] == 1)
                    {
                        SetColorWhitePixel(dot_x, dot_y);
                    }
                }
            }
        }
        void SetColorFixPixel(int _x, int _y)
        {
            pix = new Path();
            shellpixel = new RectangleGeometry();
            shellpixel.Rect = new Rect((RETREAT + _x * SIZECELL + 1), (RETREAT + _y * SIZECELL + 1), DELTASIZECELL, DELTASIZECELL);
            pix.Data = shellpixel;
            pix.Fill = Brushes.DarkRed;

            FieldGrid.Children.Add(pix);
            Grid.SetColumn(pix, 0);
            Grid.SetRow(pix, 1);
        }
        void SetColorWhitePixel(int _x, int _y)
        {
            pix = new Path();
            shellpixel = new RectangleGeometry();
            shellpixel.Rect = new Rect((RETREAT + _x * SIZECELL + 1), (RETREAT + _y * SIZECELL + 1), DELTASIZECELL, DELTASIZECELL);
            pix.Data = shellpixel;
            pix.Fill = Brushes.White;

            FieldGrid.Children.Add(pix);
            Grid.SetColumn(pix, 0);
            Grid.SetRow(pix, 1);
        }
        void ReDraw()
        {
            ClearField();
            DrawGrid();
            core_tetris.AddFigure();
            DrawFigure();
        }
        void GameOver()
        {
            ClearField();
            TextBlock TextGameOver = new TextBlock();
            TextGameOver.FontFamily = new FontFamily("Segoe Print");
            TextGameOver.FontSize = 15.0;
            TextGameOver.VerticalAlignment = VerticalAlignment.Center;
            TextGameOver.TextAlignment = TextAlignment.Center;
            TextGameOver.Foreground = Brushes.Red;
            TextGameOver.Text = "Game Over!\nClick on a new game or exit";
            FieldGrid.Children.Add(TextGameOver);

            isGameOver = true;
            timer.Stop();
        }

        void MoveDown()
        {
            if (!isPause)
            {
                core_tetris.AddFigure();
                core_tetris.currentFigures.pointY++;

                if (core_tetris.isCollision())
                {
                    core_tetris.currentFigures.pointY--;
                    core_tetris.FixFigure();
                    RemoveLines();
                    core_tetris.NewAddFigure();
                    core_tetris.NextFigures();
                    PrevDrawFigure();
                    if (core_tetris.isCollision())
                    {
                        GameOver();
                    }

                }
            }

        }
        public void RemoveLines()
        {
            bool remove = true;
            int removeLines = 0;
            for (int i = 0; i < WorkGame.HEIGHT; i++)
            {
                for (int j = 0; j < WorkGame.WIDTH; j++)
                {
                    if (core_tetris.playfield[i, j] != 2)
                    {
                        // якщо в лінія не дорівнює закріпленим фігуркам повертємо фолз.(якщо хоча б є одна двойка, то дальше перевіряти не потрібно)
                        remove = false;
                        break;
                    }
                }
                if (remove)
                {
                    // Костилі пішли: перевіряємо поле і знаходимо нашу заповнену лінію, якщо знайшли перетираємо на одинички.(такий собі маркер для малювання)
                    for (int y = 0; y < WorkGame.HEIGHT; y++)
                    {
                        for (int x = 0; x < WorkGame.WIDTH; x++)
                        {
                            if (y == i)
                            {
                                core_tetris.playfield[y, x] = 1;
                            }
                        }
                    }
                    ReDrawPixel(); // Візуально очищуємо ряд в якому були знайдено заповнену лінію.
                    for (int y = i; y >= 0; y--)
                    {
                        /*
                         * Проходимо знову по полю з низу до верху від того місця де знайшли заповнений ряд.
                         */
                        for (int x = 0; x < WorkGame.WIDTH; x++)
                        {
                            //Переносимо ряди зверху до низу.
                            if (core_tetris.playfield[y, x] == 1 && y >= 1)
                            {
                                core_tetris.playfield[y, x] = core_tetris.playfield[y - 1, x];
                                core_tetris.playfield[y - 1, x] = 1;
                            }
                        }
                        ReDrawPixel(); // і обовязково потрібно визивати метод очистки поля, щоб він перетирав траєкторію руху нашого заповненого ряду.
                    }
                    for (int y = 0; y == 0; y++)
                    {
                        for (int x = 0; x < core_tetris.playfield.GetLength(1); x++)
                        {
                            if (core_tetris.playfield[y, x] == 1)
                            {
                                core_tetris.playfield[y, x] = 0;
                            }
                        }
                    }
                    removeLines++;
                }

                remove = true;      // скидаємо прапорец в положення тру.
            }
            DrawFigure();           // визиваємо метод малювання всього що є на полі.

            switch (removeLines)
            {
                case 1:
                    score += userLevel.Score;
                    allLine++;
                    break;
                case 2:
                    score += userLevel.Score * 2;
                    allLine += 2;
                    break;
                case 3:
                    score += userLevel.Score * 2;
                    allLine += 3;
                    break;
                case 4:
                    score += userLevel.Score * 2;
                    allLine += 4;
                    break;
            }
        }
        void TetrisTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler((o, e) =>
            {
                ClearField();
                DrawGrid();
                core_tetris.AddFigure();
                DrawFigure();
                MoveDown();
                Text_Show();
                Level();
            });
            timer.Interval = new TimeSpan(0, 0, 0, 0, userLevel.GameSpeed);
            timer.Start();
        }

        private void button_click(object sender, KeyEventArgs e)
        {
            if (!isPause && !isGameOver)
            {
                // Обробник натиску кнопок, запілив на нампад
                switch (e.Key.ToString())
                {
                    case "NumPad4":
                        core_tetris.FigureMoveLeft();
                        if (core_tetris.isCollision())
                        {
                            core_tetris.FigureMoveRight();
                        }
                        ReDraw();
                        break;
                    case "NumPad6":
                        core_tetris.FigureMoveRight();
                        if (core_tetris.isCollision())
                        {
                            core_tetris.FigureMoveLeft();
                        }
                        ReDraw();
                        break;
                    case "NumPad2":
                        ClearField();
                        MoveDown();
                        if (!isGameOver)
                        {
                            ReDraw();
                        }
                        break;
                    case "NumPad5":
                        core_tetris.RotateFigure();
                        if (core_tetris.isCollision())
                        {
                            Array.Copy(core_tetris.currentFigures.previousfigure, core_tetris.currentFigures.figures, core_tetris.currentFigures.figures.Length);
                        }
                        ReDraw();
                        break;
                }
            }

        }
        void StartGame()
        {
            ButtStart.IsEnabled = false;
            score = 0;
            allLine = 0;
            counterLevel = 1;
            userLevel.GiveLevel(counterLevel);
            core_tetris.NextFigures();
            core_tetris.NewFigure();
            core_tetris.NewAddFigure();

            PrevDrawFigure();

            TetrisTimer();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Array.Clear(core_tetris.playfield, 0, core_tetris.playfield.Length);
            isGameOver = false;
            StartGame();
        }
        private void Exite_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Pause_Game(object sender, RoutedEventArgs e)
        {
            ButtPause.IsEnabled = false;
            ButtContine.IsEnabled = true;
            isPause = true;
        }
        private void Contine_Game(object sender, RoutedEventArgs e)
        {
            ButtPause.IsEnabled = true;
            ButtContine.IsEnabled = false;
            isPause = false;
        }
        private void Text_Show()
        {
            TextScore.Text = $"Score: {score}";
            TextLevel.Text = $"Level: {counterLevel}";
            TextLines.Text = $"Lines: {allLine}";
        }
        private void Level()
        {
            if (score >= userLevel.NextLevelScore)
            {
                counterLevel++;
                userLevel.GiveLevel(counterLevel);
                timer.Interval = new TimeSpan(0, 0, 0, 0, userLevel.GameSpeed);
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\tHome Edition\n   Version 1.0 Beta*(possible bugs)");
        }
    }
}
