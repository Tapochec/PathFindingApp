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

        public Tile()
        {
            InitializeComponent();
        }

        //public void SetBorder(Brush brush, int uniformThickness = 4)
        //{
        //    TileBorder.BorderBrush = brush;
        //    TileBorder.BorderThickness = new Thickness(uniformThickness);
        //    TileBorder.Margin = new Thickness(-uniformThickness / 2);
        //}

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

            tile.LabelStyle = labelStyle;
            return tile;
        }

        //public static class TileBrushes
        //{
        //    public static Brush Default => Brushes.White;
        //    public static Brush Visited => FromHex("ccbfb3"); // Изведанная клетка
        //    public static Brush NotVisited => FromHex("ddd5d5"); // Не изведанная клетка
        //    public static Brush NotAvailable => FromHex("868679"); // Недоступная клетка (например стена)
        //    public static Brush Frontier => FromHex("6688cc"); // Граница
        //    public static Brush Active => FromHex("d6e87d"); // Текущая активная ячейка
        //    public static Brush NeibghorBorder => FromHex("40bf80"); // Рамка соседа
        //    public static Brush Start => FromHex("bf4040"); // Старт
        //    public static Brush Path => FromHex("9540bf"); // Путь

        //    private static Brush FromHex(string hexCode) => new BrushConverter().ConvertFrom('#' + hexCode) as Brush;
        //}
    }

    public static class TileStyles
    {
        public static readonly Style Default = Create(Brushes.White);
        public static readonly Style Visited = Create(FromHex("ccbfb3"));
        public static readonly Style NotVisited = Create(FromHex("ddd5d5"));
        public static readonly Style NotAvailable = Create(FromHex("868679"), null, 0);
        public static readonly Style Frontier = Create(FromHex("6688cc"));
        public static readonly Style Active = Create(FromHex("d6e87d"));

        private static Style Create(Brush labelBrush, Brush bBrush = null, int labelMargin = 1, int bThickness = 4)
        {
            Style style = new Style(typeof(Label));
            style.Setters.Add(new Setter(Label.BackgroundProperty, labelBrush));
            style.Setters.Add(new Setter(Label.MarginProperty, new Thickness(labelMargin)));

            return style;
        }

        private static void AddBorderStyle(Style style, Brush bBrush, int bThickness = 4)
        {
            style.Setters.Add(new Setter(
                Border.BorderBrushProperty, bBrush, "TileBorder"));
            style.Setters.Add(new Setter(
                Border.BorderThicknessProperty, new Thickness(bThickness), "TileBorder"));
            style.Setters.Add(new Setter(
                Border.MarginProperty, new Thickness(-bThickness / 2), "TileBorder"));
        }

        private static Brush FromHex(string hexCode) => new BrushConverter().ConvertFrom('#' + hexCode) as Brush;
    }

}
