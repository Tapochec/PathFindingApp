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

                if (SourceGrid.Children.Count == 2)
                    SourceGrid.Children.RemoveAt(1);

                Polygon poly = new Polygon { Fill = Brushes.Black, Name = "Arrow" };
                // Лево / право
                if (value.X != 0)
                {
                    if (value.X == -1)
                    {
                        poly.Points.Add(new Point(10, 30));
                        poly.Points.Add(new Point(17, 25));
                        poly.Points.Add(new Point(17, 35));
                    }
                    else
                    {
                        poly.Points.Add(new Point(50, 30));
                        poly.Points.Add(new Point(43, 35));
                        poly.Points.Add(new Point(43, 25));
                    }
                }
                // Верх / низ
                else
                {
                    if (value.Y == -1)
                    {
                        poly.Points.Add(new Point(30, 10));
                        poly.Points.Add(new Point(25, 17));
                        poly.Points.Add(new Point(35, 17));
                    }
                    else
                    {
                        poly.Points.Add(new Point(30, 50));
                        poly.Points.Add(new Point(35, 43));
                        poly.Points.Add(new Point(25, 43));
                    }
                }

                if (poly.Points.Count != 0)
                    SourceGrid.Children.Add(poly);
            }
        }

        public NodeType Type = NodeType.NotVisited;

        public Tile()
        {
            InitializeComponent();
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
        public static readonly Style NotAvailable = Create(FromHex("868679"), null, 0);
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
