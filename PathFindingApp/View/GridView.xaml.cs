using PathFindingApp.Pathfinding;
using PathFindingApp.Pathfinding.Simulating;
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

namespace PathFindingApp.View
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        public int WidthCount { get; private set; }
        public int HeightCount { get; private set; }
        public bool IsFilled { get; private set; }

        public GridView()
        {
            InitializeComponent();
        }

        public void InitGrid(int width = 10, int height = 10)
        {
            WidthCount = width;
            HeightCount = height;

            // Создание самой сетки со столбцами и строками
            Grid newGrid = new Grid { /*Background = Brushes.Black*/ };
            for (int x = 0; x < width; x++)
            {
                var col = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                newGrid.ColumnDefinitions.Add(col);
            }
            for (int y = 0; y < width; y++)
            {
                var row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                newGrid.RowDefinitions.Add(row);
            }

            // Заполнение
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = Tile.Create(NodeType.NotVisited);
                    tile.LabelText = "0";

                    Grid.SetRow(tile, y);
                    Grid.SetColumn(tile, x);

                    newGrid.Children.Add(tile);
                }
            }

            // Добавление созданной сетки в контент
            Content = newGrid;
        }

        public void Clear()
        {
            Content = null;
            InitGrid();
            IsFilled = false;
        }

        public void Fill(NodeGrid grid)
        {
            foreach (object elem in (Content as Grid).Children)
            {
                Tile tile = elem as Tile;
                int x = Grid.GetColumn(tile);
                int y = Grid.GetRow(tile);

                Node node = grid[x, y];
                tile.LabelText = node.Value;
                //label.Background = CellTypeBrushes.GetBrushByType(node.Type);
            }

            IsFilled = true;
        }

        public void ShowStep(StepHistoryItem step)
        {
            if (IsFilled)
                Clear();

            Tile[,] tiles = GetTiles();

            foreach (var tuple in step.Visited)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.LabelStyle = TileStyles.Visited;
                tile.LabelText = tuple.Item2;
            }
            
            foreach (var tuple in step.Frontier)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.LabelStyle = TileStyles.Frontier;
                tile.LabelText = tuple.Item2;
            }

            tiles[step.Active.Item1.X, step.Active.Item1.Y].LabelStyle = TileStyles.Active;

            IsFilled = true;
        }

        private Tile[,] GetTiles()
        {
            Tile[,] tiles = new Tile[WidthCount, HeightCount];

            for (int y = 0; y < HeightCount; y++)
            {
                for (int x = 0; x < WidthCount; x++)
                {
                    tiles[x, y] = (Content as Grid).Children[y * 10 + x] as Tile;
                }
            }

            return tiles;
        }
    }
}
