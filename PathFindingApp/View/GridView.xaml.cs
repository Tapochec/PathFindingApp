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
                Label label = elem as Label;
                int x = Grid.GetColumn(label);
                int y = Grid.GetRow(label);

                Node node = grid[x, y];
                label.Content = node.Value;
                label.Background = CellTypeBrushes.GetBrushByType(node.Type);
            }

            IsFilled = true;
        }

        public void ShowStep(StepHistoryItem step)
        {
            if (IsFilled)
                Clear();

            Label[,] labels = GetLabels();

            foreach (var tuple in step.Visited)
            {
                Label label = labels[tuple.Item1.X, tuple.Item1.Y];
                label.Background = CellTypeBrushes.Visited;
                label.Content = tuple.Item2;
            }
            
            foreach (var tuple in step.Frontier)
            {
                Label label = labels[tuple.Item1.X, tuple.Item1.Y];
                label.Background = CellTypeBrushes.Frontier;
                label.Content = tuple.Item2;
            }

            labels[step.Active.Item1.X, step.Active.Item1.Y].Background = CellTypeBrushes.Active;

            IsFilled = true;
        }

        private Label[,] GetLabels()
        {
            Label[,] labels = new Label[WidthCount, HeightCount];

            for (int y = 0; y < HeightCount; y++)
            {
                for (int x = 0; x < WidthCount; x++)
                {
                    labels[x, y] = ((Content as Grid).Children[y * 10 + x] as Label);
                }
            }

            return labels;
        }
    }

    static class CellTypeBrushes
    {
        public static Brush Visited => FromHex("ccbfb3"); // Изведанная клетка
        public static Brush NotVisited => FromHex("ddd5d5"); // Не изведанная клетка
        public static Brush NotAvailable => FromHex("868679"); // Недоступная клетка (например стена)
        public static Brush Frontier => FromHex("6688cc"); // Граница
        public static Brush Active => FromHex("d6e87d"); // Текущая активная ячейка
        public static Brush NeibghorBorder => FromHex("40bf80"); // Рамка соседа
        public static Brush Start => FromHex("bf4040"); // Старт
        public static Brush Path => FromHex("9540bf"); // Путь

        private static Brush FromHex(string hexCode) => new BrushConverter().ConvertFrom('#' + hexCode) as Brush;

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
