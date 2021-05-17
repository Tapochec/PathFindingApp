using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

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
                // TODO: Разобраться с файлом настроек и не городить такие безобразные строчки
                PathfindingLib.GlobalSettings.EightWay = Properties.Settings.Default.EightWay = newVal;
                Properties.Settings.Default.Save();
                SettingChanged?.Invoke();
            }
        }
    }
}
