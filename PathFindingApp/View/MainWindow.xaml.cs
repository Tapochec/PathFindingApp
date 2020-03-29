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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NodeGrid _nodeGrid;
        private List<StepHistoryItem> _stepsHistory;
        private int _currentStep = -1;

        public MainWindow()
        {
            InitializeComponent();

            GridViewBorder.SizeChanged += OnGridViewBorderSizeChanged;

            // Data
            _nodeGrid = NodeGrid.CreateNodeGrid();
            _stepsHistory = WidthSearch.FillGridWithHistory(_nodeGrid);

            // View
            GridView.InitGrid();
            //GridView.ShowStep(_stepsHistory.Last());
            //_currentStep = _stepsHistory.Count - 1;
        }

        private void OnGridViewBorderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newSize = Math.Min(e.NewSize.Width, e.NewSize.Height);
            GridView.Width = newSize;
            GridView.Height = newSize;
        }

        private void FillViewClick(object sender, RoutedEventArgs e)
        {
            //GridView.Fill(_nodeGrid);
            GridView.ShowStep(_stepsHistory.Last());
            _currentStep = _stepsHistory.Count - 1;
        }

        private void StepForwardClick(object sender, RoutedEventArgs e)
        {
            if (_currentStep >= _stepsHistory.Count - 1)
                return;

            _currentStep++;

            GridView.ShowStep(_stepsHistory[_currentStep]);
        }

        private void StepBackClick(object sender, RoutedEventArgs e)
        {
            if (_currentStep <= 0)
                return;

            _currentStep--;

            GridView.ShowStep(_stepsHistory[_currentStep]);
        }

        private void ClearViewClick(object sender, RoutedEventArgs e)
        {
            GridView.Clear();
            _currentStep = -1;
        }
    }
}
