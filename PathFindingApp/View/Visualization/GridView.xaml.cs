using PathFindingApp.Pathfinding;
using PathFindingApp.Pathfinding.Simulating;
using PathFindingApp.View.Visualization.GridViewEvents;
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

namespace PathFindingApp.View.Visualization
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        public event EventHandler<WallAddedEventArgs> WallAdded;
        public event EventHandler<WallRemovedEventArgs> WallRemoved;

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public bool IsFilled { get; private set; }
        public bool CanEdit { get; private set; }

        public NodeGrid Data { get; set; }

        public GridView()
        {
            InitializeComponent();
        }

        public void InitGrid(int rowCount = 10, int colCount = 10)
        {
            SetXYCount(rowCount, colCount);
            Fill();

            CanEdit = true;
        }

        public void SetXYCount()
        {
            SetXYCount(RowCount, ColCount);
        }

        // Устанавливает количество строк и столбцов в Grid
        public void SetXYCount(int xCount, int yCount)
        {
            RowCount = xCount;
            ColCount = yCount;

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

        private void SourceGridOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!CanEdit)
            {
                MessageBox.Show("Нельзя редактировать");
                return;
            }

            Point point = e.GetPosition(SourceGrid);
            double actualEdge = SourceGrid.RowDefinitions[0].ActualHeight;
            int x = Convert.ToInt32(Math.Floor(point.X / actualEdge));
            int y = Convert.ToInt32(Math.Floor(point.Y / actualEdge));

            Tile clickedTile = SourceGrid.Children[y * 10 + x] as Tile;
            
            switch (clickedTile.Type)
            {
                case NodeType.NotAvailable:
                    clickedTile.LabelStyle = TileStyles.NotVisited;
                    clickedTile.Type = NodeType.NotVisited;

                    OnWallRemoved(new WallRemovedEventArgs(x, y));
                    break;

                default:
                    clickedTile.LabelStyle = TileStyles.NotAvailable;
                    clickedTile.Type = NodeType.NotAvailable;

                    OnWallAdded(new WallAddedEventArgs(x, y));
                    break;
            }
        }

        private void OnWallAdded(WallAddedEventArgs e)
        {
            WallAdded?.Invoke(SourceGrid, e);
        }

        private void OnWallRemoved(WallRemovedEventArgs e)
        {
            WallRemoved?.Invoke(SourceGrid, e);
        }

        public void Clear()
        {
            SourceGrid.Children.Clear();
            Fill();

            //Data = null;
            IsFilled = false;
            CanEdit = true;
        }

        public void Fill()
        {
            // Заполнение пустыми ячейками
            for (int y = 0; y < ColCount; y++)
            {
                for (int x = 0; x < RowCount; x++)
                {
                    Tile tile = Tile.Create(NodeType.NotVisited);
                    //tile.LabelText = "0";

                    Grid.SetRow(tile, y);
                    Grid.SetColumn(tile, x);

                    SourceGrid.Children.Add(tile);
                }
            }
        }

        public void Fill(NodeGrid grid)
        {
            Data = grid;

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
            CanEdit = false;
        }

        public void ShowStep(SearchHistory history, int stepIndex)
        {
            if (IsFilled)
                Clear();

            Tile[,] tiles = GetTiles();
            StepHistoryItem currentStep = history.Steps[stepIndex];

            foreach (Tuple<Position, string> tuple in currentStep.Visited)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.LabelStyle = TileStyles.Visited;
                tile.LabelText = tuple.Item2;
                if (tuple.Item1.HasPrev)
                    tile.ArrowDir = new Point(tuple.Item1.PrevX - tuple.Item1.X, tuple.Item1.PrevY - tuple.Item1.Y);
            }
            
            foreach (Tuple<Position, string> tuple in currentStep.Frontier)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.LabelStyle = TileStyles.Frontier;
                tile.LabelText = tuple.Item2;
            }

            foreach (Node node in Data.Walls)
            {
                Tile tile = tiles[node.Pos.X, node.Pos.Y];
                tile.LabelStyle = TileStyles.NotAvailable;
                tile.LabelText = "";
            }

            tiles[currentStep.Active.Item1.X, currentStep.Active.Item1.Y].LabelStyle = TileStyles.Active;

            if ((history.Steps.Count - 1) == stepIndex)
            {
                foreach (Position pos in history.Path)
                {
                    Tile tile = tiles[pos.X, pos.Y];
                    tile.LabelStyle = TileStyles.Path;
                }
            }

            tiles[history.Start.X, history.Start.Y].LabelStyle = TileStyles.Start;
            tiles[history.Goal.X, history.Goal.Y].LabelStyle = TileStyles.Goal;

            IsFilled = true;
            CanEdit = false;
        }

        private Tile[,] GetTiles()
        {
            Tile[,] tiles = new Tile[RowCount, ColCount];

            for (int y = 0; y < ColCount; y++)
            {
                for (int x = 0; x < RowCount; x++)
                {
                    tiles[x, y] = (Content as Grid).Children[y * 10 + x] as Tile;
                }
            }

            return tiles;
        }
    }
}
