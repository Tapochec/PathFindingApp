using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PathfindingLib.Pathfinding;

namespace PathFindingApp.View.Visualization
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public string LabelText
        {
            get { return (string)TileLabel.Content; }
            set { TileLabel.Content = value; }
        }

        public Style LabelStyle
        {
            get { return TileLabel.Style; }
            set { TileLabel.Style = value; }
        }

        private Point _arrowDir;
        public Point ArrowDir
        {
            set
            {
                if (value == _arrowDir)
                    return;

                _arrowDir = value;

                // Удаление предыдущей стрелки
                Polygon arrow = SourceGrid.Children.OfType<Polygon>().FirstOrDefault();
                if (arrow != null)
                    SourceGrid.Children.Remove(arrow);

                Polygon poly = new Polygon { Fill = Brushes.Black, Name = "Arrow" };
                int x = (int)value.X;
                int y = (int)value.Y;

                // Лево / право
                if (y == 0)
                {
                    poly.Points.Add(new Point(30 + 20 * x, 30));
                    poly.Points.Add(new Point(30 + 13 * x, 25));
                    poly.Points.Add(new Point(30 + 13 * x, 35));
                }
                // Верх / низ
                else if (x == 0)
                {
                    poly.Points.Add(new Point(30, 30 + 20 * y));
                    poly.Points.Add(new Point(25, 30 + 13 * y));
                    poly.Points.Add(new Point(35, 30 + 13 * y));
                }
                // Диагонали
                else
                {
                    poly.Points.Add(new Point(30 + 20 * x, 30 + 20 * y));
                    poly.Points.Add(new Point(30 + 12 * x, 30 + 20 * y));
                    poly.Points.Add(new Point(30 + 20 * x, 30 + 12 * y));
                }

                if (poly.Points.Count != 0)
                    SourceGrid.Children.Add(poly);
            }
        }

        public NodeType Type = NodeType.NotVisited;

        public Tile()
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
        }

        public static Tile Create(Node node)
        {
            return Create(node.Type);
        }

        public static Tile Create(NodeType type)
        {
            Tile tile = new Tile();
            Style labelStyle = TileStyles.Default;

            // Выбор стиля 
            switch (type)
            {
                case NodeType.Visited:
                    labelStyle = TileStyles.Visited;
                    break;

                case NodeType.NotVisited:
                    labelStyle = TileStyles.NotVisited;
                    break;

                case NodeType.NotAvailable:
                    labelStyle = TileStyles.NotAvailable;
                    break;

                case NodeType.Frontier:
                    labelStyle = TileStyles.Frontier;
                    break;

                case NodeType.Active:
                    labelStyle = TileStyles.Active;
                    break;
            }

            tile.Type = type;
            tile.LabelStyle = labelStyle;

            return tile;
        }
    }

    public static class TileStyles
    {
        public static readonly Style Default = Create(Brushes.White);
        public static readonly Style Visited = Create(FromHex("ccbfb3"));
        public static readonly Style NotVisited = Create(FromHex("ddd5d5"));
        public static readonly Style NotAvailable = Create(FromHex("868679"), null, -1);
        public static readonly Style Frontier = Create(FromHex("6688cc"));
        public static readonly Style Active = Create(FromHex("d6e87d"));
        public static readonly Style Start = Create(FromHex("bf4040"));
        public static readonly Style Goal = Create(FromHex("bf3faa"));
        public static readonly Style Path = Create(FromHex("9540bf"));
        //    public static Brush NeibghorBorder => FromHex("40bf80"); // Рамка соседа

        private static Style Create(Brush labelBrush, Brush bBrush = null, int labelMargin = 1, int bThickness = 4)
        {
            Style style = new Style(typeof(Label));
            style.Setters.Add(new Setter(Label.BackgroundProperty, labelBrush));
            style.Setters.Add(new Setter(Label.MarginProperty, new Thickness(labelMargin)));

            return style;
        }

        //private static void AddBorderStyle(Style style, Brush bBrush, int bThickness = 4)
        //{
        //    style.Setters.Add(new Setter(
        //        Border.BorderBrushProperty, bBrush, "TileBorder"));
        //    style.Setters.Add(new Setter(
        //        Border.BorderThicknessProperty, new Thickness(bThickness), "TileBorder"));
        //    style.Setters.Add(new Setter(
        //        Border.MarginProperty, new Thickness(-bThickness / 2), "TileBorder"));
        //}

        private static Brush FromHex(string hexCode) => new BrushConverter().ConvertFrom('#' + hexCode) as Brush;
    }

}
