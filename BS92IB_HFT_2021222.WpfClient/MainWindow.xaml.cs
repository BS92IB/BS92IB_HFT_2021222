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

namespace BS92IB_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            FleetWindow fw = new FleetWindow();
            fw.ShowDialog();
        }

        private void Button_click_1(object sender, RoutedEventArgs e)
        {
            ShipWindow sw = new ShipWindow();
            sw.ShowDialog();
        }

        private void Button_click_2(object sender, RoutedEventArgs e)
        {
            ArmamentWindow aw = new ArmamentWindow();
            aw.ShowDialog();
        }

        private void Button_click_3(object sender, RoutedEventArgs e)
        {
            WeaponWindow ww = new WeaponWindow();
            ww.ShowDialog();
        }
    }
}
