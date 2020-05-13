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

            // Data
            _nodeGrid = NodeGrid.CreateNodeGrid();
            _start = _nodeGrid[3, 3];
            _start.Type = NodeType.Start;
            _goal = _nodeGrid[8, 7];
            _goal.Type = NodeType.Goal;
            UpdateSearch();

            // View
            GridView.InitGrid();

            GridView.WallAdded += GridView_WallAdded;
            GridView.WallRemoved += GridView_WallRemoved;
            GridView.StartChanged += GridView_StartChanged;
            GridView.GoalChanged += GridView_GoalChanged;
        }

        private void UpdateSearch()
        {
            _history = WidthSearch.FillGridWithHistory(_nodeGrid, _start, _goal);
        }

        private void ShowLastStep()
        {
            _currentStep = _history.Steps.Count - 1;
            GridView.ShowStep(_history, _currentStep);
        }

        private void UpdateCurrentStepView()
        {
            if (GridView.IsFilled)
            {
                if (_currentStep < _history.Steps.Count)
                    GridView.ShowStep(_history, _currentStep);
                else
                    ShowLastStep();
            }
        }

        private void GridView_WallAdded(object sender, WallAddedEventArgs e)
        {
            _nodeGrid.AddWall(e.X, e.Y);
            UpdateSearch();
            UpdateCurrentStepView();
        }

        private void GridView_WallRemoved(object sender, WallRemovedEventArgs e)
        {
            _nodeGrid.RemoveWall(e.X, e.Y);
            UpdateSearch();
            UpdateCurrentStepView();
        }

        private void GridView_StartChanged(object sender, StartChangedEventArgs e)
        {
            _start.Type = NodeType.NotVisited;

            _start = _nodeGrid[e.X, e.Y];
            _start.Type = NodeType.Start;
            _start.Prev = null;
            UpdateSearch();
            UpdateCurrentStepView();
        }

        private void GridView_GoalChanged(object sender, GoalChangedEventArgs e)
        {
            _goal.Type = NodeType.NotVisited;

            _goal = _nodeGrid[e.X, e.Y];
            _goal.Type = NodeType.Goal;
            _goal.Prev = null;
            UpdateSearch();
            UpdateCurrentStepView();
        }

        #region Mouse input

        private void FillViewClick(object sender, RoutedEventArgs e)
        {
            ShowLastStep();
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
            //_nodeGrid.Clear();
            UpdateSearch();
            GridView.Clear();
            _currentStep = -1;
        }

        #endregion Mouse input
    }
}
