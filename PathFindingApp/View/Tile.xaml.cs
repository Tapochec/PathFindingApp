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
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        //#region Dependency properties

        //public static readonly DependencyProperty TileStyleProperty = DependencyProperty.Register(
        //    "TileStyle",
        //    typeof(Style),
        //    typeof(Tile));

        //public Style TileStyle
        //{
        //    get { return (Style)GetValue(TileStyleProperty); }
        //    set
        //    {
        //        SetValue(TileStyleProperty, value);
                
        //    }
        //}

        //#endregion

        public Tile()
        {
            InitializeComponent();
        }

        public void SetBackground(Brush brush, int uniformMargin = 1)
        {
            TileLabel.Background = brush;
            TileLabel.Margin = new Thickness(uniformMargin);
        }

        public void SetBorder(Brush brush, int uniformThickness = 4)
        {
            TileBorder.BorderBrush = brush;
            TileBorder.BorderThickness = new Thickness(uniformThickness);
            TileBorder.Margin = new Thickness(-uniformThickness / 2);
        }

        public static Tile Create(Node node)
        {
            return Create(node.Type);
        }

        public static Tile Create(NodeType type)
        {
            Tile tile = new Tile();
            Style tileStyle = TileStyles.Default;

            // Выбор стиля 
            switch (type)
            {
                case NodeType.Visited:
                    tileStyle = TileStyles.Visited;
                    break;

                case NodeType.NotVisited:
                    tileStyle = TileStyles.NotVisited;
                    break;

                case NodeType.NotAvailable:
                    tileStyle = TileStyles.NotAvailable;
                    break;

                case NodeType.Frontier:
                    tileStyle = TileStyles.Frontier;
                    break;

                case NodeType.Active:
                    tileStyle = TileStyles.Active;
                    break;
            }



            tile.Style = tileStyle;
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
                Style style = new Style(typeof(Tile));
                style.Setters.Add(new Setter(
                    Label.BackgroundProperty, labelBrush, nameof(TileLabel)));
                style.Setters.Add(new Setter(
                    Label.MarginProperty, labelMargin, nameof(TileLabel)));

                if (bBrush == null)
                {
                    // Reset border style
                    AddBorderStyle(style, Brushes.Transparent, 0);
                }
                else
                {
                    AddBorderStyle(style, bBrush, bThickness);
                }

                return style;
            }

            private static void AddBorderStyle(Style style, Brush bBrush, int bThickness = 4)
            {
                style.Setters.Add(new Setter(
                    Border.BorderBrushProperty, bBrush, nameof(TileBorder)));
                style.Setters.Add(new Setter(
                    Border.BorderThicknessProperty, bThickness, nameof(TileBorder)));
                style.Setters.Add(new Setter(
                    Border.MarginProperty, -bThickness / 2, nameof(TileBorder)));
            }

            private static Brush FromHex(string hexCode) => new BrushConverter().ConvertFrom('#' + hexCode) as Brush;
        }
    }
}
