using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PartnerPage.xaml
    /// </summary>
    public partial class PartnerPage : Page
    {
        private readonly BDEntities m_entities;
        private readonly ObservableCollection<ProductsInOrder> m_productsInOrder;
        public PartnerPage()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
            m_productsInOrder = new ObservableCollection<ProductsInOrder>();
            ProductsInOrder.ItemsSource = m_productsInOrder;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Products.ItemsSource = m_entities.Products.ToList();
            UpdateOrderList();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem == null)
            {
                MessageBox.Show("Выбирете продукт");
                return;
            }

            if (!int.TryParse(Count.Text, out var count) || count < 1)
            {
                MessageBox.Show("Некорректное количество");
                return;
            }

            var productInOrder = new ProductsInOrder()
            {
                Amount = count,
                ProductArticle = (Products.SelectedItem as Product).Article,
                Product = (Products.SelectedItem as Product),
                Cost = (int)(count * (Products.SelectedItem as Product).MinCostForPartner),
            };
            m_productsInOrder.Add(productInOrder);

        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_productsInOrder.Count < 1)
            {
                MessageBox.Show("Добавьте продукты");
                return;
            }

            var orderDto = new Order()
            {
                UserID = 1,
                Date = DateTime.Now,
                Status = false
            };

            try
            {
                var order = m_entities.Orders.Add(orderDto);
                foreach (var item in m_productsInOrder)
                {
                    item.OrderID = order.ID;
                    m_entities.ProductsInOrders.Add(item);
                }
                m_entities.SaveChanges();
                m_productsInOrder.Clear();
                UpdateOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;
            RemoveOrderButton.IsEnabled = order != null && !order.Status;
        }

        private void RemoveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;
            try
            {
                m_entities.ProductsInOrders.RemoveRange(order.ProductsInOrders);
                m_entities.Orders.Remove(order);
                m_entities.SaveChanges();
                UpdateOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UpdateOrderList()
        {
            Orders.ItemsSource = m_entities.Orders
                   .Where(order => order.UserID == 1)
                   .ToList();
        }
    }
}
