using PathFindingApp.Pathfinding;
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
        public GridView()
        {
            InitializeComponent();
        }

        public void InitGrid(int width = 10, int height = 10)
        {
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
                    Label label = new Label
                    {
                        Content = "0",
                        Margin = new Thickness(1),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Background = CellTypeBrushes.NotVisited,
                    };
                    Grid.SetRow(label, y);
                    Grid.SetColumn(label, x);
                    newGrid.Children.Add(label);
                }
            }

            // Добавление созданной сетки в контент
            Content = newGrid;
        }

        public void FillGrid(NodeGrid grid)
        {
            foreach (object elem in (Content as Grid).Children)
            {
                Label label = elem as Label;
                int x = Grid.GetColumn(label);
                int y = Grid.GetRow(label);

                Node node = grid[x, y];
                label.Content = node.Value;
                label.Background = CellTypeBrushes.GetBrushByType(node.Type);
            }
        }
    }

    static class CellTypeBrushes
    {
        public static Brush Visited => Brushes.LightGreen;
        public static Brush NotVisited => Brushes.LightGray;
        public static Brush NotAvailable => Brushes.Gray;

        public static Brush GetBrushByType(NodeType type)
        {
            Brush result = null;

            switch (type)
            {
                case NodeType.NotVisited:
                    result = NotVisited;
                    break;

                case NodeType.NotAvailable:
                    result = NotAvailable;
                    break;

                case NodeType.Visited:
                    result = Visited;
                    break;
            }

            return result;
        }
    }
}
