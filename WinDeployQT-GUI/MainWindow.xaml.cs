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
using SimpleConfig;
using System.Configuration;
using WinDeployQT_GUI.Datas;
using WinDeployQT_GUI.StaticClasses;

namespace WinDeployQT_GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel viewModel;
        private string _Width = "Width";
        private string _Height = "Height";

        public MainWindow()
        {
            try
            {
                StaticEntity.AppConfigurationSettings = new AppConfiguration("WinDeployQT-GUI.Settings");
                this.Width = Convert.ToDouble(StaticEntity.AppConfigurationSettings.LoadSettings(_Width));
                this.Height = Convert.ToDouble(StaticEntity.AppConfigurationSettings.LoadSettings(_Height));
                this.WindowState = (WindowState)Enum.Parse(typeof(WindowState), StaticEntity.AppConfigurationSettings.LoadSettings(WindowState.GetType().ToString()));
            }
            catch { }
            InitializeComponent();
            ViewModel viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StaticEntity.AppConfigurationSettings.Save(_Width, this.Width.ToString());
            StaticEntity.AppConfigurationSettings.Save(_Height, this.Height.ToString());
            StaticEntity.AppConfigurationSettings.Save(WindowState.GetType().ToString(), this.WindowState.ToString());
        }
    }
    
}
