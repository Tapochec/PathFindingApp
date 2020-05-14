using PathFindingApp.View.Settings.Pages;
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
using System.Windows.Shapes;

namespace PathFindingApp.View.Settings
{
    public partial class SettingsWindow : Window
    {
        public bool IsAnySettingChanged { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Global_TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            CurrentSettingGrid.Children.Clear();

            GlobalSettings setting = new GlobalSettings();
            setting.SettingChanged += () => { IsAnySettingChanged = true; };
            CurrentSettingGrid.Children.Add(setting);
        }
    }
}
