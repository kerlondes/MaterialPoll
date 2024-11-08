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
using Учебаня_практика.Pages;

namespace Учебаня_практика
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new AuthPage());
        }
        public void NavigateToManagerPage()
        {
            mainFrame.Navigate(new ManagerPage());
        }

        public void NavigateToAnalystPage()
        {
            mainFrame.Navigate(new AnalystPage());
        }
        public void NavigateToMasterPage()
        {
            mainFrame.Navigate(new MasterPage());
        }

        public void NavigateToPartnerPage()
        {
            mainFrame.Navigate(new PartnerPage());
        }

        public void NavigateToStaffPage()
        {
            mainFrame.Navigate(new StaffPage());
        }
    }
}
