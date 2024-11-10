using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using Учебаня_практика.Properties;

namespace Учебаня_практика.windows
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        private BDEntities m_entities;
        private Order m_currentOrder;

        public EditOrderWindow(Order order)
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
            m_currentOrder = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsInOrder.ItemsSource = m_currentOrder.ProductsInOrders;
            Date.Content = m_currentOrder.Date.ToString("dd.MM.yyyy");
            Status.IsChecked = m_currentOrder.Status;
        }

        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order()
            {
                ID = m_currentOrder.ID,
                Status = (bool)Status.IsChecked,
                Date = m_currentOrder.Date,
                UserID = m_currentOrder.UserID
            };

            try
            {
                m_entities.Orders.AddOrUpdate(order);
                m_entities.SaveChanges();
                MessageBox.Show("Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
