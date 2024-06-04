using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projekt_programko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Game : Window
    {
        public int Score;
        public int TilesCount;
        Random Rand = new Random();
        Button myButton;
        int Size = 4;
        int Increment = 2;
        int MaxNew = 4;
        int MinNew = 2;
        Button[,] Tiles = new Button[16, 16];
        Button[,] BuffTiles = new Button[16, 16];
        int[,] VirtualTiles = new int[16, 16];
        public Game()
        {
            InitializeComponent();
            InitGame();
            SpawnTiles();
            this.PreviewKeyDown += Game_PreviewKeyDown;
        }

        private void InitGame()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Tiles[x, y] = new Button();
                    Tiles[x, y].Content = "";
                    TheGrid.Children.Add(Tiles[x, y]);
                    Grid.SetColumnSpan(Tiles[x, y], 16 / Size);
                    Grid.SetRowSpan(Tiles[x, y], 16 / Size);
                    Grid.SetColumn(Tiles[x, y], x * (16 / Size) + 1);
                    Grid.SetRow(Tiles[x, y], y * (16 / Size) + 1);
                    SetColor(x, y);
                }
            }
        }
        private void Game_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    Move(-1, 0);
                    MergeAll(-1, 0);
                    TheRest();
                    break;
                case Key.Right:
                    Move(1, 0);
                    MergeAll(1, 0);
                    TheRest();
                    break;
                case Key.Up:
                    Move(0, -1);
                    MergeAll(0, -1);
                    TheRest();
                    break;
                case Key.Down:
                    Move(0, 1);
                    MergeAll(0, 1);
                    TheRest();
                    break;
            }
        }

        private async Task TheRest()
        {
            await Task.Delay(100);
            SpawnTiles();
            await CountScore();

        }

        private async Task CountScore()
        {
            Score = 0;
            TilesCount = 0;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (Tiles[x, y].Content.ToString() != "")
                    {
                        Score += int.Parse(Tiles[x, y].Content.ToString());
                        TilesCount++;
                    }
                }
            }
            ScoreTextBlock.Text = $"Score: {Score}";
        }

        private bool SpawnTiles()
        {
            int x = Randomak();
            int y = Randomak();
            int Runs = 0;
            for (int i = 0; i < Increment; i++)
            {
            znovu:
                if (Tiles[x, y].Content.ToString() == "")
                {
                    Tiles[x, y].Content = "2";
                    SetColor(x, y);
                }
                else
                {
                    x = Randomak();
                    y = Randomak();
                    Runs++;
                    if (Runs > Size * Size * 4)
                    {
                        int q = 0;
                        for (int g = 0; g < 16 / Size; g++)
                        {
                            for (int j = 0; j < 16 / Size; j++)
                            {
                                if (Tiles[g, j].Content.ToString()=="")
                                {
                                    x = g;
                                    y = j;
                                    goto znovu;
                                }

                            }
                        }
                        if (i == 0)
                        {
                            MessageBox.Show($"Prohra :( \nSkóre: {Score}");
                            DeleteAll();
                            InitGame();
                            SpawnTiles();
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    goto znovu;
                }
                Runs = 0;
            }
            return true;
        }
        private int Randomak()
        {
            return Rand.Next(0, Size - 1);
        }
        private void Move(int DirectionX, int DirectionY)
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (Tiles[x, y].Content.ToString() != "")
                    {
                        SetColor(x, y);
                        if (x + DirectionX >= 0 && x + DirectionX <= Size - 1 && y + DirectionY >= 0 && y + DirectionY <= Size - 1)
                        {
                            if (Tiles[x + DirectionX, y + DirectionY].Content.ToString() == "")
                            {
                                Tiles[x + DirectionX, y + DirectionY].Content = Tiles[x, y].Content;
                                RemoveTile(x, y);
                                x = 0;
                                y = -1;
                            }
                        }
                    }
                }
            }
        }

        private void MergeAll(int DirectionX, int DirectionY)
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (Tiles[x, y].Content.ToString() != "")
                    {
                        if (x + DirectionX >= 0 && x + DirectionX <= Size - 1 && y + DirectionY >= 0 && y + DirectionY <= Size - 1)
                        {
                            if (Tiles[x + DirectionX, y + DirectionY].Content.ToString() != "")
                            {
                                if (Tiles[x + DirectionX, y + DirectionY].Content.ToString() == Tiles[x, y].Content.ToString())
                                {
                                    Tiles[x + DirectionX, y + DirectionY].Content = (int.Parse(Tiles[x, y].Content.ToString()) * 2).ToString();
                                    RemoveTile(x, y);
                                    SetColor(x + DirectionX, y + DirectionY);
                                    x = 0;
                                    y = -1;
                                }
                            }
                        }
                    }
                }
            }
            Move(DirectionX, DirectionY);
        }

        private void RemoveTile(int x, int y)
        {
            Tiles[x, y].Content="";
            Tiles[x, y].Background = Brushes.White;
            Tiles[x, y].Foreground = Brushes.Black;
        }


        private void DeleteAll()
        {
            for (int i = TheGrid.Children.Count - 1; i >= 0; i--)
            {
                if (TheGrid.Children[i] is Button button && Tiles.Cast<Button>().Contains(button))
                {
                    TheGrid.Children.RemoveAt(i);
                }
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Tiles[i, j] = null;
                }
            }
        }


        private void SetColor(int x, int y)
        {
            int value = 0;
            if (Tiles[x, y].Content.ToString() != "")
                value = int.Parse(Tiles[x, y].Content.ToString());
            int r = 255;
            int g = 255;
            int b = 255;
            int multiplier = 50;
            int valuesValue = IncreasedByTimes(value);
            if (r - (valuesValue * multiplier) > 0)
            {
                r -= valuesValue * multiplier;
            }
            else if (g - ((valuesValue - 5) * multiplier) > 0)
            {
                r = 0;
                g -= ((valuesValue - 5) * multiplier);
            }
            else if (b - ((valuesValue - 10) * multiplier) > 0)
            {
                r = 0;
                g = 0;
                b -= ((valuesValue - 10) * multiplier);
            }
            else
            {
                r = 255;
                g = 255;
                b = 255;
            }
            if (r + g + b < 382)
            {
                Tiles[x, y].Foreground = Brushes.White;
            }
            Tiles[x, y].Background = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
        }
        private int IncreasedByTimes(int number)
        {
            int count = 0;

            while (number > 1)
            {
                if (number % 2 != 0)
                {
                    // The number is not a power of 2
                    throw new ArgumentException("The number is not a power of 2.");
                }

                number /= 2;
                count++;
            }

            return count;
        }
        private void NextStep(object sender, RoutedEventArgs e)
        {
            UseVirtual();
            (int TilesCount, int TilesSum, int Stakes) Down = (MoveVirtual(0, 1).TilesCount, MoveVirtual(0, 1).TilesSum, 0);
            (int TilesCount, int TilesSum, int Stakes) Up = (MoveVirtual(0, -1).TilesCount, MoveVirtual(0, -1).TilesSum, 0);
            (int TilesCount, int TilesSum, int Stakes) Left = (MoveVirtual(-1, 0).TilesCount, MoveVirtual(-1, 0).TilesSum, 0);
            (int TilesCount, int TilesSum, int Stakes) Right = (MoveVirtual(1, 0).TilesCount, MoveVirtual(1, 0).TilesSum, 0);
            Down.Stakes = (Down.TilesCount * 2) - Down.TilesSum;
            Up.Stakes = (Up.TilesCount * 2) - Up.TilesSum;
            Right.Stakes = (Right.TilesCount * 2) - Right.TilesSum;
            Left.Stakes = (Left.TilesCount * 2) - Left.TilesSum;
            int top=Down.Stakes;
            if (Up.Stakes < top)
                top = Up.Stakes;
            if (Right.Stakes < top)
                top = Right.Stakes;
            if (Left.Stakes < top)
                top = Left.Stakes;


            if (Down.Stakes == top)
                Move(0, 1);
            if (Up.Stakes == top)
                Move(0, -1);
            if (Right.Stakes == top)
                Move(1, 0);
            if (Left.Stakes == top)
                Move(-1, 0);

        }
        
        

        private (int TilesCount, int TilesSum) MoveVirtual(int DirectionX, int DirectionY)
        {
            int TilesCount = 0;
            int TilesSum = 0;
            
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if(x==0 && y == 0)
                    {
                        TilesCount = 0;
                        TilesSum = 0;
                    }
                    if (VirtualTiles[x, y] != -1)
                    {
                        TilesSum+= VirtualTiles[x, y];
                        TilesCount++;
                        if (x + DirectionX >= 0 && x + DirectionX <= Size - 1 && y + DirectionY >= 0 && y + DirectionY <= Size - 1)
                        {
                            if (VirtualTiles[x + DirectionX, y + DirectionY] == -1)
                            {
                                VirtualTiles[x + DirectionX, y + DirectionY] = VirtualTiles[x,y];
                                x = 0;
                                y = -1;
                            }
                            else if(VirtualTiles[x + DirectionX, y + DirectionY] == VirtualTiles[x, y])
                            {
                                VirtualTiles[x + DirectionX, y + DirectionY] *= 2;
                                VirtualTiles[x, y] = -1;
                                x = 0;
                                y = -1;
                            }
                        }
                    }
                }
            }

            return (TilesCount, TilesSum);
        }

        private void UseVirtual()
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (Tiles[x, y] == null||Tiles[x, y].Content.ToString() == "")
                        VirtualTiles[x, y] = -1;
                    else
                        VirtualTiles[x, y] = int.Parse(Tiles[x, y].Content.ToString());
                }
            }
        }

        private void Solve(object sender, RoutedEventArgs e)
        {
            //while()
        }
    }
}