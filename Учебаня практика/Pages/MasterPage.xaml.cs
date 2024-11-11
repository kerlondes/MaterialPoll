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

namespace Учебаня_практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        private readonly BDEntities m_entities;
        public MasterPage()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
        }

        private void Proiswodstwo_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную заявку из DataGrid
            var selectedOrder = Orders.SelectedItem as Order;

            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите заявку для изменения статуса");
                return;
            }

            // Проверяем, что статус еще не изменен (статус = false)
            if (selectedOrder.Proizweden == false)
            {
                // Меняем статус на true (Производство начато)
                selectedOrder.Proizweden = true;

                try
                {
                    // Сохраняем изменения в базе данных
                    m_entities.SaveChanges();

                    // Обновляем список заявок, чтобы отобразить изменения
                    var ordersWithFalseStatus = m_entities.Orders
                        .Where(order => order.Proizweden == false)
                        .ToList();
                    Orders.ItemsSource = ordersWithFalseStatus;

                    // Если больше нет заявок с Status = false, выключаем кнопку
                    Proiswodstwo.IsEnabled = ordersWithFalseStatus.Any();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при изменении статуса: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Статус уже изменен на 'Произведено'");
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
                var ordersWithFalseStatus = m_entities.Orders
                        .Where(order => order.Proizweden == false)
                        .ToList();
                // Устанавливаем их в DataGrid
                Orders.ItemsSource = ordersWithFalseStatus;
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;
            Proiswodstwo.IsEnabled = order != null && !order.Proizweden;
        }
    }
}
