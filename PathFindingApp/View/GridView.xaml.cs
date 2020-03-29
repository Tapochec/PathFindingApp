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
        public bool IsEditMode { get; private set; }

        public GridView()
        {
            InitializeComponent();
        }

        public void InitGrid(int xCount = 10, int yCount = 10)
        {
            SetXYCount(xCount, yCount);

            Fill();
        }

        public void SetXYCount()
        {
            SetXYCount(WidthCount, HeightCount);
        }

        // Устанавливает количество строк и столбцов в Grid
        public void SetXYCount(int xCount, int yCount)
        {
            WidthCount = xCount;
            HeightCount = yCount;

            SourceGrid.RowDefinitions.Clear();
            SourceGrid.ColumnDefinitions.Clear();

            for (int x = 0; x < xCount; x++)
            {
                var col = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                SourceGrid.ColumnDefinitions.Add(col);
            }
            for (int y = 0; y < yCount; y++)
            {
                var row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                SourceGrid.RowDefinitions.Add(row);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void Clear()
        {
            SourceGrid.Children.Clear();
            Fill();

            IsFilled = false;
        }

        public void Fill()
        {
            // Заполнение пустыми ячейками
            for (int y = 0; y < HeightCount; y++)
            {
                for (int x = 0; x < WidthCount; x++)
                {
                    Tile tile = Tile.Create(NodeType.NotVisited);
                    tile.LabelText = "0";

                    Grid.SetRow(tile, y);
                    Grid.SetColumn(tile, x);

                    SourceGrid.Children.Add(tile);
                }
            }
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
