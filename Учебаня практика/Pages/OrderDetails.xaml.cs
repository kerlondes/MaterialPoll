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
    /// Логика взаимодействия для OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Page
    {
        private BDEntities m_entities = BDEntities.GetInstance();
        private int m_orderID = 0;

        public OrderDetails(int orderID)
        {
            InitializeComponent();
            m_orderID = orderID;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsInOrder.ItemsSource = m_entities.ProductsInOrders
            .Where(a => a.OrderID == m_orderID)
            .ToList();
        }
    }
}
