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

namespace PathFindingApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Node _start;
        private Node _goal;
        private NodeGrid _nodeGrid;
        private SearchHistory _history;
        private int _currentStep = -1;

        public MainWindow()
        {
            InitializeComponent();

            GridViewBorder.SizeChanged += OnGridViewBorderSizeChanged;

            // Data
            _nodeGrid = NodeGrid.CreateNodeGrid();
            _start = _nodeGrid[3, 3];
            _start.Type = NodeType.Start;
            _goal = _nodeGrid[8, 7];
            _goal.Type = NodeType.Goal;
            UpdateSearch();
            //_history = WidthSearch.FillGridWithHistory(_nodeGrid, _start, _goal);

            // View
            GridView.InitGrid();
            //GridView.Data = _nodeGrid;
            //GridView.ShowStep(_stepsHistory.Last());
            //_currentStep = _stepsHistory.Count - 1;


            GridView.WallAdded += GridView_WallAdded;
            GridView.WallRemoved += GridView_WallRemoved;
        }

        private void UpdateSearch()
        {
            _history = WidthSearch.FillGridWithHistory(_nodeGrid, _start, _goal);
        }

        private void GridView_WallAdded(object sender, WallAddedEventArgs e)
        {
            _nodeGrid.AddWall(e.X, e.Y);
            UpdateSearch();

            if (e.NeedUpdate)
            {
                _currentStep = _history.Steps.Count - 1;
                GridView.ShowStep(_history, _currentStep);
            }
        }

        private void GridView_WallRemoved(object sender, WallRemovedEventArgs e)
        {
            _nodeGrid.RemoveWall(e.X, e.Y);
            UpdateSearch();

            if (e.NeedUpdate)
            {
                _currentStep = _history.Steps.Count - 1;
                GridView.ShowStep(_history, _currentStep);
            }
        }

        private void OnGridViewBorderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double newSize = Math.Min(e.NewSize.Width, e.NewSize.Height);
            //GridView.Width = newSize;
            //GridView.Height = newSize;
        }

        private void FillViewClick(object sender, RoutedEventArgs e)
        {
            //GridView.Fill(_nodeGrid);
            _currentStep = _history.Steps.Count - 1;
            GridView.ShowStep(_history, _currentStep);
        }

        private void StepForwardClick(object sender, RoutedEventArgs e)
        {
            if (_currentStep >= _history.Steps.Count - 1)
                return;

            _currentStep++;

            GridView.ShowStep(_history, _currentStep);
        }

        private void StepBackClick(object sender, RoutedEventArgs e)
        {
            if (_currentStep <= 0)
                return;

            _currentStep--;

            GridView.ShowStep(_history, _currentStep);
        }

        private void ClearViewClick(object sender, RoutedEventArgs e)
        {
            _nodeGrid.Clear();
            UpdateSearch();
            GridView.Clear();
            _currentStep = -1;
        }
    }
}
