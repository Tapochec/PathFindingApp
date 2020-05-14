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

namespace PathFindingApp.View.Settings.Pages
{
    /// <summary>
    /// Interaction logic for GlobalSettings.xaml
    /// </summary>
    public partial class GlobalSettings : UserControl
    {
        public event Action SettingChanged;

        public GlobalSettings()
        {
            InitializeComponent();

            EightWayCheckBox.IsChecked = Properties.Settings.Default.EightWay;
        }

        private void EightWay_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool newVal = (bool)EightWayCheckBox.IsChecked;
            if (newVal != Properties.Settings.Default.EightWay)
            {
                Properties.Settings.Default.EightWay = newVal;
                Properties.Settings.Default.Save();
                SettingChanged?.Invoke();
            }
        }
    }
}
