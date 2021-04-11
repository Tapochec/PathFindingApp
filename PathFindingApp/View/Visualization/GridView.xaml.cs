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
        public enum ViewGridModes
        {
            Detail,
            Plain
        }


        public event EventHandler<WallAddedEventArgs> WallAdded;
        public event EventHandler<WallRemovedEventArgs> WallRemoved;
        public event EventHandler<StartChangedEventArgs> StartChanged;
        public event EventHandler<GoalChangedEventArgs> GoalChanged;


        public delegate void ShowStepDelegate(int stepIndex);


        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public bool IsStepShown { get; private set; }
        public ShowStepDelegate ShowStep { get; private set; }
        public ViewGridModes Mode { get; private set; }


        private Tile _clickedTile;
        public SearchHistory History;
        

        public GridView()
        {
            InitializeComponent();

            MouseLeave += GridView_MouseLeave;
            ShowStep = ShowStepDetail;
        }

        public void Init(SearchHistory history, int rowCount = 10, int colCount = 10)
        {
            History = history;
            SetRowColCount(rowCount, colCount);
            Fill();
        }

        // Устанавливает количество строк и столбцов в SourceGrid
        public void SetRowColCount(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;

            SourceGrid.RowDefinitions.Clear();
            SourceGrid.ColumnDefinitions.Clear();

            for (int x = 0; x < rowCount; x++)
            {
                var col = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                SourceGrid.ColumnDefinitions.Add(col);
            }
            for (int y = 0; y < colCount; y++)
            {
                var row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                SourceGrid.RowDefinitions.Add(row);
            }
        }

        private void DeselectTile()
        {
            _clickedTile.TileLabel.Opacity = 1;
            SourceGrid.MouseUp -= SourceGridMouseUp;
            _clickedTile = null;
        }

        // Полность очищает сетку и заполняет её пустыми ячейками
        public void Clear()
        {
            SourceGrid.Children.Clear();
            Fill();

            IsStepShown = false;
        }

        // Fills SourceGrid with "empty" tiles
        public void Fill()
        {
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

        // Switches view mode to plain
        public void SwitchToPlain()
        {
            if (Mode == ViewGridModes.Plain)
                return;

            ShowStep = ShowStepPlain;
            Mode = ViewGridModes.Plain;
        }

        // Switches view mode to detail
        public void SwitchToDetail()
        {
            if (Mode == ViewGridModes.Detail)
                return;

            ShowStep = ShowStepDetail;
            Mode = ViewGridModes.Detail;
        }

        private void ShowStepDetail(int stepIndex)
        {
            if (IsStepShown)
                Clear();

            Tile[,] tiles = GetTiles();

            if (stepIndex == -1)
            {
                foreach (Position pos in History.NotAvailable)
                {
                    Tile tile = tiles[pos.X, pos.Y];
                    tile.Type = NodeType.NotAvailable;
                    tile.LabelStyle = TileStyles.NotAvailable;
                    tile.LabelText = "";
                    Panel.SetZIndex(tile, 1);
                }

                tiles[History.Start.X, History.Start.Y].LabelStyle = TileStyles.Start;
                tiles[History.Start.X, History.Start.Y].Type = NodeType.Start;

                tiles[History.Goal.X, History.Goal.Y].LabelStyle = TileStyles.Goal;
                tiles[History.Goal.X, History.Goal.Y].Type = NodeType.Goal;

                IsStepShown = true;
                return;
            }

            StepHistoryItem currentStep = History.Steps[stepIndex];

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
                tile.Type = NodeType.Frontier;
                tile.LabelStyle = TileStyles.Frontier;
                tile.LabelText = tuple.Item2;
            }

            foreach (Position pos in History.NotAvailable)
            {
                Tile tile = tiles[pos.X, pos.Y];
                tile.Type = NodeType.NotAvailable;
                tile.LabelStyle = TileStyles.NotAvailable;
                tile.LabelText = "";
                Panel.SetZIndex(tile, 1);
            }

            if ((History.Steps.Count - 1) == stepIndex)
            {
                if (History.Path != null)
                    foreach (Position pos in History.Path)
                    {
                        Tile tile = tiles[pos.X, pos.Y];
                        tile.LabelStyle = TileStyles.Path;
                    }
            }

            tiles[currentStep.Active.Item1.X, currentStep.Active.Item1.Y].LabelStyle = TileStyles.Active;

            tiles[History.Start.X, History.Start.Y].LabelStyle = TileStyles.Start;
            tiles[History.Start.X, History.Start.Y].Type = NodeType.Start;

            tiles[History.Goal.X, History.Goal.Y].LabelStyle = TileStyles.Goal;
            tiles[History.Goal.X, History.Goal.Y].Type = NodeType.Goal;

            IsStepShown = true;
        }

        private void ShowStepPlain(int stepIndex)

        {
            if (IsStepShown)
                Clear();

            Tile[,] tiles = GetTiles();

            if (stepIndex == -1)
            {
                foreach (Position pos in History.NotAvailable)
                {
                    Tile tile = tiles[pos.X, pos.Y];
                    tile.Type = NodeType.NotAvailable;
                    tile.LabelStyle = TileStyles.NotAvailable;
                    //tile.LabelText = "";
                    Panel.SetZIndex(tile, 1);
                }

                tiles[History.Start.X, History.Start.Y].LabelStyle = TileStyles.Start;
                tiles[History.Start.X, History.Start.Y].Type = NodeType.Start;

                tiles[History.Goal.X, History.Goal.Y].LabelStyle = TileStyles.Goal;
                tiles[History.Goal.X, History.Goal.Y].Type = NodeType.Goal;

                IsStepShown = true;
                return;
            }

            StepHistoryItem currentStep = History.Steps[stepIndex];

            foreach (Tuple<Position, string> tuple in currentStep.Visited)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.LabelStyle = TileStyles.Visited;
                //tile.LabelText = tuple.Item2;
                //if (tuple.Item1.HasPrev)
                //    tile.ArrowDir = new Point(tuple.Item1.PrevX - tuple.Item1.X, tuple.Item1.PrevY - tuple.Item1.Y);
            }

            foreach (Tuple<Position, string> tuple in currentStep.Frontier)
            {
                Tile tile = tiles[tuple.Item1.X, tuple.Item1.Y];
                tile.Type = NodeType.Frontier;
                tile.LabelStyle = TileStyles.Frontier;
                //tile.LabelText = tuple.Item2;
            }

            foreach (Position pos in History.NotAvailable)
            {
                Tile tile = tiles[pos.X, pos.Y];
                tile.Type = NodeType.NotAvailable;
                tile.LabelStyle = TileStyles.NotAvailable;
                //tile.LabelText = "";
                Panel.SetZIndex(tile, 1);
            }

            if ((History.Steps.Count - 1) == stepIndex)
            {
                if (History.Path != null)
                    foreach (Position pos in History.Path)
                    {
                        Tile tile = tiles[pos.X, pos.Y];
                        tile.LabelStyle = TileStyles.Path;
                    }
            }

            tiles[currentStep.Active.Item1.X, currentStep.Active.Item1.Y].LabelStyle = TileStyles.Active;

            tiles[History.Start.X, History.Start.Y].LabelStyle = TileStyles.Start;
            tiles[History.Start.X, History.Start.Y].Type = NodeType.Start;

            tiles[History.Goal.X, History.Goal.Y].LabelStyle = TileStyles.Goal;
            tiles[History.Goal.X, History.Goal.Y].Type = NodeType.Goal;

            IsStepShown = true;
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

        #region Events

        private void SourceGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(SourceGrid);
            double actualEdge = SourceGrid.RowDefinitions[0].ActualHeight;
            int x = Convert.ToInt32(Math.Floor(point.X / actualEdge));
            int y = Convert.ToInt32(Math.Floor(point.Y / actualEdge));
            Tile clickedTile = SourceGrid.Children[y * 10 + x] as Tile;

            switch (clickedTile.Type)
            {
                case NodeType.NotAvailable:
                    //clickedTile.LabelStyle = TileStyles.NotVisited;
                    //clickedTile.Type = NodeType.NotVisited;

                    OnWallRemoved(new WallRemovedEventArgs(x, y));
                    break;

                case NodeType.Visited:
                case NodeType.NotVisited:
                case NodeType.Frontier:
                    //clickedTile.LabelStyle = TileStyles.NotAvailable;
                    //clickedTile.Type = NodeType.NotAvailable;
                    //Panel.SetZIndex(clickedTile, 1);

                    OnWallAdded(new WallAddedEventArgs(x, y));
                    break;

                case NodeType.Start:
                case NodeType.Goal:
                    clickedTile.TileLabel.Opacity = 0.5;

                    _clickedTile = clickedTile;
                    SourceGrid.MouseUp += SourceGridMouseUp;
                    break;

                default:
                    break;
            }
        }

        private void SourceGridMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(SourceGrid);
            double actualEdge = SourceGrid.RowDefinitions[0].ActualHeight;
            int x = Convert.ToInt32(Math.Floor(point.X / actualEdge));
            int y = Convert.ToInt32(Math.Floor(point.Y / actualEdge));
            Tile newTile = SourceGrid.Children[y * 10 + x] as Tile;

            if ((_clickedTile == newTile) || (newTile.Type == NodeType.NotAvailable))
            {
                DeselectTile();
                return;
            }

            bool worked = false;
            if (_clickedTile.Type == NodeType.Start && newTile.Type != NodeType.Goal)
            {
                OnStartChanged(new StartChangedEventArgs(x, y));
                worked = true;
            }
            else if (_clickedTile.Type == NodeType.Goal && newTile.Type != NodeType.Start)
            {
                OnGoalChanged(new GoalChangedEventArgs(x, y));
                worked = true;
            }

            if (!worked)
                DeselectTile();

            _clickedTile = null;
            SourceGrid.MouseUp -= SourceGridMouseUp;
        }

        private void GridView_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_clickedTile != null)
                DeselectTile();
        }

        #endregion Events

        #region Event handlers invokes

        private void OnWallAdded(WallAddedEventArgs e)
        {
            WallAdded?.Invoke(SourceGrid, e);
        }

        private void OnWallRemoved(WallRemovedEventArgs e)
        {
            WallRemoved?.Invoke(SourceGrid, e);
        }

        private void OnStartChanged(StartChangedEventArgs e)
        {
            StartChanged?.Invoke(SourceGrid, e);
        }

        private void OnGoalChanged(GoalChangedEventArgs e)
        {
            GoalChanged?.Invoke(SourceGrid, e);
        }

        #endregion Event handlers invokes
    }
}
