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
using Учебаня_практика.windows;

namespace Учебаня_практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private BDEntities m_entities = BDEntities.GetInstance();

        public StaffPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Staff.ItemsSource = m_entities.Users.ToList();
        }

        private void Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.IsEnabled = Staff.SelectedItem != null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditUserWindow();
            window.Closed += Window_Closed;
            window.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Staff.ItemsSource = m_entities.Users.ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditUserWindow(Staff.SelectedItem as User);
            window.Closed += Window_Closed;
            window.Show();
        }
    }
}
